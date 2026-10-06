namespace GordonFancyTool.Pages;

public partial class ProjectPage : Page
{
    private bool pageLoaded = false;

    private readonly ProjectValues _projectValues;
    private readonly List<ElementValue> _elementValues;

    public ProjectPage()
    {
        _projectValues = FileController.LoadProjectValues();
        _elementValues = FileController.LoadElements();
        InitializeComponent();
    }

    public ProjectPage(ElementValue updatedValues)
    {
        _projectValues = FileController.LoadProjectValues();
        _elementValues = FileController.LoadElements();
        int index = _elementValues.FindIndex(e => e.Id == updatedValues.Id);
        if (index > -1)
        {
            _elementValues[index] = updatedValues;
            FileController.SaveElements(_elementValues);
        }

        InitializeComponent();
    }

    private void PageLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var item in _elementValues)
        {
            CreateElementItem(item);
        }
        pageLoaded = true;
    }

    private void ImportData(object sender, RoutedEventArgs e)
    {
        ImportController.StartImporting();
    }

    private void OpenElementView(object? sender, string newValue)
    {
        if (sender is not ProjectItem item) return;

        NavigationService.Navigate(new ElementPage(_projectValues, item.ElementValue));
    }

    private void AddElement(object sender, RoutedEventArgs e)
    {
        string? newTitle = GetNewElementTitle();
        if (newTitle == null) return;

        ElementValue elementValue = new()
        {
            Id = _elementValues.Last().Id + 1,
            Title = newTitle,
        };

        CreateElementItem(elementValue);
        _elementValues.Add(elementValue);
        FileController.SaveElements(_elementValues);
    }

    private void CreateElementItem(ElementValue newElement)
    {
        ProjectItem newItem = new(newElement, OpenElementView, DeleteElement);
        items.Children.Add(newItem);

        if (pageLoaded)
            FileController.SaveElements(_elementValues);
    }

    private string? GetNewElementTitle()
    {
        var dialog = new NewElementPopup("Ny Opgrave", "Indtast et navn:")
        {
            Owner = Window.GetWindow(this)
        };

        if (dialog.ShowDialog() == true)
        {
            return dialog.Result;
        }

        return null;
    }

    private void OpenEditProjectValues(object sender, RoutedEventArgs e)
    {
        var dialog = new ChangeProjectValuesPopup(_projectValues)
        {
            Owner = Window.GetWindow(this)
        };

        if (dialog.ShowDialog() == true)
        {
            FileController.SaveProjectSettings(dialog.ProjectValues);
        }
    }

    private void OpenEditTimeOnExcel(object sender, RoutedEventArgs e)
    {
        var dialog = new ExcelTimeChangePopup()
        {
            Owner = Window.GetWindow(this)
        };

        if (dialog.ShowDialog() == true)
        {
            List<ExcelData> data = dialog.Items.Select(i => i.ExcelData).ToList();

            FileController.SaveExcelData(data);
        }
    }

    private void OpenCreateCollections(object sender, RoutedEventArgs e)
    {
        var dialog = new CreateCollectionsPopup()
        {
            Owner = Window.GetWindow(this)
        };

        if (dialog.ShowDialog() == true)
        {
            List<ExcelCollection> collections = dialog.ExcelCollections.Select(ec => ec.ExcelCollection).ToList();

            FileController.SaveCollections(collections);
        }
    }

    private void DeleteElement(object? sender, ElementValue elementValue)
    {
        int index = _elementValues.IndexOf(elementValue);
        if (index == -1)
            return;

        _elementValues.RemoveAt(index);
        items.Children.RemoveAt(index);
        FileController.SaveElements(_elementValues);
    }
}
