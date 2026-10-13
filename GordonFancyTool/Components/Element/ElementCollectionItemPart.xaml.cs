namespace GordonFancyTool.Components;

public partial class ElementCollectionItemPart : UserControl
{
    public ExcelCollectionItem CollectionItem { get; private set; }
    public ExcelData ExcelData { get; private set; }
    public Func<double> GetFullPrice;

    public ElementCollectionItemPart(ExcelCollectionItem collectionItem, ExcelData excelData, Func<double> getFullPrice)
    {
        CollectionItem = collectionItem;
        ExcelData = excelData;
        GetFullPrice = getFullPrice;

        InitializeComponent();

        Product.Text = excelData.Name;
        Price.Text = excelData.PurchasePrice;
        EAN.Text = excelData.EAN;
        MinToInstall.Text = excelData.MinToInstall.ToString();

        Units.Text = collectionItem.Quantity.ToString();
        Length.Text = collectionItem.Length.ToString();

        UpdatePrices(1);
    }

    public void UpdatePrices(double CollectionQuantity)
    {
        double pricePerUnit = DoubleCal.TryParse(Price.Text);
        double units = DoubleCal.TryParse(Units.Text);
        double length = DoubleCal.TryParse(Length.Text);

        units = DoubleCal.Round(units * CollectionQuantity);
        length = DoubleCal.Round(length * CollectionQuantity);

        double priceForUnits = pricePerUnit * units;
        double fullPrice = DoubleCal.Round(priceForUnits + (priceForUnits * length / 100));
        double profit = DoubleCal.Round(fullPrice - priceForUnits);

        FullPrice.Value = fullPrice;
        CompanyProfit.Value = profit;

        CalculateProcentageOfOffer();
    }

    public void CalculateProcentageOfOffer()
    {
        double totalFullPrice = GetFullPrice.Invoke();
        double percentageOfOffers = DoubleCal.Round(FullPrice.Value / totalFullPrice * 100);

        PercentageOfOffers.Value = percentageOfOffers;
    }
}
