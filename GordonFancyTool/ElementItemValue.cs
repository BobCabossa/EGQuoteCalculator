namespace GordonFancyTool;

public class ElementItemValue
{
    public ElementItemValue() { }
    public ElementItemValue(int number, ExcelData excelData) 
    {
        Number = number;
        ExcelData = excelData;
    }

    public int Number { get; set; }
    public double Length { get; set; }
    public double Units { get; set; }

    public ExcelData ExcelData { get; set; } = new();
}
