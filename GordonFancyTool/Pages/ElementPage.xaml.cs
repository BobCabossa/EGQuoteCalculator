namespace GordonFancyTool.Pages;

public partial class ElementPage : Page
{
    public ObservableCollection<ExcelData> ExcelDatas { get; } = new()
    {
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
        new ExcelData("Hej", "10", "101"),
        new ExcelData("Farvel", "20", "102"),
        new ExcelData("Hej med dig", "30", "103"),
        new ExcelData("Goodbye farvel hej", "40", "104")
    };

    public ElementPage()
    {
        InitializeComponent();
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
        TotalHours = hours.ToString();

        // Total pay the company gets for their man power.
        string normalSaleText = Normal.Sale.Text;
        string studentSaleText = Student.Sale.Text;

        _ = double.TryParse(normalSaleText, out double normalSale);
        _ = double.TryParse(studentSaleText, out double studentSale);
        double totalSale = DoubleCal.Round(normalSale + studentSale);

        TotalHoursPay = totalSale.ToString();
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
        double totalPartPercentage = 0;
        double totalPartProfit = 0;
        foreach (var item in Items.Children)
        {
            if (item is not ElementItem itemValue)
                continue;

            _ = double.TryParse(itemValue.FullPrice, out double priceForPart);
            _ = double.TryParse(itemValue.PercentageOfOffers, out double percentgeOfOffer);
            _ = double.TryParse(itemValue.CompanyProfit, out double companyProfit);

            totalPartPirce += priceForPart;
            totalPartPercentage += percentgeOfOffer;
            totalPartProfit += companyProfit;
        }

        TotalPartPirce = totalPartPirce.ToString();
        TotalPartPercentage = totalPartPercentage.ToString();
        TotalPartProfit = totalPartProfit.ToString();

        Normal.CalculateHoursPercentage();
        Student.CalculateHoursPercentage();
        AdultStudent.CalculateHoursPercentage();
        BottomPart.CalculateEverything();
        foreach (var item in Items.Children)
        {
            if (item is not ElementItem itemValue)
                continue;

            itemValue.CalculateProcentageOfOffer();
        }
    }
}
