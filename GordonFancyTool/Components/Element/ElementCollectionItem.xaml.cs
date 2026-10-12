using static GordonFancyTool.Pages.ElementPage;

namespace GordonFancyTool.Components;

public partial class ElementCollectionItem : UserControl
{
    public ElementItemValue ElementItemValue { get; set; }
    public ExcelCollection ExcelCollection { get; set; }

    public event EventHandler<ElementItemValue> DeleteItem;

    public ElementCollectionItem(ElementItemValue itemValue, ExcelCollection collection, ElementPageLists elementPageLists, EventHandler<ElementItemValue> deleteItem)
    {
        ElementItemValue = itemValue;
        ExcelCollection = collection;

        DeleteItem = deleteItem;

        InitializeComponent();

        CreateItems(elementPageLists);
    }

    public ElementCollectionItem(ExcelCollection collection, ElementPageLists elementPageLists)
    {
        ElementItemValue = null!;
        ExcelCollection = collection;

        DeleteItem = null!;

        InitializeComponent();

        RemoveButton.IsEnabled = false;
        RemoveButton.Visibility = Visibility.Collapsed;

        QuantityChange.IsEnabled = false;
        QuantityChange.Visibility = Visibility.Collapsed;

        CreateItems(elementPageLists);
    }

    private void CreateItems(ElementPageLists elementPageLists)
    {
        foreach (var item in ExcelCollection.Items)
        {
            if (item.ItemType == ExcelCollectionItem.CollectionItemType.Single)
            {
                var newItemPart = new ElementCollectionItemPart();
                Items.Children.Add(newItemPart);
                continue;
            }
            else if (item.ItemType == ExcelCollectionItem.CollectionItemType.None)
            {
                continue;
            }

            ExcelCollection? excelCollection = elementPageLists.ExcelCollections.FirstOrDefault(e => e.Name == item.Name);
            if (excelCollection == null)
                continue;

            var newItemCollection = new ElementCollectionItem(excelCollection, elementPageLists);

            Items.Children.Add(newItemCollection);
        }
    }

    public void PriceUpdated(object sender, string e)
    {
        // update prices! (maybe this doesn't have any and only the children has.)
    }

    private void RemoveClick(object sender, RoutedEventArgs e)
    {
        DeleteItem.Invoke(this, ElementItemValue);
    }
}
