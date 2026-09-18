namespace GordonFancyTool.Components;

public partial class ElementBottom : UserControl
{
    public ElementBottom()
    {
        InitializeComponent();
    }

    // Total
    public static readonly DependencyProperty FasteningProperty =
       DependencyProperty.Register(
           nameof(Fastening),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty MiscalculationProperty =
       DependencyProperty.Register(
           nameof(Miscalculation),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty MaterialCostsIncreaseProperty =
       DependencyProperty.Register(
           nameof(MaterialCostsIncrease),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty VehicleOnConstructionSiteProperty =
       DependencyProperty.Register(
           nameof(VehicleOnConstructionSite),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty EnvironmentalTaxProperty =
       DependencyProperty.Register(
           nameof(EnvironmentalTax),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty FixedEnergySurchargesProperty =
       DependencyProperty.Register(
           nameof(FixedEnergySurcharges),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty FixedPackagingContributionsProperty =
       DependencyProperty.Register(
           nameof(FixedPackagingContributions),
           typeof(string),
           typeof(ElementBottom));

    public string Fastening
    {
        set => SetValue(FasteningProperty, value);
        get => (string)GetValue(FasteningProperty);
    }

    public string Miscalculation
    {
        set => SetValue(MiscalculationProperty, value);
        get => (string)GetValue(MiscalculationProperty);
    }

    public string MaterialCostsIncrease
    {
        set => SetValue(MaterialCostsIncreaseProperty, value);
        get => (string)GetValue(MaterialCostsIncreaseProperty);
    }

    public string VehicleOnConstructionSite
    {
        set => SetValue(VehicleOnConstructionSiteProperty, value);
        get => (string)GetValue(VehicleOnConstructionSiteProperty);
    }

    public string EnvironmentalTax
    {
        set => SetValue(EnvironmentalTaxProperty, value);
        get => (string)GetValue(EnvironmentalTaxProperty);
    }

    public string FixedEnergySurcharges
    {
        set => SetValue(FixedEnergySurchargesProperty, value);
        get => (string)GetValue(FixedEnergySurchargesProperty);
    }

    public string FixedPackagingContributions
    {
        set => SetValue(FixedPackagingContributionsProperty, value);
        get => (string)GetValue(FixedPackagingContributionsProperty);
    }

    // Constitute to price
    public static readonly DependencyProperty FasteningConstitutingProperty =
        DependencyProperty.Register(
           nameof(FasteningConstituting),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty MiscalculationConstitutingProperty =
       DependencyProperty.Register(
           nameof(MiscalculationConstituting),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty MaterialCostsIncreaseConstitutingProperty =
       DependencyProperty.Register(
           nameof(MaterialCostsIncreaseConstituting),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty VehicleOnConstructionSiteConstitutingProperty =
       DependencyProperty.Register(
           nameof(VehicleOnConstructionSiteConstituting),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty EnvironmentalTaxConstitutingProperty =
       DependencyProperty.Register(
           nameof(EnvironmentalTaxConstituting),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty FixedEnergySurchargesConstitutingProperty =
       DependencyProperty.Register(
           nameof(FixedEnergySurchargesConstituting),
           typeof(string),
           typeof(ElementBottom));

    public static readonly DependencyProperty FixedPackagingContributionsConstitutingProperty =
       DependencyProperty.Register(
           nameof(FixedPackagingContributionsConstituting),
           typeof(string),
           typeof(ElementBottom));

    public string FasteningConstituting
    {
        set => SetValue(FasteningConstitutingProperty, value);
        get => (string)GetValue(FasteningConstitutingProperty);
    }

    public string MiscalculationConstituting
    {
        set => SetValue(MiscalculationConstitutingProperty, value);
        get => (string)GetValue(MiscalculationConstitutingProperty);
    }

    public string MaterialCostsIncreaseConstituting
    {
        set => SetValue(MaterialCostsIncreaseConstitutingProperty, value);
        get => (string)GetValue(MaterialCostsIncreaseConstitutingProperty);
    }

    public string VehicleOnConstructionSiteConstituting
    {
        set => SetValue(VehicleOnConstructionSiteConstitutingProperty, value);
        get => (string)GetValue(VehicleOnConstructionSiteConstitutingProperty);
    }

    public string EnvironmentalTaxConstituting
    {
        set => SetValue(EnvironmentalTaxConstitutingProperty, value);
        get => (string)GetValue(EnvironmentalTaxConstitutingProperty);
    }

    public string FixedEnergySurchargesConstituting
    {
        set => SetValue(FixedEnergySurchargesConstitutingProperty, value);
        get => (string)GetValue(FixedEnergySurchargesConstitutingProperty);
    }

    public string FixedPackagingContributionsConstituting
    {
        set => SetValue(FixedPackagingContributionsConstitutingProperty, value);
        get => (string)GetValue(FixedPackagingContributionsConstitutingProperty);
    }

    public void CalculateEverything()
    {

    }
}
