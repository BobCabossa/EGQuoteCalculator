namespace GordonFancyTool.Helpers;

public static class FileController
{
    private const string _importFolder = "Import";
    private const string _dataFolder = "data";
    private const string _elementsFile = "ElementsValues.json";
    private const string _projectFile = "ProjectValues.json";
    private const string _excelFile = "ExcelData.json";

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = true
    };

    private static void CreateFolder(string folder)
    {
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
    }

    public static string GetImportFolder()
    {
        string path = Path.Combine(AppContext.BaseDirectory, _importFolder);
        CreateFolder(path);
        return path;
    }

    private static string GetDataFolder()
    {
        string path = Path.Combine(AppContext.BaseDirectory, _dataFolder);

        CreateFolder(path);
        return path;
    }

    private static string GetElementFilePath() => Path.Combine(GetDataFolder(), _elementsFile);
    private static string GetProjectFilePath() => Path.Combine(GetDataFolder(), _projectFile);
    private static string GetExcelFilePath() => Path.Combine(GetDataFolder(), _excelFile);

    public static List<ElementValue> LoadElements()
    {
        return LoadFile<List<ElementValue>>(GetElementFilePath());
    }

    public static ProjectValues LoadProjectValues()
    {
        return LoadFile<ProjectValues>(GetProjectFilePath());
    }

    public static ObservableCollection<ExcelData> LoadExcelDataObservableCollection()
    {
        return LoadFile<ObservableCollection<ExcelData>>(GetExcelFilePath());
    }

    public static List<ExcelData> LoadExcelDataList()
    {
        return LoadFile<List<ExcelData>>(GetExcelFilePath());
    }

    public static void SaveElements(List<ElementValue> elementValues)
    {
        SaveFile(GetElementFilePath(), elementValues);
    }

    public static void SaveProjectSettings(ProjectValues projectValues)
    {
        SaveFile(GetProjectFilePath(), projectValues);
    }

    public static void SaveExcelData(List<ExcelData> data)
    {
        SaveFile(GetExcelFilePath(), data);
    }

    private static void SaveFile(string filePath, object data)
    {
        string json = JsonSerializer.Serialize(data, _serializerOptions);

        File.WriteAllText(filePath, json);
    }

    private static T LoadFile<T>(string path) where T : class, new()
    {
        if (!File.Exists(path))
        {
            return new T();
        }

        T? value;
        try
        {
            string rawJson = File.ReadAllText(path);
            value = JsonSerializer.Deserialize<T>(rawJson);
        }
        catch (JsonException)
        {
            return new();
        }

        if (value == null)
        {
            return new();
        }

        return value;
    }
}
