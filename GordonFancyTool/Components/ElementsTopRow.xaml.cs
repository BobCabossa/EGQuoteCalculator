namespace GordonFancyTool.Components;

public partial class ElementsTopRow : UserControl
{
    public ElementsTopRow()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(ElementsTopRow));

    public static readonly DependencyProperty StaffingProperty =
        DependencyProperty.Register(
            nameof(Staffing),
            typeof(string),
            typeof(ElementsTopRow));

    public static readonly DependencyProperty HourlyRateProperty =
        DependencyProperty.Register(
            nameof(HourlyRate),
            typeof(double),
            typeof(ElementsTopRow));

    public static readonly DependencyProperty CompanyProfitPercentageProperty =
      DependencyProperty.Register(
          nameof(CompanyProfitPercentage),
          typeof(double),
          typeof(ElementsTopRow));

    public static readonly DependencyProperty ShowProperty =
       DependencyProperty.Register(
           nameof(Show),
           typeof(string),
           typeof(ElementsTopRow));

    public static readonly DependencyProperty ProjectValueProperty =
       DependencyProperty.Register(
           nameof(ProjectValue),
           typeof(ProjectValues),
           typeof(ElementsTopRow),
           new PropertyMetadata(
                new ProjectValues(),
                OnProjectValueChanged));

    public string Title
    {
        set => SetValue(TitleProperty, value);
        get => (string)GetValue(TitleProperty);
    }

    public string Staffing
    {
        set => SetValue(StaffingProperty, value);
        get => (string)GetValue(StaffingProperty);
    }

    public double HourlyRate
    {
        set => SetValue(HourlyRateProperty, value);
        get => (double)GetValue(HourlyRateProperty);
    }

    public double CompanyProfitPercentage
    {
        set => SetValue(CompanyProfitPercentageProperty, value);
        get => (double)GetValue(CompanyProfitPercentageProperty);
    }

    public string Show
    {
        set => SetValue(ShowProperty, value);
        get => (string)GetValue(ShowProperty);
    }

    public ProjectValues ProjectValue
    {
        set => SetValue(ProjectValueProperty, value);
        get => (ProjectValues)GetValue(ProjectValueProperty);
    }

    public event EventHandler<string>? HoursChanged;

    private static void OnProjectValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (ElementsTopRow)d;
        ProjectValues projectValue = (ProjectValues)e.NewValue;

        control.SocialCost.Value = projectValue.SocialCost;
        control.SalaryIncrease.Value = projectValue.SalaryIncrease;
        control.RiskFactor.Value = projectValue.RiskFactor;
    }

    public void HourChanged(object sender, string newValue)
    {
        double? hours = Hours.Value;
        if (!hours.HasValue)
        {
            Sale.Value = 0;
            PercentageOfOffers.Value = 0;
            CompanyProfit.Value = 0;
            SocialSecurityCosts.Value = 0;
            AmountForSalaryIncrease.Value = 0;
            AmountForRiskRate.Value = 0;
            return;
        }

        double companyProfitPercentage = CompanyProfitPercentage;
        double socialCost = SocialCost.Value;
        double salaryIncrease = SalaryIncrease.Value;
        double riskFactor = RiskFactor.Value;

        // From 50% to 0.5 to calculate with
        socialCost /= 100;
        salaryIncrease /= 100;
        riskFactor /= 100;
        companyProfitPercentage /= 100;

        double costPerHour = HourlyRate * hours.Value;

        double socialSecurityCosts = DoubleCal.Round(costPerHour * socialCost);
        double amountForSalaryIncrease = DoubleCal.Round((costPerHour + socialSecurityCosts) * salaryIncrease);
        double amountForRiskRate = DoubleCal.Round((costPerHour + socialSecurityCosts + amountForSalaryIncrease) * riskFactor);

        double companyProfit = DoubleCal.Round((costPerHour + socialSecurityCosts + amountForSalaryIncrease + amountForRiskRate) * companyProfitPercentage);

        double sale = DoubleCal.Round(costPerHour + socialSecurityCosts + amountForSalaryIncrease + amountForRiskRate + companyProfit);

        SocialSecurityCosts.Value = socialSecurityCosts;
        AmountForSalaryIncrease.Value = amountForSalaryIncrease;
        AmountForRiskRate.Value = amountForRiskRate;
        CompanyProfit.Value = companyProfit;
        Sale.Value = sale;
        HoursChanged?.Invoke(sender, newValue);
        CalculateHoursContributions();
    }

    public void CalculateHoursContributions()
    {

    }
}
