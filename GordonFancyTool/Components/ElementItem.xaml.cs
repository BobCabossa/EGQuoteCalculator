namespace GordonFancyTool.Components;

public partial class ElementItem : UserControl
{
    public event EventHandler<string> ValuesChanged;
    public Func<double> GetFullPrice;

    public ElementItem(ExcelData excelData, int number, EventHandler<string> ValuesChanged, Func<double> getFullPrice)
    {
        InitializeComponent();

        Product.Text = excelData.ProductName;
        Price.Text = excelData.PurchasePrice;
        EAN.Text = excelData.EAN;
        
        Index.Text = number.ToString();

        this.ValuesChanged += ValuesChanged;
        GetFullPrice = getFullPrice;
    }

    public void PriceUpdated(object sender, string e)
    {
        _ = double.TryParse(Price.Text, out double pricePerUnit);
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
        double totalFullPrice = GetFullPrice.Invoke();
        double percentageOfOffers = DoubleCal.Round(FullPrice.Value / totalFullPrice * 100);
        
        PercentageOfOffers.Value = percentageOfOffers;
    }
}
