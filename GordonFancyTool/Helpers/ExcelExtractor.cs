namespace GordonFancyTool.Helpers;

public static class ExcelExtractor
{
    public static List<ExcelData>? LoadExcel(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        using var workbook = new XLWorkbook(filePath); // Get ark
        var worksheet = workbook.Worksheet(1);          // Get worksheet 1

        var headerRow = worksheet.FirstRowUsed();
        if (headerRow == null)
        {
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
            .Address
            .ColumnNumber;
    }
}
