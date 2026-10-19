namespace GordonFancyTool.Components;

public partial class ButtonConfirm : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ButtonConfirm),
            new PropertyMetadata(
                "Ok"));

    public string Text
    {
        set => SetValue(TextProperty, value);
        get => (string)GetValue(TextProperty);
    }

    public event EventHandler<string>? BtnClicked;

    public ButtonConfirm()
    {
        InitializeComponent();
    }

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        BtnClicked?.Invoke(sender, Uid);
    }
}
