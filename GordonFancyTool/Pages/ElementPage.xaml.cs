namespace GordonFancyTool.Pages;

/// <summary>
/// Interaction logic for ElementPage.xaml
/// </summary>
public partial class ElementPage : Page
{
    public ElementPage()
    {
        InitializeComponent();
    }

    public void BackToProject(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ProjectPage());
    }
}
