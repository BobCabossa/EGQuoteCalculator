namespace GordonFancyTool;

internal class ExcelData
{
    public string ProductName { get; set; } = string.Empty;
    public string PurchasePrice { get; set; } = string.Empty;

    public ExcelData() { }

    public ExcelData(string ProductName, string PurchasePrice)
    {
        this.ProductName = ProductName;
        this.PurchasePrice = PurchasePrice;
    }
}
