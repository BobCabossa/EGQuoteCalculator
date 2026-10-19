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

    public void RemoveUnknown(object sender, string e)
    {
        OnItemDelete.Invoke(sender, UnknownItemName.Text);
    }
}
