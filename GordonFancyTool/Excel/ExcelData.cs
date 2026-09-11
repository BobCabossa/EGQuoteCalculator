namespace GordonFancyTool.Excel;

public class ExcelData
{
    public string ProductName { get; set; } = string.Empty;
    public string PurchasePrice { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;

    public ExcelData() { }

    public ExcelData(string ProductName, string PurchasePrice, string EAN)
    {
        this.ProductName = ProductName;
        this.PurchasePrice = PurchasePrice;
    }
}
