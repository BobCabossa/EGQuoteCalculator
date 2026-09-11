namespace GordonFancyTool.Excel;

public class ExcelExtractor
{
    public static string? ChooseFile()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Choose Excel file",
            Filter = "Excel files (*.xlsx;*.xltx)|*.xlsx;*.xltx|All files (*.*)|*.*",
            Multiselect = false
        };

        if (dialog.ShowDialog() == true)
        {
            // Do something with the file
            MessageBox.Show(dialog.FileName);

            return dialog.FileName;
        }

        return null;
    }

    public static ExcelData[]? LoadData(string filePath)
    {
        string excelFile = string.IsNullOrEmpty(filePath) ? "E:\\Downloads\\be0ea9e7f9df4669953e073c636648f4.xlsx" : filePath;

        if (!File.Exists(excelFile))
        {
            MessageBox.Show("Excel filen kunne ikke finds");
            return null;
        }

        using var workbook = new XLWorkbook(excelFile); // Get ark
        var worksheet = workbook.Worksheet(1);          // Get worksheet 1

        var headerRow = worksheet.FirstRowUsed();
        if (headerRow == null)
        {
            MessageBox.Show("Excel filen er tom");
            return null;
        }

        int ProduktnavnColumn = GetCellId(headerRow, "Produktnavn");
        int Indkøbspris = GetCellId(headerRow, "Indkøbspris");
        int EAN = GetCellId(headerRow, "Ean");

        List<ExcelData> data = [];
        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            data.Add(new ExcelData
            (
                row.Cell(ProduktnavnColumn).GetString(),
                row.Cell(Indkøbspris).GetString(),
                row.Cell(EAN).GetString()
            ));
        }

        return data.ToArray();
    }

    private static int GetCellId(IXLRow headerRow, string columnName)
    {
        return headerRow.Cells()
            .First(c => c.GetString() == columnName)
            .Address.ColumnNumber;
    }
}
