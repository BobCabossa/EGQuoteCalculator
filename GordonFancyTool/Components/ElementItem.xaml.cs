namespace GordonFancyTool.Components;

public partial class ElementItem : UserControl
{
    private ExcelData excelData;

    public ElementItem(ExcelData excelData)
    {
        InitializeComponent();

        Product.Text = excelData.ProductName;
        Price.Text = excelData.PurchasePrice;
        EAN.Text = excelData.EAN;
        this.excelData = excelData;
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
        CompanyProfit.Text = profit.ToString();
    }
}
