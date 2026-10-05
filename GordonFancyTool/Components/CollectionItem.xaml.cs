namespace GordonFancyTool.Components;

public partial class CollectionItem : UserControl
{
    public ExcelCollection ExcelCollection { get; private set; }
    public ObservableCollection<ExcelData> _data { get; private set; }

    public CollectionItem(ExcelCollection collection, ObservableCollection<ExcelData> excelDatas)
    {
        _data = excelDatas;
        ExcelCollection = collection;
        InitializeComponent();

        foreach (var excelName in collection.Items)
        {
            CreateItem(excelName);
        }
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
        ExcelData? excelData = _data.FirstOrDefault(e => e.ProductName == collectionItem.Name);
        if (excelData == null) return;

        CollectionItemPart newPart = new(excelData, collectionItem, OnRemovePart);
        Items.Children.Add(newPart);
    }

    private void OnRemovePart(object? sender, string e)
    {
        if (sender is not CollectionItemPart collectionPart)
            return;

        // remove it...
    }
}
