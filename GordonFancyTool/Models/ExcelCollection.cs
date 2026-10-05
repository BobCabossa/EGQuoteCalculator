namespace GordonFancyTool.Models;

public class ExcelCollection
{
    public string Title { get; set; } = string.Empty;
    public List<ExcelCollectionItem> Items { get; set; } = new();

    public ExcelCollection() { }

    public ExcelCollection(string title)
    {
        Title = title;
    }
}
