namespace GordonFancyTool.Pages;

/// <summary>
/// Interaction logic for ProjectPage.xaml
/// </summary>
public partial class ProjectPage : Page
{
    public ProjectPage()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ElementPage());
    }
}
