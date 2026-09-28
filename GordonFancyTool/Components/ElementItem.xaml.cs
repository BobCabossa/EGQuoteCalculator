namespace GordonFancyTool.Components;

public partial class ElementItem : UserControl
{
    public static readonly DependencyProperty ElementItemValueProperty =
    DependencyProperty.Register(
        nameof(ElementItemValue),
        typeof(ElementItemValue),
        typeof(ElementItem));

    public ElementItemValue ElementItemValue
    {
        set => SetValue(ElementItemValueProperty, value);
        get => (ElementItemValue)GetValue(ElementItemValueProperty);
    }

    public event EventHandler<string> ValuesChanged;
    public Func<double> GetFullPrice;

    public ElementItem(ElementItemValue ElementItemValue, EventHandler<string> ValuesChanged, Func<double> getFullPrice)
    {
        this.ElementItemValue = ElementItemValue;
        InitializeComponent();

        Product.Text = ElementItemValue.ExcelData.ProductName;
        Price.Text = ElementItemValue.ExcelData.PurchasePrice;
        EAN.Text = ElementItemValue.ExcelData.EAN;

        Index.Text = ElementItemValue.Number.ToString();

        this.ValuesChanged += ValuesChanged;
        GetFullPrice = getFullPrice;
    }

    public void PriceUpdated(object sender, string e)
    {
        _ = double.TryParse(Price.Text, out double pricePerUnit);
        ElementItemValue ElementItemValue = this.ElementItemValue;

        double? units = Units.Value;
        double? length = Length.Value;

        if (units != null)
            ElementItemValue.Units = units.Value;

        else if (length != null)
            ElementItemValue.Length = length.Value;

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
