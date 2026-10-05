namespace GordonFancyTool.Components;

public partial class CollectionItemPart : UserControl
{
    public ExcelData _data { get; }
    public ExcelCollectionItem CollectionItem;

    public event EventHandler<string> _deletePart;

    public CollectionItemPart(ExcelData data, ExcelCollectionItem collectionItem, EventHandler<string> deletePart)
    {
        CollectionItem = collectionItem;
        _deletePart = deletePart;
        _data = data;
        InitializeComponent();

        ValueBox.ValueString = collectionItem.Quantity.ToString();
    }

    public void ValueUpdated(object sender, string e)
    {
        CollectionItem.Quantity = ValueBox.Value ?? 0;
    }

    private void RemoveClick(object sender, RoutedEventArgs e)
    {
        _deletePart.Invoke(this, Name);
    }
}
