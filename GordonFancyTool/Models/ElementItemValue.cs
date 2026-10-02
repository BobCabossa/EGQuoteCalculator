namespace GordonFancyTool.Models;

public class ElementItemValue
{
    public ElementItemValue() { }
    public ElementItemValue(int number, ExcelData excelData)
    {
        Number = number;
        ExcelProductName = excelData.ProductName;
    }

    public int Number { get; set; }
    public double Length { get; set; }
    public double Units { get; set; }

    public string ExcelProductName { get; set; } = string.Empty;
}
