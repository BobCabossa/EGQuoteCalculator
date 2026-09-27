namespace GordonFancyTool.Pages;

public partial class ProjectPage : Page
{
    private ProjectValues _projectValues = new();
    private List<ElementValue> elementValues = [];

    public ProjectPage()
    {
        InitializeComponent();
        elementValues.Add(new()
        {
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
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ElementPage(_projectValues, elementValues.Last()));
    }
}
