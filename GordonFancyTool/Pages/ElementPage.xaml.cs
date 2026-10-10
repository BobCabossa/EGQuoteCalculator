namespace GordonFancyTool.Pages;
#pragma warning disable IDE0060 // Remove messages about unused parmenter. As they come from public api's, i can't remove them.

public partial class ElementPage : Page
{
    private bool _loading = true;

    private readonly ElementValue _elementValues;

    public ProjectValues ProjectValue { get; set; }

    public ElementPage(ProjectValues projectValues, ElementValue elementValue)
    {
        List<ExcelData> excelDatas = FileController.LoadExcelDataList();
        List<ExcelCollection> excelCollections = FileController.LoadCollectionList();
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
            ElementItem? newItem = null;

            if (item.ItemType == ElementItemValue.ElementItemType.Single)
            {
                ExcelData? excelData = excelDatas.FirstOrDefault(e => e.Name == item.ExcelProductName);
                if (excelData == null) continue;

                newItem = CreateItem(item, excelData);
            }
            else if (item.ItemType == ElementItemValue.ElementItemType.Collection)
            {
                ExcelCollection? excelCollection = excelCollections.FirstOrDefault(e => e.Name == item.ExcelProductName);
                if (excelCollection == null) continue;

                //newItem = CreateItemCollection(item, excelCollection);
            }

            if (newItem == null) continue;

            newItem.Units.ValueString = item.Units.ToString();
            newItem.Length.ValueString = item.Length.ToString();
            newItem.PriceUpdated(this, "");
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

    public void BackToProject(object sender, RoutedEventArgs e)
    {
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
        ElementCollectionItem newItem = new();
        Items.Children.Add(newItem);

        return newItem;
    }

    private void ItemValueChanged(object? sender, string newValue)
    {
        // Save update
        if (sender is ElementItem item)
        {
            ElementItemValue? itemValue = _elementValues.ElementItemValues.FirstOrDefault(i => i.Number == item.ElementItemValue.Number);
            if (itemValue == null) return;

            itemValue.Length = item.ElementItemValue.Length;
            itemValue.Units = item.ElementItemValue.Units;
        }

        // Update UI values
        CalculateMinutesToInstall();

        SomeValuesChanged(sender, newValue);
    }

    private void SomeValuesChanged(object? sender, string newValue)
    {
        ValueChanged();
    }

    private void OnItemDelete(object? sender, ElementItemValue e)
    {
        int index = _elementValues.ElementItemValues.IndexOf(e);
        if (index == -1) return;

        _elementValues.ElementItemValues.RemoveAt(index);
        Items.Children.RemoveAt(index);
    }
}
