namespace GordonFancyTool.Pages;

public partial class ProjectPage : Page
{
    private readonly ProjectValues _projectValues;
    private readonly List<ElementValue> elementValues;

    public ProjectPage()
    {
        _projectValues = FileController.LoadProjectValues();
        elementValues = FileController.LoadElements();
        InitializeComponent();
    }

    public ProjectPage(ElementValue updatedValues)
    {
        _projectValues = FileController.LoadProjectValues();
        elementValues = FileController.LoadElements();
        int index = elementValues.FindIndex(e => e.Id == updatedValues.Id);
        if (index > -1)
        {
            elementValues[index] = updatedValues;
            FileController.SaveElements(elementValues);
        }

        InitializeComponent();
    }

    private void PageLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var item in elementValues)
        {
            Window.GetWindow(this).Title = "Projekter";
            CreateElementItem(item);
        }
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
            Id = items.RowDefinitions.Count,
            Title = newTitle,
        };

        CreateElementItem(elementValue);
        elementValues.Add(elementValue);
        FileController.SaveElements(elementValues);
    }

    private void CreateElementItem(ElementValue newElement)
    {
        items.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        int newIndex = items.RowDefinitions.Count - 1;

        ProjectItem newItem = new(newElement, OpenElementView);

        Grid.SetRow(newItem, newIndex - 1);
        Grid.SetRow(AddElementButton, newIndex);

        items.Children.Add(newItem);

        FileController.SaveElements(elementValues);
    }

    private string? GetNewElementTitle()
    {
        var dialog = new NewElementPopup()
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
}
