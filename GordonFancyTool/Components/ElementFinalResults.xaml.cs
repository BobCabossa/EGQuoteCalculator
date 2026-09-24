namespace GordonFancyTool.Components;

public partial class ElementFinalResults : UserControl
{
    public ElementFinalResults()
    {
        InitializeComponent();
    }

    public void CalculateResults(double totalHoursPay, double materialCostsIncrease, ProjectValues projectValues)
    {
        double vehicleOnConstructionSitePrice = projectValues.VehicleOnConstructionSitePrice;
        double environmentalTaxPrice = projectValues.EnvironmentalTaxPrice;
        double fixedEnergySurchargesPrice = projectValues.FixedEnergySurchargesPrice;
        double fixedPackagingContributionsPrice = projectValues.FixedPackagingContributionsPrice;

        double totalFixedPrices = vehicleOnConstructionSitePrice + environmentalTaxPrice + fixedEnergySurchargesPrice + fixedPackagingContributionsPrice;

        double totalPriceExclusiveVAT = totalHoursPay + materialCostsIncrease + totalFixedPrices;
        
        double VATPercentage = projectValues.VAT / 100;
        double VAT = DoubleCal.Round(totalPriceExclusiveVAT * VATPercentage);

        double fullPrice = totalPriceExclusiveVAT + VAT;

        TotalPriceForStaffing.Value = totalHoursPay;
        TotalPriceForMaterial.Value = materialCostsIncrease;
        TotalPriceForFixedPrices.Value = totalFixedPrices;

        TotalPriceExclusiveVAT.Value = totalPriceExclusiveVAT;
        FullPrice.Value = fullPrice;
        this.VAT.Value = VAT;
    }
}
