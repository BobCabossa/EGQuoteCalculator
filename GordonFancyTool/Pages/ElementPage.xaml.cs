namespace GordonFancyTool.Pages;
#pragma warning disable IDE0060 // Remove messages about unused parmenter. As they come from public api's, i can't remove them.

public partial class ElementPage : Page
{
    public class ElementPageLists(List<ExcelData> excelDatas, List<ExcelCollection> excelCollections)
    {
        public List<ExcelData> ExcelDatas { get; private set; } = excelDatas;
        public List<ExcelCollection> ExcelCollections { get; private set; } = excelCollections;
    }

    private bool _loading = true;

    private readonly ElementValue _elementValues;
    private readonly ElementPageLists PageLists;
    public ProjectValues ProjectValue { get; set; }

    public ElementPage(ProjectValues projectValues, ElementValue elementValue)
    {
        PageLists = new(
            FileController.LoadExcelDataList(),
            FileController.LoadCollectionList());

        _elementValues = elementValue;
        ProjectValue = projectValues;

        InitializeComponent();

        ElementName.Text = elementValue.Title;
        FinalResults.VATValue.Text = projectValues.VAT.ToString();
        HourlyPayStoppageTimePercentage.Value = projectValues.AdditionalStoppageTimePercentage;

        Normal.GetFullPrice = GetTotalPrice;
        Student.GetFullPrice = GetTotalPrice;
        AdultStudent.GetFullPrice = GetTotalPrice;

        Normal.Hours.ValueString = _elementValues.NormalHours.ToString();
        Student.Hours.ValueString = _elementValues.StudentHours.ToString();
        AdultStudent.Hours.ValueString = _elementValues.AdultStudentHours.ToString();

        foreach (var item in elementValue.ElementItemValues)
        {
            SearchModel? searchModel = item.ItemType switch
            {
                ElementItemValue.ElementItemType.Single => PageLists.ExcelDatas.FirstOrDefault(e => e.Name == item.ExcelProductName),
                ElementItemValue.ElementItemType.Collection => PageLists.ExcelCollections.FirstOrDefault(e => e.Name == item.ExcelProductName),
                _ => null, // Defualt
            };

            if (searchModel == null)
            {
                ElementUnknownItem unknownItem = new(item.ExcelProductName, RemoveUnknown);
                Items.Children.Add(unknownItem);
                continue;
            }

            if (item.ItemType == ElementItemValue.ElementItemType.Single && searchModel is ExcelData excelData)
            {
                ElementItem newItem = CreateItem(item, excelData);
                newItem.PriceUpdated(this, "");
            }
            else if (item.ItemType == ElementItemValue.ElementItemType.Collection && searchModel is ExcelCollection excelCollection)
            {
                ElementCollectionItem newItem = CreateItem(item, excelCollection);
                newItem.UpdateItemsQuantityAndPrices(item.CollectionQuantity);
                newItem.PriceUpdated();
            }
        }
    }

    private async void PageLoaded(object sender, RoutedEventArgs e)
    {
        await Dispatcher.BeginInvoke(() =>
        {
            Window.GetWindow(this).Title = "Element: " + _elementValues.Title;

            CalculateMinutesToInstall();
            HoursChanged(sender, "");

            _loading = false;
        }, DispatcherPriority.Render);
    }

    public void BackToProject(object sender, string e)
    {
        UpdateLocalElementItems();
        ProjectPage projectPage = new(_elementValues);

        NavigationService.Navigate(projectPage);
    }

    public void HoursChanged(object sender, string newValue)
    {
        Normal.CalculateHours();
        Student.CalculateHours();
        AdultStudent.CalculateHours();

        CalculateStoppageTime();
        CalculateTotalHours();
        ValueChanged();
    }

    public void AddItem(object? sender, SearchModel data)
    {
        int newNumber;
        try
        {
            newNumber = _elementValues.ElementItemValues.Last().Number + 1;
        }
        catch (InvalidOperationException)
        {
            newNumber = 1;
        }

        if (data is ExcelData excelData)
        {
            AddSingleItem(newNumber, excelData);
        }
        else if (data is ExcelCollection excelCollection)
        {
            AddCollectionItem(newNumber, excelCollection);
        }
    }

    private void AddSingleItem(int newNumber, ExcelData excelData)
    {
        ElementItemValue itemValue = new(newNumber, excelData);

        _ = CreateItem(itemValue, excelData);
        _elementValues.ElementItemValues.Add(itemValue);
    }

    private void AddCollectionItem(int newNumber, ExcelCollection excelCollection)
    {
        ElementItemValue itemValue = new(newNumber, excelCollection);

        _ = CreateItem(itemValue, excelCollection);
        _elementValues.ElementItemValues.Add(itemValue);
    }

    private ElementItem CreateItem(ElementItemValue itemValue, ExcelData excelData)
    {
        ElementItem newItem = new(itemValue, excelData, ItemValueChanged, OnItemDelete, GetTotalPrice);
        Items.Children.Add(newItem);

        return newItem;
    }

    private ElementCollectionItem CreateItem(ElementItemValue itemValue, ExcelCollection excelCollection)
    {
        ElementCollectionItem newItem = new(itemValue, excelCollection, PageLists, ItemValueChanged, OnItemDelete, GetTotalPrice);
        Items.Children.Add(newItem);

        return newItem;
    }

    private void ItemValueChanged(object? sender, string newValue)
    {
        // Update UI values
        CalculateMinutesToInstall();

        ValueChanged();
    }

    private void OnItemDelete(object? sender, ElementItemValue e)
    {
        int index = _elementValues.ElementItemValues.IndexOf(e);
        if (index == -1) return;

        _elementValues.ElementItemValues.RemoveAt(index);
        Items.Children.RemoveAt(index);
    }

    private void RemoveUnknown(object? sender, string name)
    {
        int index = _elementValues.ElementItemValues.FindIndex(e => e.ExcelProductName == name);
        if (index == -1) return;

        _elementValues.ElementItemValues.RemoveAt(index);
        Items.Children.RemoveAt(index);
    }

    private void UpdateLocalElementItems()
    {
        foreach (var child in Items.Children)
        {
            if (child is ElementItem item)
            {
                ElementItemValue? itemValue = _elementValues.ElementItemValues.FirstOrDefault(i => i.Number == item.ElementItemValue.Number);
                if (itemValue == null) return;

                itemValue.Length = item.ElementItemValue.Length;
                itemValue.Units = item.ElementItemValue.Units;
            }
            else if (child is ElementCollectionItem collectionItem)
            {
                ElementItemValue? itemValue = _elementValues.ElementItemValues.FirstOrDefault(i => i.Number == collectionItem.ElementItemValue.Number);
                if (itemValue == null) return;

                itemValue.CollectionQuantity = collectionItem.ElementItemValue.CollectionQuantity;
            }
        }
    }
}
