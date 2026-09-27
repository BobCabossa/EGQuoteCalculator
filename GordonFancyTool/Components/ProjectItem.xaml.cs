namespace GordonFancyTool.Components;

public partial class ProjectItem : UserControl
{
    public readonly ElementValue ElementValue;
    private readonly EventHandler<string> OnOpenElement;

    public ProjectItem(ElementValue elementValue, EventHandler<string> OnOpenElement)
    {
        ElementValue = elementValue;
        this.OnOpenElement = OnOpenElement;
        InitializeComponent();
        Title.Text = elementValue.Title;
    }

    private void OpenElementView(object sender, RoutedEventArgs e)
    {
        OnOpenElement?.Invoke(this, "");
    }
}
