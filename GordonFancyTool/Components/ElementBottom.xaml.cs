namespace GordonFancyTool.Components;

public partial class ElementBottom : UserControl
{
    public static readonly DependencyProperty ProjectValueProperty =
       DependencyProperty.Register(
           nameof(ProjectValue),
           typeof(ProjectValues),
           typeof(ElementBottom),
           new PropertyMetadata(
                new ProjectValues(),
                OnProjectValueChanged));

    public ProjectValues ProjectValue
    {
        set => SetValue(ProjectValueProperty, value);
        get => (ProjectValues)GetValue(ProjectValueProperty);
    }

    public ElementBottom()
    {
        InitializeComponent();
    }

    private static void OnProjectValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (ElementBottom)d;
        ProjectValues projectValue = (ProjectValues)e.NewValue;

        control.FasteningPercentage.Value = projectValue.FasteningPercentage;
        control.MiscalculationPercentage.Value = projectValue.MiscalculationPercentage;
        control.MaterialCostsIncreasePercentage.Value = projectValue.MaterialCostsIncreasePercentage;
        control.VehicleOnConstructionSitePrice.Value = projectValue.VehicleOnConstructionSitePrice;
        control.EnvironmentalTaxPrice.Value = projectValue.EnvironmentalTaxPrice;
        control.FixedEnergySurchargesPrice.Value = projectValue.FixedEnergySurchargesPrice;
        control.FixedPackagingContributionsPrice.Value = projectValue.FixedPackagingContributionsPrice;
    }

    public void CalculateEverything(double totalPartPrice)
    {
        double fasteningPercentage = FasteningPercentage.Value;
        double miscalculationPercentage = MiscalculationPercentage.Value;
        double materialCostsIncreasePercentage = MaterialCostsIncreasePercentage.Value;
        double vehicleOnConstructionSitePrice = VehicleOnConstructionSitePrice.Value;
        double environmentalTaxPrice = EnvironmentalTaxPrice.Value;
        double fixedEnergySurchargesPrice = FixedEnergySurchargesPrice.Value;
        double fixedPackagingContributionsPrice = FixedPackagingContributionsPrice.Value;

        fasteningPercentage /= 100;
        miscalculationPercentage /= 100;
        materialCostsIncreasePercentage /= 100;

        double fasteningPrice = DoubleCal.Round(totalPartPrice * fasteningPercentage);
        double fasteningFullPrice = totalPartPrice + fasteningPrice;

        double miscalculationPrice = DoubleCal.Round(fasteningFullPrice * miscalculationPercentage);
        double miscalculationFullPrice = fasteningFullPrice + miscalculationPrice;

        double materialCostsIncreasePrice = DoubleCal.Round(miscalculationFullPrice * materialCostsIncreasePercentage);
        double materialCostsIncreaseFullPrice = miscalculationFullPrice + materialCostsIncreasePrice;

        double vehicleOnConstructionSiteFullPrice = miscalculationFullPrice + vehicleOnConstructionSitePrice;
        double environmentalTaxFullPrice = vehicleOnConstructionSiteFullPrice + environmentalTaxPrice;
        double fixedEnergySurchargesFullPrice = environmentalTaxFullPrice + fixedEnergySurchargesPrice;
        double fixedPackagingContributionsFullPrice = fixedEnergySurchargesFullPrice + fixedPackagingContributionsPrice;

        double fasteningConstitutngPercentage = DoubleCal.Round(fasteningPrice / miscalculationFullPrice * 100);
        double miscalculationConstitutngPercentage = DoubleCal.Round(miscalculationPrice / miscalculationFullPrice * 100);

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

        FasteningConstitutngPercentage.Value = fasteningConstitutngPercentage;
        MiscalculationConstitutngPercentage.Value = miscalculationConstitutngPercentage;
    }
}
