namespace GordonFancyTool.Components;

public partial class ButtonRemove : UserControl
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ButtonRemove),
                new PropertyMetadata(
                    "Fjern"));

    public string Text
    {
        set => SetValue(TextProperty, value);
        get => (string)GetValue(TextProperty);
    }

    public event EventHandler<string>? BtnClicked;

    public ButtonRemove()
    {
        InitializeComponent();
    }

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        BtnClicked?.Invoke(sender, Uid);
    }
}
