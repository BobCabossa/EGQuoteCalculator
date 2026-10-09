namespace GordonFancyTool.Pages;

public partial class ElementPage : Page
{
    public double GetTotalPrice() => FinalResults.TotalPriceExclusiveVAT.Value;

    private void ValueChanged()
    {
        double totalPartPirce = CalculateTotalPartPrice();

        BottomPart.CalculateEverything(totalPartPirce);
        FinalResults.CalculateResults(TotalHoursPay.Value, BottomPart.MaterialCostsIncrease.Value, TotalHoursProfit.Value, TotalPartProfit.Value, ProjectValue);

        Normal.CalculateHoursContributions();
        Student.CalculateHoursContributions();
        AdultStudent.CalculateHoursContributions();

        CalculatePartContributions();

        double StoppageTimeContributions = DoubleCal.Round(HourlyPayStoppageTimePrice.Value / GetTotalPrice() * 100);
        HourlyPayStoppageTimeContributions.Value = StoppageTimeContributions;
    }

    private void CalculateTotalHours()
    {
        // Total hours their workers need to complete the task.
        double normal = Normal.Hours.Value ?? 0;
        double student = Student.Hours.Value ?? 0;
        double adultStudent = AdultStudent.Hours.Value ?? 0;
        double additionalTime = HourlyPayStoppageTime.Value;

        double hours = DoubleCal.Round(normal + student + adultStudent + additionalTime);

        // Total pay the company gets for their man power.
        double normalSale = Normal.Sale.Value;
        double studentSale = Student.Sale.Value;
        double adultStudentSale = AdultStudent.Sale.Value;
        double additionalPay = HourlyPayStoppageTimePrice.Value;

        double totalSale = DoubleCal.Round(normalSale + studentSale + adultStudentSale + additionalPay);

        double normalProfit = Normal.CompanyProfit.Value;
        double studentProfit = Student.CompanyProfit.Value;
        double adultStudentProfit = AdultStudent.CompanyProfit.Value;
        double additionalProfit = HourlyPayStoppageTimeProfit.Value;

        double totalHoursProfit = DoubleCal.Round(normalProfit + studentProfit + adultStudentProfit + additionalProfit);

        TotalHoursPay.Value = totalSale;
        TotalHours.Value = hours;
        TotalHoursProfit.Value = totalHoursProfit;

        if (!_loading)
        {
            _elementValues.NormalHours = normal;
            _elementValues.StudentHours = student;
            _elementValues.AdultStudentHours = adultStudent;
        }
    }

    private double CalculateTotalPartPrice()
    {
        double totalPartPirce = 0;
        double totalPartProfit = 0;
        foreach (var item in Items.Children)
        {
            if (item is not ElementItem itemValue)
                continue;

            totalPartPirce += itemValue.FullPrice.Value;
            totalPartProfit += itemValue.CompanyProfit.Value;
        }

        TotalPartPirce.Value = DoubleCal.Round(totalPartPirce);
        TotalPartProfit.Value = DoubleCal.Round(totalPartProfit);

        return totalPartPirce;
    }

    private void CalculatePartContributions()
    {
        double totalPartPercentage = 0;
        foreach (var item in Items.Children)
        {
            if (item is not ElementItem itemValue)
                continue;

            itemValue.CalculateProcentageOfOffer();

            totalPartPercentage += itemValue.PercentageOfOffers.Value;
        }

        TotalPartPercentage.Value = DoubleCal.Round(totalPartPercentage);
    }

    private void CalculateStoppageTime()
    {
        double hourlyPayStoppageTimePercentage = HourlyPayStoppageTimePercentage.Value / 100;

        double normalHours = Normal.Hours.Value ?? 0;
        double studentHours = Student.Hours.Value ?? 0;
        double adultStudentHours = AdultStudent.Hours.Value ?? 0;
        double workTime = DoubleCal.Round(normalHours + studentHours + adultStudentHours);
        double additionalWorkTime = DoubleCal.Round(workTime * hourlyPayStoppageTimePercentage);

        double normalPrice = Normal.Sale.Value;
        double studentPrice = Student.Sale.Value;
        double adultStudentPrice = AdultStudent.Sale.Value;

        double normalAdditionalPay = DoubleCal.Round(normalPrice * normalHours * hourlyPayStoppageTimePercentage);
        double studentAdditionalPay = DoubleCal.Round(studentPrice * studentHours * hourlyPayStoppageTimePercentage);
        double adultStudentAdditionalPay = DoubleCal.Round(adultStudentPrice * adultStudentHours * hourlyPayStoppageTimePercentage);

        double additionalPay = DoubleCal.Round(normalAdditionalPay + studentAdditionalPay + adultStudentAdditionalPay);

        double normalProfit = DoubleCal.Round(Normal.CompanyProfit.Value * hourlyPayStoppageTimePercentage);
        double studentProfit = DoubleCal.Round(Student.CompanyProfit.Value * hourlyPayStoppageTimePercentage);
        double adultStudentProfit = DoubleCal.Round(AdultStudent.CompanyProfit.Value * hourlyPayStoppageTimePercentage);

        double additionalProfit = DoubleCal.Round(normalProfit + studentProfit + adultStudentProfit);

        HourlyPayStoppageTime.Value = additionalWorkTime;
        HourlyPayStoppageTimePrice.Value = additionalPay;
        HourlyPayStoppageTimeProfit.Value = additionalProfit;
    }

    private void CalculateMinutesToInstall()
    {
        double totalTime = 0;
        foreach (var elementItem in Items.Children)
        {
            if (elementItem is not ElementItem itemValue)
                continue;

            totalTime += itemValue.ExcelData.MinToInstall * itemValue.ElementItemValue.Units;
        }
        Normal.SetMaterialTime(totalTime);
    }
}
