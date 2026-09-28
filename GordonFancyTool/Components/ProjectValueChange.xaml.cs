namespace GordonFancyTool.Components;
#pragma warning disable IDE0060 // Remove messages about unused parmenter. As they come from public api's, i can't remove them.

public partial class ProjectValueChange : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(ProjectValueChange));

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(double),
            typeof(ProjectValueChange));

    public string Title
    {
        set => SetValue(TitleProperty, value);
        get => (string)GetValue(TitleProperty);
    }

    public double Value
    {
        set => SetValue(ValueProperty, value);
        get => (double)GetValue(ValueProperty);
    }

    public ProjectValueChange()
    {
        InitializeComponent();
    }

    private void ComponentLoaded(object sender, RoutedEventArgs e)
    {
        Dispatcher.BeginInvoke(() =>
        {
            ValueBox.Value = Value;
        });
    }

    public void ValueUpdated(object sender, string e)
    {
        Value = ValueBox.Value ?? 0;
    }
}
