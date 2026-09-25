namespace GordonFancyTool.Pages;

public partial class ElementPage : Page
{
    public ProjectValues projectValues { get; set; }
    public ObservableCollection<ExcelData> ExcelDatas { get; } = new()
    {
        new ExcelData("Hej", "10", "10000"),
        new ExcelData("Farvel", "20", "10001"),
        new ExcelData("Goodbye farvel hej", "40", "10002"),
        new ExcelData("Hej med dig", "30", "10003"),
        new ExcelData("Hej", "10", "101"),
        new ExcelData("Farvel", "20", "102"),
        new ExcelData("Hej med dig", "30", "103"),
        new ExcelData("Hej", "10", "101"),
        new ExcelData("Farvel", "20", "102"),
        new ExcelData("Hej med dig", "30", "103"),
        new ExcelData("Hej", "10", "101"),
        new ExcelData("Farvel", "20", "102"),
        new ExcelData("Hej med dig", "30", "103"),
        new ExcelData("Hej", "10", "101"),
        new ExcelData("Farvel", "20", "102"),
        new ExcelData("Hej med dig", "30", "103"),
        new ExcelData("Hej", "10", "101"),
        new ExcelData("Farvel", "20", "102"),
        new ExcelData("Hej med dig", "30", "103"),
        new ExcelData("Hej", "10", "101"),
        new ExcelData("Farvel", "20", "102"),
        new ExcelData("Hej med dig", "30", "103"),
        new ExcelData("Hej", "10", "101"),
        new ExcelData("Farvel", "20", "102"),
        new ExcelData("Hej med dig", "30", "103"),
    };

    public ElementPage(ProjectValues projectValues)
    {
        this.projectValues = projectValues;
        InitializeComponent();
        FinalResults.VATValue.Text = projectValues.VAT.ToString();

        Normal.GetFullPrice = GetTotalPrice;
        Student.GetFullPrice = GetTotalPrice;
        AdultStudent.GetFullPrice = GetTotalPrice;

        Random random = new();
        foreach (var item in ExcelDatas.Skip(4))
        {
            item.PurchasePrice = random.Next(1, 5000).ToString();
            item.EAN = random.Next(10000, 99999).ToString();
        }
    }

    public double GetTotalPrice() => FinalResults.TotalPriceExclusiveVAT.Value;

    private void PageLoaded(object sender, RoutedEventArgs e)
    {
        Dispatcher.BeginInvoke(() =>
        {
            HoursChanged(sender, "");
        }, DispatcherPriority.Render);
    }

    public void BackToProject(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ProjectPage());
    }

    public void HoursChanged(object sender, string newValue)
    {
        Normal.CalculateHours();
        Student.CalculateHours();
        AdultStudent.CalculateHours();

        // Total hours their workers need to complete the task.
        double normal = Normal.Hours.Value ?? 0;
        double student = Student.Hours.Value ?? 0;
        double adultStudent = AdultStudent.Hours.Value ?? 0;

        double hours = normal + student + adultStudent;

        // Total pay the company gets for their man power.
        double normalSale = Normal.Sale.Value;
        double studentSale = Student.Sale.Value;

        double totalSale = normalSale + studentSale;

        TotalHoursPay.Value = totalSale;
        TotalHours.Value = hours;
        SomeValuesChanged(sender, newValue);
    }

    public void AddItem(object? sender, ExcelData excelData)
    {
        Items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        int newIndex = Items.RowDefinitions.Count;

        ElementItem newItem = new(excelData, newIndex, SomeValuesChanged, GetTotalPrice);

        Grid.SetRow(newItem, newIndex - 1);

        Items.Children.Add(newItem);
    }

    private void SomeValuesChanged(object? sender, string newValue)
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

        TotalPartPirce.Value = totalPartPirce;
        TotalPartProfit.Value = totalPartProfit;

        BottomPart.CalculateEverything(totalPartPirce);
        FinalResults.CalculateResults(TotalHoursPay.Value, BottomPart.MaterialCostsIncrease.Value, projectValues);

        Normal.CalculateHoursContributions();
        Student.CalculateHoursContributions();
        AdultStudent.CalculateHoursContributions();

        double totalPartPercentage = 0;
        foreach (var item in Items.Children)
        {
            if (item is not ElementItem itemValue)
                continue;

            itemValue.CalculateProcentageOfOffer();

            totalPartPercentage += itemValue.PercentageOfOffers.Value;
        }

        TotalPartPercentage.Value = totalPartPercentage;
    }
}
