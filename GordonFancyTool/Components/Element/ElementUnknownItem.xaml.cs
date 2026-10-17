namespace GordonFancyTool.Components;

public partial class ElementUnknownItem : UserControl
{
    public event EventHandler<string> OnItemDelete;
    public ElementUnknownItem(string unknownItemName, EventHandler<string> onItemDelete)
    {
        OnItemDelete = onItemDelete;

        InitializeComponent();

        UnknownItemName.Text = unknownItemName;
    }

    private void RemoveUnknown(object sender, RoutedEventArgs e)
    {
        OnItemDelete.Invoke(sender, UnknownItemName.Text);
    }
}
