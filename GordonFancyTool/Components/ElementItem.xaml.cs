namespace GordonFancyTool.Components;

public partial class ElementItem : UserControl
{
    private readonly ExcelData excelData;

    public event EventHandler<string> ValuesChanged;

    public ElementItem(ExcelData excelData, EventHandler<string> ValuesChanged)
    {
        InitializeComponent();

        Product.Text = excelData.ProductName;
        Price.Text = excelData.PurchasePrice;
        EAN.Text = excelData.EAN;
        this.excelData = excelData;
        this.ValuesChanged += ValuesChanged;
    }

    public static readonly DependencyProperty IndexProperty =
       DependencyProperty.Register(
           nameof(Index),
           typeof(string),
           typeof(ElementItem));

    public static readonly DependencyProperty FullPriceProperty =
       DependencyProperty.Register(
           nameof(FullPrice),
           typeof(string),
           typeof(ElementItem));

    public static readonly DependencyProperty PercentageOfOffersProperty =
       DependencyProperty.Register(
           nameof(PercentageOfOffers),
           typeof(string),
           typeof(ElementItem));

    public static readonly DependencyProperty CompanyProfitProperty =
       DependencyProperty.Register(
           nameof(CompanyProfit),
           typeof(string),
           typeof(ElementItem));

    public string Index
    {
        set => SetValue(IndexProperty, value);
        get => (string)GetValue(IndexProperty);
    }

    public string FullPrice
    {
        set => SetValue(FullPriceProperty, value);
        get => (string)GetValue(FullPriceProperty);
    }
    
    public string PercentageOfOffers
    {
        set => SetValue(PercentageOfOffersProperty, value);
        get => (string)GetValue(PercentageOfOffersProperty);
    }

    public string CompanyProfit
    {
        set => SetValue(CompanyProfitProperty, value);
        get => (string)GetValue(CompanyProfitProperty);
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

        FullPrice = fullPrice.ToString();
        CompanyProfit = profit.ToString();

        CalculateProcentageOfOffer();

        ValuesChanged.Invoke(this, e);
    }

    public void CalculateProcentageOfOffer()
    {
        PercentageOfOffers = "0";
    }
}
