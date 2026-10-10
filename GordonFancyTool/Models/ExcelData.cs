namespace GordonFancyTool.Models;

public class ExcelData : SearchModel
{
    // This comes from the imported files.
    public string PurchasePrice { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;

    // This sets the users themselves.
    public double MinToInstall { get; set; } = 0;

    public ExcelData() { }

    public ExcelData(string ProductName, string PurchasePrice, string EAN)
    {
        this.Name = ProductName;
        this.PurchasePrice = PurchasePrice;
        this.EAN = EAN;
    }

    public ExcelData(ExcelData OldData, ExcelData newData)
    {
        Name = OldData.Name;
        MinToInstall = OldData.MinToInstall;

        PurchasePrice = newData.PurchasePrice;
        EAN = newData.EAN;
    }
}
