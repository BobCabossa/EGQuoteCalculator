namespace GordonFancyTool.Components;
#pragma warning disable IDE0060 // Remove messages about unused parmenter. As they come from public api's, i can't remove them.

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

    public ExcelData ExcelData { get; private set; }
    public event EventHandler<string> ValuesChanged;
    public event EventHandler<ElementItemValue> OnItemDelete;
    public Func<double> GetFullPrice;

    public ElementItem(ElementItemValue ElementItemValue, ExcelData excelData, EventHandler<string> valuesChanged, EventHandler<ElementItemValue> onItemDelete, Func<double> getFullPrice)
    {
        this.ElementItemValue = ElementItemValue;
        ExcelData = excelData;

        ValuesChanged = valuesChanged;
        OnItemDelete = onItemDelete;
        GetFullPrice = getFullPrice;

        InitializeComponent();

        Product.Text = excelData.ProductName;
        Price.Text = excelData.PurchasePrice;
        EAN.Text = excelData.EAN;
        MinToInstall.Text = excelData.MinToInstall.ToString();

        Index.Text = ElementItemValue.Number.ToString();
    }

    public void PriceUpdated(object sender, string e)
    {
        _ = double.TryParse(Price.Text, out double pricePerUnit);
        ElementItemValue ElementItemValue = this.ElementItemValue;

        double? units = Units.Value;
        double? length = Length.Value;

        if (units != null)
            ElementItemValue.Units = units.Value;

        if (length != null)
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

    private void OnDeleteItem(object sender, RoutedEventArgs e)
    {
        OnItemDelete.Invoke(sender, ElementItemValue);
    }
}
