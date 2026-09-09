namespace GordonFancyTool.Components;

/// <summary>
/// Interaction logic for Test.xaml
/// </summary>
public partial class ElementsTopRow : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(ElementsTopRow));

    public static readonly DependencyProperty Text1Property =
        DependencyProperty.Register(
            nameof(Text1), 
            typeof(string), 
            typeof(ElementsTopRow));

    public static readonly DependencyProperty Text2Property =
        DependencyProperty.Register(
            nameof(Text2), 
            typeof(string), 
            typeof(ElementsTopRow));

    public static readonly DependencyProperty Text3Property =
        DependencyProperty.Register(
            nameof(Text3),
            typeof(string),
            typeof(ElementsTopRow));

    public ElementsTopRow()
    {
        InitializeComponent();
    }

    public string Title
    {
        set => SetValue(TitleProperty, value);
        get => (string)GetValue(TitleProperty);
    }

    public string Text1
    {
        set => SetValue(Text1Property, value);
        get => (string)GetValue(Text1Property);
    }

    public string Text2
    {
        set => SetValue(Text2Property, value);
        get => (string)GetValue(Text2Property);
    }

    public string Text3
    {
        set => SetValue(Text3Property, value);
        get => (string)GetValue(Text3Property);
    }
}
