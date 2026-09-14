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

    private ExcelData? _selectedExcelData;

    public ExcelData? SelectedExcelData
    {
        get => _selectedExcelData;
        set
        {
            _selectedExcelData = value;

            // Do whatever you want here when the user selects something.
            if (value != null)
            {
                // Example:
                MessageBox.Show(value.ProductName);
                AddItem(value);
            }
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

    private void AddItem(ExcelData excelData)
    {
        Items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        int newIndex = Items.RowDefinitions.Count;

        ElementItem newItem = new(excelData)
        {
            Index = newIndex.ToString()
        };

        Grid.SetRow(newItem, newIndex);

        Items.Children.Add(newItem);
    }
}
