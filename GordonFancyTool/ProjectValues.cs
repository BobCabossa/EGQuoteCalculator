namespace GordonFancyTool;

public class ProjectValues
{
    public double VAT = 25;

    public double fasteningPercentage { get; set; } = 1;
    public double miscalculationPercentage { get; set; } = 10;
    public double materialCostsIncreasePercentage { get; set; } = 3.6;
    public double vehicleOnConstructionSitePrice { get; set; } = 50;
    public double environmentalTaxPrice { get; set; } = 52;
    public double fixedEnergySurchargesPrice { get; set; } = 85;
    public double fixedPackagingContributionsPrice { get; set; } = 13;

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
