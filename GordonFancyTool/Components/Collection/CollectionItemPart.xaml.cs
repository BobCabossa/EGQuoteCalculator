namespace GordonFancyTool.Components;

public partial class CollectionItemPart : UserControl
{
    public ExcelData Data { get; }
    public ExcelCollectionItem CollectionItem;

    public event EventHandler<string> DeletePart;

    private bool _loading = true;

    public CollectionItemPart(ExcelData data, ExcelCollectionItem collectionItem, EventHandler<string> deletePart)
    {
        CollectionItem = collectionItem;
        DeletePart = deletePart;
        Data = data;
        InitializeComponent();

        QuantityValueBox.ValueString = collectionItem.Quantity.ToString();
        LengthValueBox.ValueString = collectionItem.Length.ToString();

        _loading = false;
    }

    public void ValueUpdated(object sender, string e)
    {
        if (_loading) return;

        CollectionItem.Quantity = QuantityValueBox.Value ?? 0;
        CollectionItem.Length = LengthValueBox.Value ?? 0;
    }

    private void RemoveClick(object sender, string e)
    {
        DeletePart.Invoke(this, Name);
    }
}
