namespace GordonFancyTool.Models;

public class ExcelCollection : SearchModel
{
    public int Index { get; set; }
    public List<ExcelCollectionItem> Items { get; set; } = new();

    public ExcelCollection() { }

    public ExcelCollection(int index, string title)
    {
        Name = title;
        Index = index;
    }
}
