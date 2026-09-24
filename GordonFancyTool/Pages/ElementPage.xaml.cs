namespace GordonFancyTool.Pages;

public partial class ElementPage : Page
{
    private ProjectValues projectValues;
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

        Random random = new();
        foreach (var item in ExcelDatas.Skip(4))
        {
            item.PurchasePrice = random.Next(1, 5000).ToString();
            item.EAN = random.Next(10000, 99999).ToString();
        }
    }

    public static readonly DependencyProperty TotalHoursProperty =
       DependencyProperty.Register(
           nameof(TotalHours),
           typeof(string),
           typeof(ElementPage));

    public static readonly DependencyProperty TotalHoursPayProperty =
       DependencyProperty.Register(
           nameof(TotalHoursPay),
           typeof(string),
           typeof(ElementPage));

    public static readonly DependencyProperty TotalPartPirceProperty =
       DependencyProperty.Register(
           nameof(TotalPartPirce),
           typeof(string),
           typeof(ElementPage));

    public static readonly DependencyProperty TotalPartPercentageProperty =
       DependencyProperty.Register(
           nameof(TotalPartPercentage),
           typeof(string),
           typeof(ElementPage));

    public static readonly DependencyProperty TotalPartProfitProperty =
       DependencyProperty.Register(
           nameof(TotalPartProfit),
           typeof(string),
           typeof(ElementPage));

    public string TotalHours
    {
        set => SetValue(TotalHoursProperty, value);
        get => (string)GetValue(TotalHoursProperty);
    }

    public string TotalHoursPay
    {
        set => SetValue(TotalHoursPayProperty, value);
        get => (string)GetValue(TotalHoursPayProperty);
    }

    public string TotalPartPirce
    {
        set => SetValue(TotalPartPirceProperty, value);
        get => (string)GetValue(TotalPartPirceProperty);
    }

    public string TotalPartPercentage
    {
        set => SetValue(TotalPartPercentageProperty, value);
        get => (string)GetValue(TotalPartPercentageProperty);
    }

    public string TotalPartProfit
    {
        set => SetValue(TotalPartProfitProperty, value);
        get => (string)GetValue(TotalPartProfitProperty);
    }

    public void BackToProject(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ProjectPage());
    }

    public void HoursChanged(object sender, string newValue)
    {
        // Total hours their workers need to complete the task.
        double normal = Normal.Hours.Value ?? 0;
        double student = Student.Hours.Value ?? 0;
        double adultStudent = AdultStudent.Hours.Value ?? 0;
        
        double hours = normal + student + adultStudent;

        // Total pay the company gets for their man power.
        double normalSale = DoubleCal.TryParse(Normal.Sale.Text);
        double studentSale = DoubleCal.TryParse(Student.Sale.Text);

        double totalSale = DoubleCal.Round(normalSale + studentSale);

        TotalHoursPay = totalSale.ToString();
        TotalHours = hours.ToString();
        SomeValuesChanged(sender, newValue);
    }

    public void AddItem(object? sender, ExcelData excelData)
    {
        Items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        int newIndex = Items.RowDefinitions.Count;

        ElementItem newItem = new(excelData, SomeValuesChanged)
        {
            Index = newIndex.ToString(),
        };

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

            totalPartPirce += DoubleCal.TryParse(itemValue.FullPrice);
            totalPartProfit += DoubleCal.TryParse(itemValue.CompanyProfit);
        }

        TotalPartPirce = totalPartPirce.ToString();
        TotalPartProfit = totalPartProfit.ToString();

        BottomPart.CalculateEverything(totalPartPirce);
        FinalResults.CalculateResults(TotalHoursPay, BottomPart.MaterialCostsIncrease, projectValues);

        Normal.CalculateHoursContributions();
        Student.CalculateHoursContributions();
        AdultStudent.CalculateHoursContributions();
        BottomPart.CalculateContributions();

        double totalPartPercentage = 0;
        foreach (var item in Items.Children)
        {
            if (item is not ElementItem itemValue)
                continue;

            itemValue.CalculateProcentageOfOffer();

            totalPartPercentage += DoubleCal.TryParse(itemValue.PercentageOfOffers);
        }

        TotalPartPercentage = totalPartPercentage.ToString();
    }
}
