namespace GordonFancyTool.Components;

public partial class CollectionItem : UserControl
{
    public ExcelCollection ExcelCollection { get; private set; }
    public ObservableCollection<ExcelData> Data { get; private set; }
    private readonly List<CollectionItemPart> _itemParts = new();

    public event EventHandler<CollectionItem> DeleteItem;

    public CollectionItem(ExcelCollection collection, ObservableCollection<ExcelData> excelDatas, EventHandler<CollectionItem> deleteItem)
    {
        DeleteItem = deleteItem;
        Data = excelDatas;
        ExcelCollection = collection;
        InitializeComponent();

        foreach (var excelName in collection.Items)
        {
            CreateItem(excelName);
        }

        CollectionsTitle.Text = collection.Title;
    }

    public void UpdateCollection()
    {
        foreach (var item in Items.Children)
        {
            if (item is not CollectionItemPart itemPart)
                continue;

            // Update existing
            int index = ExcelCollection.Items.FindIndex(e => e.Name == itemPart.CollectionItem.Name);
            if (index > -1)
            {
                ExcelCollection.Items[index] = itemPart.CollectionItem;
                continue;
            }

            // Else add as a new part
            ExcelCollection.Items.Add(itemPart.CollectionItem);
        }
    }

    public void AddItem(object? sender, ExcelData excelData)
    {
        ExcelCollectionItem collectionItem = new(excelData.ProductName);
        ExcelCollection.Items.Add(collectionItem);
        CreateItem(collectionItem);
    }

    private void CreateItem(ExcelCollectionItem collectionItem)
    {
        ExcelData? excelData = Data.FirstOrDefault(e => e.ProductName == collectionItem.Name);
        if (excelData == null) return;

        CollectionItemPart newPart = new(excelData, collectionItem, OnRemovePart);
        Items.Children.Add(newPart);
        _itemParts.Add(newPart);
    }

    private void OnRemovePart(object? sender, string e)
    {
        if (sender is not CollectionItemPart collectionPart)
            return;

        int index = _itemParts.IndexOf(collectionPart);
        if (index == -1)
            return;
        
        Items.Children.RemoveAt(index);
        _itemParts.RemoveAt(index);
    }

    public void OnRemoveItem(object sender, RoutedEventArgs e)
    {
        DeleteItem.Invoke(sender, this);
    }

    private void OnChangeTitle(object? sender, RoutedEventArgs e)
    {
        var dialog = new NewElementPopup("Endre navn", "Indtast et navn:", ExcelCollection.Title)
        {
            Owner = Window.GetWindow(this)
        };

        if (dialog.ShowDialog() == true)
        {
            ExcelCollection.Title = dialog.Result ?? "";
            CollectionsTitle.Text = dialog.Result ?? "";
        }
    }
}
