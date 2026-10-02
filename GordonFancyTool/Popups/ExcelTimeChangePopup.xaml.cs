namespace GordonFancyTool.Popups;

public partial class ExcelTimeChangePopup : Window
{
    private readonly List<ExcelTimeItem> _items;

    public ExcelTimeChangePopup()
    {
        List<ExcelData> datas = FileController.LoadExcelDataList();

        InitializeComponent();
        _items = new();
        foreach (var item in datas)
        {
            items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            int newIndex = items.RowDefinitions.Count;

            ExcelTimeItem newItem = new(item);
            items.Children.Add(newItem);

            Grid.SetRow(newItem, newIndex - 1);

            _items.Add(newItem);
        }
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;

        List<ExcelData> data = new();
        foreach (var item in _items)
        {
            data.Add(item.ExcelData);
        }

        FileController.SaveExcelData(data);
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
