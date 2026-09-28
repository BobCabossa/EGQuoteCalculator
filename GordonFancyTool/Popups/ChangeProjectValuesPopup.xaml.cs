namespace GordonFancyTool.Popups;

public partial class ChangeProjectValuesPopup : Window
{
    public ProjectValues ProjectValues { get; private set; }

    public ChangeProjectValuesPopup(ProjectValues projectValues)
    {
        ProjectValues = projectValues;
        InitializeComponent();
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
