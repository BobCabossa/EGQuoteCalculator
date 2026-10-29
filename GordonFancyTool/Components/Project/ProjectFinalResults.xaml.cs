namespace GordonFancyTool.Components;

public partial class ProjectFinalResults : UserControl
{
    public ProjectFinalResults()
    {
        InitializeComponent();
    }

    public void CalculateResult(double VAT, StackPanel itemsParent)
    {
        VAT /= 100;

        double priceExclusiveVAT = 0;
        double profit = 0;
        double contributionMarginRatio = 0;
        double normalStaffingTime = 0;
        double apprenticeStaffingTime = 0;

        foreach (var item in itemsParent.Children)
        {
            if (item is not ProjectItem projectItem)
                continue;

            priceExclusiveVAT += projectItem.ElementValue.TotalPriceExclusiveVAT;
            profit += projectItem.ElementValue.TotalProfit;
            normalStaffingTime += projectItem.NormalStaffingTime.Value;
            apprenticeStaffingTime += projectItem.ApprenticeStaffingTime.Value;

            double c = projectItem.ContributionMarginRatio.Value;
            contributionMarginRatio += double.IsNaN(c) ? 0 : c;
        }

        double vatPrice = VAT * priceExclusiveVAT;
        double totalPrice = priceExclusiveVAT + vatPrice;

        double totalStaffingDays = (normalStaffingTime + apprenticeStaffingTime) / 7.5;

        PriceExclusiveVAT.Value = priceExclusiveVAT;
        Profit.Value = profit;
        ContributionMarginRatio.Value = contributionMarginRatio;
        NormalStaffingTime.Value = normalStaffingTime;
        ApprenticeStaffingTime.Value = apprenticeStaffingTime;
        TotalStaffingTime.Value = totalStaffingDays;

        Vat.Value = VAT;
        VatPrice.Value = vatPrice;
        TotalPrice.Value = totalPrice;
    }
}
