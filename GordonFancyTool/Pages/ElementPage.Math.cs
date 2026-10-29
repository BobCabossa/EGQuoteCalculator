namespace GordonFancyTool.Pages;

public partial class ElementPage : Page
{
    public double GetTotalPrice() => FinalResults.TotalPriceExclusiveVAT.Value;

    // To save the users input and some value so we don't have to do them in project page
    private void SetUpdatedValues()
    {
        if (_loading) return;

        double normal = Normal.Hours.Value ?? 0;
        double student = Student.Hours.Value ?? 0;
        double adultStudent = AdultStudent.Hours.Value ?? 0;

        _elementValues.NormalHours = normal;
        _elementValues.StudentHours = student;
        _elementValues.AdultStudentHours = adultStudent;

        _elementValues.NormalMaterialHours = Normal.MaterialTime.Value;
        _elementValues.StudentsMaterialHours = Student.MaterialTime.Value + AdultStudent.MaterialTime.Value;

        _elementValues.TotalPriceExclusiveVAT = FinalResults.TotalPriceExclusiveVAT.Value;
        _elementValues.TotalProfit = FinalResults.TotalProfit.Value;
    }

    // Value somewhere changed and everything need to update their values
    private void ValueChanged()
    {
        double totalPartPirce = CalculateTotalPartPrice();

        BottomPart.CalculateEverything(totalPartPirce);
        FinalResults.CalculateResults(TotalHoursPay.Value, BottomPart.MaterialCostsIncrease.Value, TotalHoursProfit.Value, TotalPartProfit.Value, ProjectValue);

        Normal.CalculateHoursContributions();
        Student.CalculateHoursContributions();
        AdultStudent.CalculateHoursContributions();

        CalculatePartContributions();

        double StoppageTimeContributions = HourlyPayStoppageTimePrice.Value / GetTotalPrice() * 100;
        HourlyPayStoppageTimeContributions.Value = StoppageTimeContributions;

        SetUpdatedValues();
    }

    // Time below
    private void CalculateTotalHours()
    {
        // Total hours their workers need to complete the task.
        double normal = Normal.Hours.Value ?? 0;
        double student = Student.Hours.Value ?? 0;
        double adultStudent = AdultStudent.Hours.Value ?? 0;
        double additionalTime = HourlyPayStoppageTime.Value;

        double normalProfit = Normal.CompanyProfit.Value;
        double studentProfit = Student.CompanyProfit.Value;
        double adultStudentProfit = AdultStudent.CompanyProfit.Value;
        double additionalProfit = HourlyPayStoppageTimeProfit.Value;

        double normalSale = Normal.Sale.Value;
        double studentSale = Student.Sale.Value;
        double adultStudentSale = AdultStudent.Sale.Value;
        double additionalPay = HourlyPayStoppageTimePrice.Value;

        double hours = normal + student + adultStudent + additionalTime;
        double totalSale = normalSale + studentSale + adultStudentSale + additionalPay;
        double totalHoursProfit = normalProfit + studentProfit + adultStudentProfit + additionalProfit;

        TotalHoursPay.Value = totalSale;
        TotalHours.Value = hours;
        TotalHoursProfit.Value = totalHoursProfit;

        SetUpdatedValues();
    }

    private void CalculateStoppageTime()
    {
        double hourlyPayStoppageTimePercentage = HourlyPayStoppageTimePercentage.Value / 100;

        double normalHours = Normal.Hours.Value ?? 0;
        double studentHours = Student.Hours.Value ?? 0;
        double adultStudentHours = AdultStudent.Hours.Value ?? 0;

        double normalCompanyProfit = Normal.CompanyProfit.Value;
        double studentCompanyProfit = Student.CompanyProfit.Value;
        double adultStudentCompanyProfit = AdultStudent.CompanyProfit.Value;

        double normalPrice = Normal.Sale.Value;
        double studentPrice = Student.Sale.Value;
        double adultStudentPrice = AdultStudent.Sale.Value;

        // Hours
        double workTime = normalHours + studentHours + adultStudentHours;
        double additionalWorkTime = workTime * hourlyPayStoppageTimePercentage;

        // Pay for labor
        double normalAdditionalPay = normalPrice * normalHours * hourlyPayStoppageTimePercentage;
        double studentAdditionalPay = studentPrice * studentHours * hourlyPayStoppageTimePercentage;
        double adultStudentAdditionalPay = adultStudentPrice * adultStudentHours * hourlyPayStoppageTimePercentage;

        double additionalPay = normalAdditionalPay + studentAdditionalPay + adultStudentAdditionalPay;

        // Profit for the work
        double normalProfit = normalCompanyProfit * hourlyPayStoppageTimePercentage;
        double studentProfit = studentCompanyProfit * hourlyPayStoppageTimePercentage;
        double adultStudentProfit = adultStudentCompanyProfit * hourlyPayStoppageTimePercentage;

        double additionalProfit = normalProfit + studentProfit + adultStudentProfit;

        HourlyPayStoppageTime.Value = additionalWorkTime;
        HourlyPayStoppageTimePrice.Value = additionalPay;
        HourlyPayStoppageTimeProfit.Value = additionalProfit;
    }

    // Parts below
    private double CalculateTotalPartPrice()
    {
        double totalPartPirce = 0;
        double totalPartProfit = 0;
        foreach (var item in Items.Children)
        {

            if (item is ElementItem itemValue)
            {
                totalPartPirce += itemValue.FullPrice.Value;
                totalPartProfit += itemValue.CompanyProfit.Value;
            }
            else if (item is ElementCollectionItem collectionItem)
            {
                totalPartPirce += collectionItem.FullPrice.Value;
                totalPartProfit += collectionItem.CompanyProfit.Value;
            }
        }

        TotalPartPirce.Value = totalPartPirce;
        TotalPartProfit.Value = totalPartProfit;

        return totalPartPirce;
    }

    private void CalculatePartContributions()
    {
        double totalPartPercentage = 0;
        foreach (var item in Items.Children)
        {
            if (item is ElementItem itemValue)
            {
                itemValue.CalculateProcentageOfOffer();
                totalPartPercentage += itemValue.PercentageOfOffers.Value;
            }
            else if (item is ElementCollectionItem collectionItem)
            {
                collectionItem.CalculateProcentageOfOfferOfChilrens();
                collectionItem.CalculateProcentageOfOffer();
                totalPartPercentage += collectionItem.PercentageOfOffers.Value;
            }
        }

        TotalPartPercentage.Value = totalPartPercentage;
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
