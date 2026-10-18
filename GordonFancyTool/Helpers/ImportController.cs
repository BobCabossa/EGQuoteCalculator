namespace GordonFancyTool.Helpers;

public static class ImportController
{
    private static CancellationTokenSource? _cts;
    private enum FileType
    {
        Excel,
        CSV,
    }

    private static readonly string[] ExcelExtensions =
    [
        ".xlsx",
        ".xlsm"
    ];

    private const string CSV = ".csv";

    public static void StopWork()
    {
        _cts?.Cancel();
    }

    public static void StartImporting(ImportingPopup popup)
    {
        _cts = new CancellationTokenSource();
        Task.Run(() =>
        {
            List<(string, FileType)> dataPaths = GetAllDataPaths(popup);
            List<ExcelData> newDatas = ImportNewData(dataPaths, popup);
            List<ExcelData> dataToSave = OverrideExcelData(newDatas, popup);
            FileController.SaveExcelData(dataToSave);
            ActionsDone(popup);

            _cts.Dispose();
            _cts = null;
        }, _cts.Token);
    }

    private static List<(string, FileType)> GetAllDataPaths(ImportingPopup popup)
    {
        string subFolder = FileController.GetImportFolder();
        string[] files = Directory.GetFiles(subFolder);

        int actionsNeeded = 1;
        List<(string, FileType)> result = new();
        foreach (var file in files)
        {
            string extension = Path.GetExtension(file);

            if (ExcelExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                result.Add((file, FileType.Excel));
                actionsNeeded += 1;
            }
            else if (CSV.Contains(extension, StringComparison.OrdinalIgnoreCase))
            {
                result.Add((file, FileType.CSV));
                actionsNeeded += 1;
            }
        }

        UpdateActionsNeeded(actionsNeeded, popup);
        return result;
    }

    private static List<ExcelData> ImportNewData(List<(string, FileType)> dataPaths, ImportingPopup popup)
    {
        List<ExcelData> newDatas = new();
        foreach ((string path, FileType type) dataPath in dataPaths)
        {
            if (dataPath.type == FileType.Excel)
            {
                var newData = ExcelExtractor.LoadExcel(dataPath.path);
                ActionsDone(popup);
                if (newData == null)
                    continue;

                UpdateActionsNeeded(newData.Count, popup);
                newDatas.AddRange(newData);
            }
            else if (dataPath.type == FileType.CSV)
            {
                var newData = CsvExtractor.LoadCsv(dataPath.path);
                ActionsDone(popup);
                if (newData == null)
                    continue;

                UpdateActionsNeeded(newData.Count, popup);
                newDatas.AddRange(newData);
            }
        }

        return newDatas;
    }

    private static List<ExcelData> OverrideExcelData(List<ExcelData> newExcel, ImportingPopup popup)
    {
        List<ExcelData> excelToSave = new();
        List<ExcelData> oldExcel = FileController.LoadExcelDataList();

        foreach (ExcelData data in newExcel)
        {
            ExcelData? excel = oldExcel.FirstOrDefault(e => e.Name == data.Name);

            if (excel == null)
            {
                excelToSave.Add(data);
                ActionsDone(popup);
                continue;
            }

            excelToSave.Add(new(excel, data));
            ActionsDone(popup);
        }

        return excelToSave;
    }

    private static async void UpdateActionsNeeded(int actionsNeeded, ImportingPopup popup)
    {
        await popup.Dispatcher.BeginInvoke(() =>
        {
            popup.ActionsNeeded += actionsNeeded;
        }, DispatcherPriority.Background);
    }

    private static async void ActionsDone(ImportingPopup popup)
    {
        await popup.Dispatcher.BeginInvoke(() =>
        {
            popup.ActionsDone++;
        }, DispatcherPriority.Background);
    }
}
