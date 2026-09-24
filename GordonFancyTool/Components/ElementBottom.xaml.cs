namespace GordonFancyTool.Components;

public partial class ElementBottom : UserControl
{
    public ElementBottom()
    {
        InitializeComponent();
    }

    public void CalculateEverything(double totalPartPrice)
    {
        double fasteningPercentage = DoubleCal.TryParse(FasteningPercentage.Text);
        double miscalculationPercentage = DoubleCal.TryParse(MiscalculationPercentage.Text);
        double materialCostsIncreasePercentage = DoubleCal.TryParse(MaterialCostsIncreasePercentage.Text);
        double vehicleOnConstructionSitePrice = DoubleCal.TryParse(VehicleOnConstructionSitePrice.Text);
        double environmentalTaxPrice = DoubleCal.TryParse(EnvironmentalTaxPrice.Text);
        double fixedEnergySurchargesPrice = DoubleCal.TryParse(FixedEnergySurchargesPrice.Text);
        double fixedPackagingContributionsPrice = DoubleCal.TryParse(FixedPackagingContributionsPrice.Text);

        fasteningPercentage /= 100;
        miscalculationPercentage /= 100;
        materialCostsIncreasePercentage /= 100;

        double fasteningPrice = DoubleCal.Round(totalPartPrice * fasteningPercentage);
        double fasteningFullPrice = DoubleCal.Round(totalPartPrice + fasteningPrice);

        double miscalculationPrice = DoubleCal.Round(fasteningFullPrice * miscalculationPercentage);
        double miscalculationFullPrice = DoubleCal.Round(fasteningFullPrice + miscalculationPrice);

        double materialCostsIncreasePrice = DoubleCal.Round(miscalculationFullPrice * materialCostsIncreasePercentage);
        double materialCostsIncreaseFullPrice = DoubleCal.Round(miscalculationFullPrice + materialCostsIncreasePrice);

        double vehicleOnConstructionSiteFullPrice = DoubleCal.Round(miscalculationFullPrice + vehicleOnConstructionSitePrice);
        double environmentalTaxFullPrice = DoubleCal.Round(vehicleOnConstructionSiteFullPrice + environmentalTaxPrice);
        double fixedEnergySurchargesFullPrice = DoubleCal.Round(environmentalTaxFullPrice + fixedEnergySurchargesPrice);
        double fixedPackagingContributionsFullPrice = DoubleCal.Round(fixedEnergySurchargesFullPrice + fixedPackagingContributionsPrice);

        Fastening.Value = fasteningFullPrice;
        Miscalculation.Value = miscalculationFullPrice;
        MaterialCostsIncrease.Value = materialCostsIncreaseFullPrice;
        VehicleOnConstructionSite.Value = vehicleOnConstructionSiteFullPrice;
        EnvironmentalTax.Value = environmentalTaxFullPrice;
        FixedEnergySurcharges.Value = fixedEnergySurchargesFullPrice;
        FixedPackagingContributions.Value = fixedPackagingContributionsFullPrice;

        FasteningConstituting.Value = fasteningPrice;
        MiscalculationConstituting.Value = miscalculationPrice;
        MaterialCostsIncreaseConstituting.Value = materialCostsIncreasePrice;
        VehicleOnConstructionSiteConstituting.Value = vehicleOnConstructionSitePrice;
        EnvironmentalTaxConstituting.Value = environmentalTaxPrice;
        FixedEnergySurchargesConstituting.Value = fixedEnergySurchargesPrice;
        FixedPackagingContributionsConstituting.Value = fixedPackagingContributionsPrice;
    }

    public void CalculateContributions()
    {

    }
}
