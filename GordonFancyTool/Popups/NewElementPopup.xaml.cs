namespace GordonFancyTool.Popups;

public partial class NewElementPopup : Window
{
    public NewElementPopup()
    {
        InitializeComponent();
    }

    public string? Result { get; private set; } = "";

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        Result = InputTextBox.Text;
        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Result = null;
        DialogResult = false;
    }
}
