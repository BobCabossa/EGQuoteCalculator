namespace GordonFancyTool.Helpers;

public static class ImportController
{
    private static readonly string[] ExcelExtensions =
    [
        ".xlsx",
        ".xlsm"
    ];

    private const string CSV = ".csv";

    public static void StartImporting()
    {
        List<ExcelData> newDatas = ImportNewData();
        List<ExcelData> dataToSave = OverrideExcelData(newDatas);
        FileController.SaveExcelData(dataToSave);
    }

    private static List<ExcelData> ImportNewData()
    {
        string subFolder = FileController.GetImportFolder();
        string[] files = Directory.GetFiles(subFolder);

        List<ExcelData> newDatas = new();
        foreach (string file in files)
        {
            string extension = Path.GetExtension(file);

            if (ExcelExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                var newData = ExcelExtractor.LoadExcel(file);
                if (newData == null)
                    continue;

                newDatas.AddRange(newData);
            }
            else if (CSV.Contains(extension, StringComparison.OrdinalIgnoreCase))
            {
                var newData = CsvExtractor.LoadCsv(file);
                if (newData == null)
                    continue;

                newDatas.AddRange(newData);
            }
        }

        return newDatas;
    }

    private static List<ExcelData> OverrideExcelData(List<ExcelData> newExcel)
    {
        List<ExcelData> excelToSave = new();
        List<ExcelData> oldExcel = FileController.LoadExcelDataList();

        foreach (ExcelData data in newExcel)
        {
            ExcelData? excel = oldExcel.FirstOrDefault(e => e.ProductName == data.ProductName);

            if (excel == null)
            {
                excelToSave.Add(data);
                continue;
            }

            excelToSave.Add(new(excel, data));
        }

        return excelToSave;
    }
}
