namespace GordonFancyTool.Components;

public partial class ProjectItem : UserControl
{
    public readonly ElementValue ElementValue;
    private readonly EventHandler<string> OnOpenElement;
    private readonly EventHandler<ElementValue> OnDeleteItem;

    public ProjectItem(ElementValue elementValue, EventHandler<string> OnOpenElement, EventHandler<ElementValue> onDeleteItem)
    {
        ElementValue = elementValue;
        this.OnOpenElement = OnOpenElement;
        InitializeComponent();
        Title.Text = elementValue.Title;
        OnDeleteItem = onDeleteItem;
    }

    private void OpenElementView(object sender, RoutedEventArgs e)
    {
        OnOpenElement?.Invoke(this, "");
    }

    private void DeleteItem(object sender, RoutedEventArgs e)
    {
        OnDeleteItem.Invoke(sender, ElementValue);
    }
}
