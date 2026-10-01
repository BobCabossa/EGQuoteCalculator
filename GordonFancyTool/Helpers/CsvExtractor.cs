namespace GordonFancyTool.Helpers;

public static class CsvExtractor
{
    public static List<ExcelData>? LoadCsv(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        return new();
    }
}
