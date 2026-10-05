namespace GordonFancyTool.Models;

public class ExcelCollectionItem
{
    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; } = 0;

    public ExcelCollectionItem() { }

    public ExcelCollectionItem(string name)
    {
        Name = name;
    }
}
