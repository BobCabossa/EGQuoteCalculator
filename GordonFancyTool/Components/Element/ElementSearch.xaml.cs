namespace GordonFancyTool.Components;

public partial class ElementSearch : UserControl
{
    public ObservableCollection<SearchModel> FilteredItems { get; } = new();

    public ObservableCollection<SearchModel> SearchItems { get; set; } = new();

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

    public event EventHandler<SearchModel>? ValueSelected;

    public ElementSearch()
    {
        ObservableCollection<ExcelData> ExcelDatas = FileController.LoadExcelDataObservableCollection();
        ObservableCollection<ExcelCollection> ExcelCollections = FileController.LoadCollectionObservableCollection();

        foreach (var item in ExcelDatas)
        {
            SearchItems.Add(item);
        }

        foreach (var item in ExcelCollections)
        {
            SearchItems.Add(item);
        }

        InitializeComponent();
    }

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

        List<SearchModel> filteredItems = [];
        foreach (var item in SearchItems)
        {
            if (item.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
            {
                filteredItems.Add(item);
            }
        }

        if (filteredItems.Count > 0)
        {
            List<ExcelData> moreSorting = new();
            foreach (var item in filteredItems)
            {
                if (item is ExcelCollection)
                {
                    FilteredItems.Add(item);
                }
                else if (item is ExcelData excelData)
                {
                    moreSorting.Add(excelData);
                }
            }


            IOrderedEnumerable<SearchModel> sorted = moreSorting.OrderByDescending(x => x.PurchasePrice);

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
