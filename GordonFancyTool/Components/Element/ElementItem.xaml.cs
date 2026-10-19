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

    public ElementItem(ElementItemValue elementItemValue, ExcelData excelData, EventHandler<string> valuesChanged, EventHandler<ElementItemValue> onItemDelete, Func<double> getFullPrice)
    {
        ElementItemValue = elementItemValue;
        ExcelData = excelData;

        ValuesChanged = valuesChanged;
        OnItemDelete = onItemDelete;
        GetFullPrice = getFullPrice;

        InitializeComponent();

        Product.Text = excelData.Name;
        Price.Text = excelData.PurchasePrice;
        EAN.Text = excelData.EAN;
        MinToInstall.Text = excelData.MinToInstall.ToString();

        Units.ValueString = elementItemValue.Units.ToString();
        Length.ValueString = elementItemValue.Length.ToString();

        Index.Text = elementItemValue.Number.ToString();
    }

    public void PriceUpdated(object sender, string e)
    {
        _ = double.TryParse(Price.Text, out double pricePerUnit);
        ElementItemValue elementItemValue = ElementItemValue;

        double? units = Units.Value;
        double? length = Length.Value;

        // Updates item values!!!
        if (units != null)
            elementItemValue.Units = units.Value;

        if (length != null)
            elementItemValue.Length = length.Value;

        if (units == null || length == null) return;

        double priceForUnits = pricePerUnit * units.Value;
        double fullPrice = priceForUnits + (priceForUnits * length.Value / 100);
        double profit = fullPrice - priceForUnits;

        FullPrice.Value = fullPrice;
        CompanyProfit.Value = profit;

        CalculateProcentageOfOffer();

        ValuesChanged.Invoke(this, e);
    }

    public void CalculateProcentageOfOffer()
    {
        double totalFullPrice = GetFullPrice.Invoke();
        double percentageOfOffers = FullPrice.Value / totalFullPrice * 100;

        PercentageOfOffers.Value = percentageOfOffers;
    }

    public void OnDeleteItem(object sender, string e)
    {
        OnItemDelete.Invoke(sender, ElementItemValue);
    }
}
