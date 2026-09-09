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
            typeof(string),
            typeof(ElementsTopRow));

    public static readonly DependencyProperty SocialCostProperty =
        DependencyProperty.Register(
            nameof(SocialCost), 
            typeof(string), 
            typeof(ElementsTopRow));

    public static readonly DependencyProperty SalaryIncreaseProperty =
        DependencyProperty.Register(
            nameof(SalaryIncrease), 
            typeof(string),
            typeof(ElementsTopRow));

    public static readonly DependencyProperty RiskFactorProperty =
        DependencyProperty.Register(
            nameof(RiskFactor),
            typeof(string),
            typeof(ElementsTopRow));

    public static readonly DependencyProperty CompanyProfitPercentageProperty =
        DependencyProperty.Register(
            nameof(CompanyProfitPercentage),
            typeof(string),
            typeof(ElementsTopRow));

    public static readonly DependencyProperty ShowProperty =
       DependencyProperty.Register(
           nameof(Show),
           typeof(string),
           typeof(ElementsTopRow));

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

    public string HourlyRate
    {
        set => SetValue(HourlyRateProperty, value);
        get => (string)GetValue(HourlyRateProperty);
    }

    public string SocialCost
    {
        set => SetValue(SocialCostProperty, value);
        get => (string)GetValue(SocialCostProperty);
    }

    public string SalaryIncrease
    {
        set => SetValue(SalaryIncreaseProperty, value);
        get => (string)GetValue(SalaryIncreaseProperty);
    }

    public string RiskFactor
    {
        set => SetValue(RiskFactorProperty, value);
        get => (string)GetValue(RiskFactorProperty);
    }

    public string CompanyProfitPercentage
    {
        set => SetValue(CompanyProfitPercentageProperty, value);
        get => (string)GetValue(CompanyProfitPercentageProperty);
    }

    public string Show
    {
        set => SetValue(ShowProperty, value);
        get => (string)GetValue(ShowProperty);
    }

    public void HourChanged(object sender, string newValue)
    {
        double? hours = Hours.Value;
        if (!hours.HasValue)
        {
            Sale.Text = string.Empty;
            PercentageOfOffers.Text = string.Empty;
            CompanyProfit.Text = string.Empty;
            SocialSecurityCosts.Text = string.Empty;
            AmountForSalaryIncrease.Text = string.Empty;
            AmountForRiskRate.Text = string.Empty;
            return;
        }

        _ = double.TryParse(HourlyRate, out double hourlyRate);
        _ = double.TryParse(SocialCost, out double socialCost);
        _ = double.TryParse(SalaryIncrease, out double salaryIncrease);
        _ = double.TryParse(RiskFactor, out double riskFactor);
        _ = double.TryParse(CompanyProfitPercentage, out double companyProfitPercentage);

        // From 50% to 0.5 to calculate with
        socialCost /= 100;
        salaryIncrease /= 100;
        riskFactor /= 100;
        companyProfitPercentage /= 100;

        double costPerHour = hourlyRate * hours.Value;

        double socialSecurityCosts = double.Round(costPerHour * socialCost, 2);
        double amountForSalaryIncrease = double.Round((costPerHour + socialSecurityCosts) * salaryIncrease, 2);
        double amountForRiskRate = double.Round((costPerHour + socialSecurityCosts + amountForSalaryIncrease) * riskFactor, 2);

        double companyProfit = double.Round((costPerHour + socialSecurityCosts + amountForSalaryIncrease + amountForRiskRate) * companyProfitPercentage, 2);

        double sale = double.Round(costPerHour + socialSecurityCosts + amountForSalaryIncrease + amountForRiskRate + companyProfit, 2);

        SocialSecurityCosts.Text = socialSecurityCosts.ToString();
        AmountForSalaryIncrease.Text = amountForSalaryIncrease.ToString();
        AmountForRiskRate.Text = amountForRiskRate.ToString();
        CompanyProfit.Text = companyProfit.ToString();
        Sale.Text = sale.ToString();
        CalculateHoursPercentage();
    }
    
    private void CalculateHoursPercentage()
    {
        _ = double.TryParse(Sale.Text, out double hoursPrice);
    }
}
