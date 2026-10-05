namespace GordonFancyTool.Popups;

public partial class CreateCollectionsPopup : Window
{
    private ObservableCollection<ExcelData> _data;
    public List<ExcelCollection> ExcelCollections { get; private set; } = new();

    public CreateCollectionsPopup()
    {
        _data = FileController.LoadExcelDataObservableCollection();
        ExcelCollections = FileController.LoadCollection();

        InitializeComponent();

        foreach (ExcelCollection collection in ExcelCollections)
        {
            CreateItem(collection);
        }
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        foreach (var item in ExcelCollections)
        {

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
        if (title == null) return;

        ExcelCollection newCollection = new(title);
        ExcelCollections.Add(newCollection);
        CreateItem(newCollection);
    }

    private void CreateItem(ExcelCollection collection)
    {
        items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        int newIndex = items.RowDefinitions.Count - 1;

        CollectionItem newItem = new(collection, _data);

        Grid.SetRow(newItem, newIndex);

        items.Children.Add(newItem);
    }
}
