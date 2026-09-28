namespace GordonFancyTool.Popups;

public partial class NewElementPopup : Window
{
    public NewElementPopup()
    {
        InitializeComponent();
    }

    public string? Result { get; private set; } = "";

    private void PopupLoaded(object sender, RoutedEventArgs e)
    {
        _ = InputTextBox.Focus();
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        Result = InputTextBox.Text;
        DialogResult = true;
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        Result = null;
        DialogResult = false;
    }
}
