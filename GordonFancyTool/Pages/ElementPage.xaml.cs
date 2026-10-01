namespace GordonFancyTool.Pages;
#pragma warning disable IDE0060 // Remove messages about unused parmenter. As they come from public api's, i can't remove them.

public partial class ElementPage : Page
{
    private const string stringValue = "Don'tContinue";

    private readonly ElementValue _elementValues;

    public ProjectValues ProjectValue { get; set; }
    public ObservableCollection<ExcelData> ExcelDatas { get; }

    public ElementPage(ProjectValues projectValues, ElementValue elementValue)
    {
        ExcelDatas = FileController.LoadExcelDataObservableCollection();
        _elementValues = elementValue;
        ProjectValue = projectValues;
        InitializeComponent();
        ElementName.Text = elementValue.Title;
        FinalResults.VATValue.Text = projectValues.VAT.ToString();
        HourlyPayStoppageTimePercentage.Value = projectValues.AdditionalStoppageTimePercentage;

        Normal.GetFullPrice = GetTotalPrice;
        Student.GetFullPrice = GetTotalPrice;
        AdultStudent.GetFullPrice = GetTotalPrice;

        foreach (var item in elementValue.ElementItemValues)
        {
            ElementItem newItem = CreateItem(item, item.ExcelData);
            newItem.Units.Value = item.Units;
            newItem.Length.Value = item.Length;
            newItem.PriceUpdated(this, "");
        }
    }

    public double GetTotalPrice() => FinalResults.TotalPriceExclusiveVAT.Value;

    private void PageLoaded(object sender, RoutedEventArgs e)
    {
        Dispatcher.BeginInvoke(() =>
        {
            Window.GetWindow(this).Title = "Element: " + _elementValues.Title;
            Normal.Hours.Value = _elementValues.NormalHours;
            Student.Hours.Value = _elementValues.StudentHours;
            AdultStudent.Hours.Value = _elementValues.AdultStudentHours;

            HoursChanged(sender, "");
            foreach (var item in Items.Children)
            {
                ItemValueChanged(item, stringValue);
            }

        }, DispatcherPriority.Render);
    }

    public void BackToProject(object sender, RoutedEventArgs e)
    {
        ProjectPage projectPage = new(_elementValues);

        NavigationService.Navigate(projectPage);
    }

    public void HoursChanged(object sender, string newValue)
    {
        Normal.CalculateHours();
        Student.CalculateHours();
        AdultStudent.CalculateHours();
        CalculateStoppageTime();

        // Total hours their workers need to complete the task.
        double normal = Normal.Hours.Value ?? 0;
        double student = Student.Hours.Value ?? 0;
        double adultStudent = AdultStudent.Hours.Value ?? 0;
        double additionalTime = HourlyPayStoppageTime.Value;

        double hours = normal + student + adultStudent + additionalTime;

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

        SomeValuesChanged(sender, newValue);
    }

    public void AddItem(object? sender, ExcelData excelData)
    {
        ElementItem item = CreateItem(null, excelData);
        _elementValues.ElementItemValues.Add(item.ElementItemValue);
    }

    private ElementItem CreateItem(ElementItemValue? itemValue, ExcelData excelData)
    {
        Items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        int newIndex = Items.RowDefinitions.Count;

        itemValue ??= new(newIndex, excelData);

        ElementItem newItem = new(itemValue, ItemValueChanged, GetTotalPrice);

        Grid.SetRow(newItem, newIndex - 1);

        Items.Children.Add(newItem);

        return newItem;
    }

    private void ItemValueChanged(object? sender, string newValue)
    {
        if (sender is ElementItem item)
        {
            ElementItemValue? itemValue = _elementValues.ElementItemValues.FirstOrDefault(i => i.Number == item.ElementItemValue.Number);
            if (itemValue == null) return;

            itemValue.Length = item.ElementItemValue.Length;
            itemValue.Units = item.ElementItemValue.Units;
        }

        if (newValue != stringValue)
            SomeValuesChanged(sender, newValue);
    }

    private void SomeValuesChanged(object? sender, string newValue)
    {
        double totalPartPirce = CalculateTotalPartPrice();

        BottomPart.CalculateEverything(totalPartPirce);
        FinalResults.CalculateResults(TotalHoursPay.Value, BottomPart.MaterialCostsIncrease.Value, TotalHoursProfit.Value, TotalPartProfit.Value, ProjectValue);

        Normal.CalculateHoursContributions();
        Student.CalculateHoursContributions();
        AdultStudent.CalculateHoursContributions();

        CalculatePertContributions();

        double StoppageTimeContributions = DoubleCal.Round(HourlyPayStoppageTimePrice.Value * GetTotalPrice() / 100);
        HourlyPayStoppageTimeContributions.Value = StoppageTimeContributions;
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

    private void CalculatePertContributions()
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
        double workTime = normalHours + studentHours + adultStudentHours;
        double additionalWorkTime = DoubleCal.Round(workTime * hourlyPayStoppageTimePercentage);

        double normalPrice = Normal.Sale.Value;
        double studentPrice = Student.Sale.Value;
        double adultStudentPrice = AdultStudent.Sale.Value;

        double normalAdditionalPay = DoubleCal.Round(normalPrice * normalHours * hourlyPayStoppageTimePercentage);
        double studentAdditionalPay = DoubleCal.Round(studentPrice * studentHours * hourlyPayStoppageTimePercentage);
        double adultStudentAdditionalPay = DoubleCal.Round(adultStudentPrice * adultStudentHours * hourlyPayStoppageTimePercentage);

        double additionalPay = normalAdditionalPay + studentAdditionalPay + adultStudentAdditionalPay;

        double normalProfit = DoubleCal.Round(Normal.CompanyProfit.Value * hourlyPayStoppageTimePercentage);
        double studentProfit = DoubleCal.Round(Student.CompanyProfit.Value * hourlyPayStoppageTimePercentage);
        double adultStudentProfit = DoubleCal.Round(AdultStudent.CompanyProfit.Value * hourlyPayStoppageTimePercentage);

        double additionalProfit = normalProfit + studentProfit + adultStudentProfit;

        HourlyPayStoppageTime.Value = additionalWorkTime;
        HourlyPayStoppageTimePrice.Value = additionalPay;
        HourlyPayStoppageTimeProfit.Value = additionalProfit;
    }
}
