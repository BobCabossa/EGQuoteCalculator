namespace GordonFancyTool.Popups;

public partial class ExcelTimeChangePopup : Window
{
    public readonly List<ExcelTimeItem> Items = new();

    public ExcelTimeChangePopup()
    {
        List<ExcelData> datas = FileController.LoadExcelDataList();

        InitializeComponent();

        foreach (var item in datas)
        {
            items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            int newIndex = items.RowDefinitions.Count;

            ExcelTimeItem newItem = new(item);
            items.Children.Add(newItem);

            Grid.SetRow(newItem, newIndex - 1);

            Items.Add(newItem);
        }
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
