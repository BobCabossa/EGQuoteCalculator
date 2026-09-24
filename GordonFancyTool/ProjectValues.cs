namespace GordonFancyTool;

public class ProjectValues
{
    public double VAT = 25;

    public double FasteningPercentage { get; set; } = 1;
    public double MiscalculationPercentage { get; set; } = 10;
    public double MaterialCostsIncreasePercentage { get; set; } = 3.6;
    public double VehicleOnConstructionSitePrice { get; set; } = 50;
    public double EnvironmentalTaxPrice { get; set; } = 52;
    public double FixedEnergySurchargesPrice { get; set; } = 85;
    public double FixedPackagingContributionsPrice { get; set; } = 13;

    public double NormalHourlyRate { get; set; } = 225;
    public double StudentHourlyRate { get; set; } = 136;
    public double AdultStudentHourlyRate { get; set; } = 146.9;

    public double NormalCompanyProfitPercentage { get; set; } = 39.033;
    public double StudentCompanyProfitPercentage { get; set; } = 60.19;
    public double AdultStudentCompanyProfitPercentage { get; set; } = 48.305;

    public double SocialCost { get; set; } = 58;
    public double SalaryIncrease { get; set; } = 3;
    public double RiskFactor { get; set; } = 10;
}
