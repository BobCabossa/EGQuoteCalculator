namespace GordonFancyTool;

public static class FileController
{
    private const string _subFolder = "data";
    private const string _elementsFile = "elementsValues.json";
    private const string _projectFile = "projectValues.json";
    private const string _excelFile = "ExcelData.json";

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = true
    };

    private static string GetSubFolder()
    {
        string path = Environment.CurrentDirectory + "\\" + _subFolder + "\\";

        if (Directory.Exists(path))
            return path;

        Directory.CreateDirectory(path);
        return path;
    }
    private static string GetElementFilePath() => GetSubFolder() + _elementsFile;
    private static string GetProjectFilePath() => GetSubFolder() + _projectFile;
    private static string GetExcelFilePath() => GetSubFolder() + _excelFile;

    public static List<ElementValue> LoadElements()
    {
        return LoadFile<List<ElementValue>>(GetElementFilePath());
    }

    public static ProjectValues LoadProjectValues()
    {
        return LoadFile<ProjectValues>(GetProjectFilePath());
    }

    public static ObservableCollection<ExcelData> LoadExcelData()
    {
        return LoadFile<ObservableCollection<ExcelData>>(GetExcelFilePath());
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
