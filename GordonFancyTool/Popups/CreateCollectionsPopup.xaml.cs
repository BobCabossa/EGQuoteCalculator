namespace GordonFancyTool.Popups;

public partial class CreateCollectionsPopup : Window
{
    private readonly ObservableCollection<ExcelData> _data;
    public List<CollectionItem> ExcelCollections { get; private set; } = new();

    public CreateCollectionsPopup()
    {
        _data = FileController.LoadExcelDataObservableCollection();
        List<ExcelCollection> Collections = FileController.LoadCollection();

        InitializeComponent();

        foreach (ExcelCollection collection in Collections)
        {
            CreateItem(collection);
        }
    }

    private void OkClick(object sender, RoutedEventArgs e)
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

        ExcelCollection newCollection = new(title ?? "");
        CreateItem(newCollection);
    }

    private void CreateItem(ExcelCollection collection)
    {
        CollectionItem newItem = new(collection, _data, OnRemoveItem);
        items.Children.Add(newItem);
        ExcelCollections.Add(newItem);
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
