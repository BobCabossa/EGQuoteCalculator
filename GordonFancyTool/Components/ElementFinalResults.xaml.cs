namespace GordonFancyTool.Components;

public partial class ElementFinalResults : UserControl
{
    public ElementFinalResults()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty TotalPriceForStaffingProperty =
       DependencyProperty.Register(
           nameof(TotalPriceForStaffing),
           typeof(double),
           typeof(ElementFinalResults));
    
    public static readonly DependencyProperty TotalPriceForMaterialProperty =
       DependencyProperty.Register(
           nameof(TotalPriceForMaterial),
           typeof(double),
           typeof(ElementFinalResults));

    public static readonly DependencyProperty TotalPriceForFixedPricesProperty =
       DependencyProperty.Register(
           nameof(TotalPriceForFixedPrices),
           typeof(double),
           typeof(ElementFinalResults));

    public static readonly DependencyProperty TotalPriceExclusiveVATProperty =
       DependencyProperty.Register(
           nameof(TotalPriceExclusiveVAT),
           typeof(double),
           typeof(ElementFinalResults));

    public static readonly DependencyProperty VATProperty =
       DependencyProperty.Register(
           nameof(VAT),
           typeof(double),
           typeof(ElementFinalResults));

    public static readonly DependencyProperty FullPriceProperty =
       DependencyProperty.Register(
           nameof(FullPrice),
           typeof(double),
           typeof(ElementFinalResults));

    public double TotalPriceForStaffing
    {
        set => SetValue(TotalPriceForStaffingProperty, value);
        get => (double)GetValue(TotalPriceForStaffingProperty);
    }

    public double TotalPriceForMaterial
    {
        set => SetValue(TotalPriceForMaterialProperty, value);
        get => (double)GetValue(TotalPriceForMaterialProperty);
    }

    public double TotalPriceForFixedPrices
    {
        set => SetValue(TotalPriceForFixedPricesProperty, value);
        get => (double)GetValue(TotalPriceForFixedPricesProperty);
    }

    public double TotalPriceExclusiveVAT
    {
        set => SetValue(TotalPriceExclusiveVATProperty, value);
        get => (double)GetValue(TotalPriceExclusiveVATProperty);
    }

    public double VAT
    {
        set => SetValue(VATProperty, value);
        get => (double)GetValue(VATProperty);
    }

    public double FullPrice
    {
        set => SetValue(FullPriceProperty, value);
        get => (double)GetValue(FullPriceProperty);
    }

    public void CalculateResults(string totalHoursPayText, string materialCostsIncreaseText, ProjectValues projectValues)
    {
        double totalHoursPay = DoubleCal.TryParse(totalHoursPayText);
        double materialCostsIncrease = DoubleCal.TryParse(materialCostsIncreaseText);

        double vehicleOnConstructionSitePrice = projectValues.vehicleOnConstructionSitePrice;
        double environmentalTaxPrice = projectValues.environmentalTaxPrice;
        double fixedEnergySurchargesPrice = projectValues.fixedEnergySurchargesPrice;
        double fixedPackagingContributionsPrice = projectValues.fixedPackagingContributionsPrice;

        double totalFixedPrices = vehicleOnConstructionSitePrice + environmentalTaxPrice + fixedEnergySurchargesPrice + fixedPackagingContributionsPrice;

        double totalPriceExclusiveVAT = totalHoursPay + materialCostsIncrease + totalFixedPrices;
        
        double VATPercentage = projectValues.VAT / 100;
        double VAT = DoubleCal.Round(totalPriceExclusiveVAT * VATPercentage);

        double fullPrice = totalPriceExclusiveVAT + VAT;

        TotalPriceForStaffing = totalHoursPay;
        TotalPriceForMaterial = materialCostsIncrease;
        TotalPriceForFixedPrices = totalFixedPrices;

        TotalPriceExclusiveVAT = totalPriceExclusiveVAT;
        FullPrice = fullPrice;
        this.VAT = VAT;
    }
}
