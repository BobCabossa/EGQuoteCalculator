namespace GordonFancyTool.Pages;

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
