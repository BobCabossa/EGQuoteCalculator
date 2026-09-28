namespace GordonFancyTool.Helpers;

public static class ExcelExtractor
{
    public static void OverrideExcelData()
    {
        string? file = ChooseFile();
        if (string.IsNullOrEmpty(file))
            return;

        List<ExcelData>? loadedData = LoadData(file);

        if (loadedData == null) return;

        FileController.SaveExcelData(loadedData);
        MessageBox.Show("Excel data blev succesfuld indskreven.");
    }

    private static string? ChooseFile()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Vælg Excel filen",
            Filter = "Excel files (*.xlsx;*.xltx)|*.xlsx;*.xltx|All files (*.*)|*.*",
            Multiselect = false
        };

        if (dialog.ShowDialog() == true)
        {
            return dialog.FileName;
        }

        return null;
    }

    private static List<ExcelData>? LoadData(string filePath)
    {
        string excelFile = filePath;

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

        return data;
    }

    private static int GetCellId(IXLRow headerRow, string columnName)
    {
        return headerRow.Cells()
            .First(c => c.GetString() == columnName)
            .Address.ColumnNumber;
    }
}
