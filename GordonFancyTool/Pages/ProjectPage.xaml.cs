namespace GordonFancyTool.Pages;

public partial class ProjectPage : Page
{
    private readonly ProjectValues _projectValues = new();
    private readonly List<ElementValue> elementValues;

    public ProjectPage()
    {
        elementValues = FileController.LoadElements();
        elementValues.Add(new()
        {
            Id = 0,
            Title = "Something i want to do?",
            NormalHours = 1,
            StudentHours = 2,
            AdultStudentHours = 3,
            ElementItemValues = [
                new(1, new("Flit", "12", "ean123"))
                {
                    Length = 1,
                    Units = 1,
                },
                new(2, new("MC", "888", "ean31123"))
                {
                    Length = 11,
                    Units= 10,
                }
            ]
        });
        FileController.SaveElements(elementValues);
        InitializeComponent();
    }

    public ProjectPage(ElementValue updatedValues)
    {
        elementValues = FileController.LoadElements();
        int index = elementValues.FindIndex(e => e.Id == updatedValues.Id);
        if (index > -1)
        {
            elementValues[index] = updatedValues;
            FileController.SaveElements(elementValues);
        }

        InitializeComponent();
    }

    private void AddExcelData(object sender, RoutedEventArgs e)
    {
        ExcelExtractor.OverrideExcelData();
    }

    private void OpenElementView(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ElementPage(_projectValues, elementValues.Last()));
    }
}
