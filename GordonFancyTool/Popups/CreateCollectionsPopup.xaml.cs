namespace GordonFancyTool.Popups;

public partial class CreateCollectionsPopup : Window
{
    private readonly ObservableCollection<ExcelData> _data;
    public List<CollectionItem> ExcelCollections { get; private set; } = new();

    public CreateCollectionsPopup()
    {
        _data = FileController.LoadExcelDataObservableCollection();
        List<ExcelCollection> Collections = FileController.LoadCollectionList();

        InitializeComponent();

        foreach (ExcelCollection collection in Collections)
        {
            CreateItem(collection);
        }
    }

    public void OkClick(object sender, string e)
    {
        foreach (var item in ExcelCollections)
        {
            item.UpdateCollection();
        }

        DialogResult = true;
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void AddCollection(object sender, RoutedEventArgs e)
    {
        var dialog = new NewElementPopup("Ny samling", "Indtast et navn:")
        {
            Owner = this
        };

        if (dialog.ShowDialog() == false) return;

        string? title = dialog.Result;
        int newIndex;
        try
        {
            newIndex = ExcelCollections.Last().ExcelCollection.Index + 1;
        }
        catch (InvalidOperationException)
        {
            newIndex = 1;
        }

        ExcelCollection newCollection = new(newIndex, title ?? "");
        CreateItem(newCollection);
    }

    private void CreateItem(ExcelCollection collection)
    {
        CollectionItem newItem = new(collection, _data, OnRemoveItem, OnItemNameChanged);
        items.Children.Add(newItem);
        ExcelCollections.Add(newItem);
    }

    private void OnItemNameChanged(object? sender, CollectionItem itemChanged)
    {
        int itemIndex = ExcelCollections.IndexOf(itemChanged);
        if (itemIndex == -1)
            return;

        foreach (var item in ExcelCollections)
        {
            item.AnotherCollectionNameUpdated(itemChanged);
        }
    }

    private void OnRemoveItem(object? sender, CollectionItem deleteCollection)
    {
        int index = ExcelCollections.IndexOf(deleteCollection);
        if (index == -1)
            return;

        ExcelCollections.RemoveAt(index);
        items.Children.RemoveAt(index);
    }
}
