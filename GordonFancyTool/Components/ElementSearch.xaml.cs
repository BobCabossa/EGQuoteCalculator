namespace GordonFancyTool.Components;

public partial class ElementSearch : UserControl
{    
    public ElementSearch()
    {
        InitializeComponent();
    }

    public ObservableCollection<ExcelData> FilteredItems { get; } = new();

    public ObservableCollection<ExcelData>? SearchItems
    {
        get => (ObservableCollection<ExcelData>?)GetValue(SearchItemsProperty);
        set => SetValue(SearchItemsProperty, value);
    }

    public static readonly DependencyProperty SearchItemsProperty =
        DependencyProperty.Register(
            nameof(SearchItems),
            typeof(ObservableCollection<ExcelData>),
            typeof(ElementSearch));

    public string SearchText
    {
        get => (string?)GetValue(SearchTextProperty) ?? string.Empty;
        set => SetValue(SearchTextProperty, value);
    }

    public static readonly DependencyProperty SearchTextProperty =
        DependencyProperty.Register(
            nameof(SearchText),
            typeof(string),
            typeof(ElementSearch),
            new PropertyMetadata(string.Empty));

    public bool IsDropDownOpen
    {
        get => (bool)GetValue(IsDropDownOpenProperty);
        set => SetValue(IsDropDownOpenProperty, value);
    }

    public static readonly DependencyProperty IsDropDownOpenProperty =
        DependencyProperty.Register(
            nameof(IsDropDownOpen),
            typeof(bool),
            typeof(ElementSearch),
            new PropertyMetadata(false));

    public event EventHandler<ExcelData>? ValueSelected;

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        FilterItems();
    }

    private void FilterItems()
    {
        FilteredItems.Clear();

        if (SearchItems == null)
            return;

        string search = SearchText?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(search))
        {
            IsDropDownOpen = false;
            return;
        }

        List<ExcelData> filteredItems = [];
        foreach (var item in SearchItems)
        {
            if (item.ProductName.Contains(search, StringComparison.OrdinalIgnoreCase))
            {
                filteredItems.Add(item);
            }
        }

        if (filteredItems.Count > 0)
        {
            IOrderedEnumerable<ExcelData> sorted = filteredItems.OrderByDescending(x => x.PurchasePrice);
            foreach (var item in sorted)
            {
                FilteredItems.Add(item);
            }

            IsDropDownOpen = true;
            return;
        }

        IsDropDownOpen = false;
    }
    private void ResultsList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (ResultsList.SelectedItem is ExcelData item)
        {
            SelectItem(item);
        }
    }

    private void ResultsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ResultsList.SelectedItem is ExcelData item)
        {
            SelectItem(item);
        }
    }

    private void SelectItem(ExcelData item)
    {
        if (item == null)
            return;

        ValueSelected?.Invoke(this, item);
        SearchText = "";

        IsDropDownOpen = false;

        SearchTextBox.Focus();
        SearchTextBox.CaretIndex = SearchTextBox.Text.Length;
    }
}
