namespace GordonFancyTool.Components;

public partial class ButtonEdit : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ButtonEdit),
            new PropertyMetadata(
                "Endre"));

    public string Text
    {
        set => SetValue(TextProperty, value);
        get => (string)GetValue(TextProperty);
    }

    public event EventHandler<string>? BtnClicked;

    public ButtonEdit()
    {
        InitializeComponent();
    }

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        BtnClicked?.Invoke(sender, Uid);
    }
}
