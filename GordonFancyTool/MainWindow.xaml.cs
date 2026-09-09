namespace GordonFancyTool;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private ObservableCollection<ExcelData> data = new();
    //private string filePath = string.Empty;

    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new ProjectPage());
    }

    //private void ChooseFile_Click(object sender, RoutedEventArgs e)
    //{
    //    var dialog = new OpenFileDialog
    //    {
    //        Title = "Choose Excel file",
    //        Filter = "Excel files (*.xlsx;*.xltx)|*.xlsx;*.xltx|All files (*.*)|*.*",
    //        Multiselect = false
    //    };

    //    if (dialog.ShowDialog() == true)
    //    {
    //        filePath = dialog.FileName;

    //        // Do something with the file
    //        MessageBox.Show(filePath);
    //    }
    //}

    //private void LoadData_Click(object sender, RoutedEventArgs e)
    //{
    //    string excelFile = string.IsNullOrEmpty(filePath) ? "E:\\Downloads\\be0ea9e7f9df4669953e073c636648f4.xlsx" : filePath;
    //    using var workbook = new XLWorkbook(excelFile); // Get ark
    //    var worksheet = workbook.Worksheet(1);          // Get worksheet 1

    //    var headerRow = worksheet.FirstRowUsed();
    //    if (headerRow == null)
    //    {
    //        MessageBox.Show("Excel fil tom");
    //        return;
    //    }

    //    int LMnrColumn =        GetCellId(headerRow, "LMnr");
    //    int EanColumn =         GetCellId(headerRow, "Ean");
    //    int ProduktnavnColumn = GetCellId(headerRow, "Produktnavn");
    //    int TypenummerColumn =  GetCellId(headerRow, "Typenummer");
    //    int LeverandørColumn =  GetCellId(headerRow, "Leverandør");
    //    int BrandColumn =       GetCellId(headerRow, "Brand");
    //    int ListeprisColumn =   GetCellId(headerRow, "Listepris");
    //    int Indkøbspris =       GetCellId(headerRow, "Indkøbspris");
    //    int AntalColumn =       GetCellId(headerRow, "Antal");
    //    int EnhedColumn =       GetCellId(headerRow, "Enhed");

    //    foreach (var row in worksheet.RowsUsed().Skip(1))
    //    {
    //        data.Add(new ExcelData
    //        (
    //            //row.Cell(EanColumn).GetValue<int>(),
    //            row.Cell(LMnrColumn).GetString(),
    //            row.Cell(EanColumn).GetString(),
    //            row.Cell(ProduktnavnColumn).GetString(),
    //            row.Cell(TypenummerColumn).GetString(),
    //            row.Cell(LeverandørColumn).GetString(),
    //            row.Cell(BrandColumn).GetString(),
    //            row.Cell(ListeprisColumn).GetString(),
    //            row.Cell(Indkøbspris).GetString(),
    //            row.Cell(AntalColumn).GetString(),
    //            row.Cell(EnhedColumn).GetString()
    //        ));
    //    }

    //    DataGrid.ItemsSource = data;
    //}

    //private static int GetCellId(IXLRow headerRow, string columnName)
    //{
    //    return headerRow.Cells()
    //        .First(c => c.GetString() == columnName)
    //        .Address.ColumnNumber;
    //}
}