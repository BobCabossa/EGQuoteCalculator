namespace GordonFancyTool.Pages;

public partial class ElementPage : Page
{
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

    private void AddItem(object sender, RoutedEventArgs e)
    {
        Items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        int newIndex = Items.RowDefinitions.Count - 1;

        ExcelData fakeData = new("Fake item", "146,25", "7321677185634");

        ElementItem newItem = new(fakeData)
        {
            Index = newIndex.ToString()
        };

        Grid.SetRow(newItem, newIndex);

        Items.Children.Add(newItem);
    }
}
