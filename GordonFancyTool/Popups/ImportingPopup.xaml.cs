namespace GordonFancyTool.Popups;

public partial class ImportingPopup : Window
{
    public static readonly DependencyProperty ActionsNeededProperty =
        DependencyProperty.Register(
            nameof(ActionsNeeded),
            typeof(int),
            typeof(ImportingPopup));

    public static readonly DependencyProperty ActionsDoneProperty =
        DependencyProperty.Register(
            nameof(ActionsDone),
            typeof(int),
            typeof(ImportingPopup));

    public int ActionsNeeded
    {
        set => SetValue(ActionsNeededProperty, value);
        get => (int)GetValue(ActionsNeededProperty);
    }

    public int ActionsDone
    {
        set
        {
            SetValue(ActionsDoneProperty, value);
            CheckIfDone(value);
        }
        get => (int)GetValue(ActionsDoneProperty);
    }

    public ImportingPopup()
    {
        InitializeComponent();

        ImportController.StartImporting(this);
    }

    private void CheckIfDone(int newValue)
    {
        if (newValue != ActionsNeeded)
            return;

        OkButton.IsEnabled = true;
        DoneText.Visibility = Visibility.Visible;
    }

    private void ClickOk(object sender, string e)
    {
        DialogResult = true;
    }

    private void ClickCancel(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        ImportController.StopWork();
    }
}
