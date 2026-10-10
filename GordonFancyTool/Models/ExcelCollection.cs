namespace GordonFancyTool.Models;

public class ExcelCollection : SearchModel
{
    public List<ExcelCollectionItem> Items { get; set; } = new();

    public ExcelCollection() { }

    public ExcelCollection(string title)
    {
        Name = title;
    }
}
