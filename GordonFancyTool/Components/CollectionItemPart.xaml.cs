namespace GordonFancyTool.Components;

public partial class CollectionItemPart : UserControl
{
    public ExcelData Data { get; }
    public ExcelCollectionItem CollectionItem;

    public event EventHandler<string> DeletePart;

    public CollectionItemPart(ExcelData data, ExcelCollectionItem collectionItem, EventHandler<string> deletePart)
    {
        CollectionItem = collectionItem;
        DeletePart = deletePart;
        Data = data;
        InitializeComponent();

        ValueBox.ValueString = collectionItem.Quantity.ToString();
    }

    public void ValueUpdated(object sender, string e)
    {
        CollectionItem.Quantity = ValueBox.Value ?? 0;
    }

    private void RemoveClick(object sender, RoutedEventArgs e)
    {
        DeletePart.Invoke(this, Name);
    }
}
