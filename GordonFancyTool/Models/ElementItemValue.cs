namespace GordonFancyTool.Models;

public class ElementItemValue
{
    public enum ElementItemType
    {
        None,
        Single,
        Collection,
    }

    public ElementItemValue() { }
    public ElementItemValue(int number, ExcelData excelData)
    {
        Number = number;
        ExcelProductName = excelData.Name;
        ItemType = ElementItemType.Single;
    }

    public ElementItemValue(int number, ExcelCollection excelCollection)
    {
        Number = number;
        ExcelProductName = excelCollection.Name;
        ItemType = ElementItemType.Collection;
    }

    public int Number { get; set; }
    public double Length { get; set; }
    public double Units { get; set; }

    public string ExcelProductName { get; set; } = string.Empty;
    public ElementItemType ItemType { get; set; } = ElementItemType.None;
}
