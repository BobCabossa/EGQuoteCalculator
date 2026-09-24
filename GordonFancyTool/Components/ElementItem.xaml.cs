namespace GordonFancyTool.Components;

public partial class ElementItem : UserControl
{
    private readonly ExcelData excelData;

    public event EventHandler<string> ValuesChanged;

    public ElementItem(ExcelData excelData, EventHandler<string> ValuesChanged, int number)
    {
        InitializeComponent();

        Product.Text = excelData.ProductName;
        Price.Text = excelData.PurchasePrice;
        EAN.Text = excelData.EAN;
        this.excelData = excelData;
        this.ValuesChanged += ValuesChanged;
        Index.Text = number.ToString();
    }

    public void PriceUpdated(object sender, string e)
    {
        _ = double.TryParse(excelData.PurchasePrice, out double pricePerUnit);
        double? units = Units.Value;
        double? length = Length.Value;

        if (units == null || length == null) return;

        double priceForUnits = pricePerUnit * units.Value;
        double fullPrice = DoubleCal.Round(priceForUnits + (priceForUnits * length.Value / 100));
        double profit = DoubleCal.Round(fullPrice - priceForUnits);

        FullPrice.Value = fullPrice;
        CompanyProfit.Value = profit;

        CalculateProcentageOfOffer();

        ValuesChanged.Invoke(this, e);
    }

    public void CalculateProcentageOfOffer()
    {
        PercentageOfOffers.Value = 0;
    }
}
