namespace GordonFancyTool.Popups;

public partial class NewElementPopup : Window
{
    public static readonly DependencyProperty DescriptionProperty =
    DependencyProperty.Register(
        nameof(Description),
        typeof(string),
        typeof(NewElementPopup));

    public string Description
    {
        set => SetValue(DescriptionProperty, value);
        get => (string)GetValue(DescriptionProperty);
    }

    public string? Result { get; private set; } = "";


    public NewElementPopup(string title, string description, string result = "")
    {
        InitializeComponent();

        Title = title;
        Description = description;
        InputTextBox.Text = result;
    }

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
