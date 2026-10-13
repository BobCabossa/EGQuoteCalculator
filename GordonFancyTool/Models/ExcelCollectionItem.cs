namespace GordonFancyTool.Models;

public class ExcelCollectionItem
{
    public enum CollectionItemType
    {
        None,
        Single,
        Collection,
    }

    public string Name { get; set; } = string.Empty;
    public double Quantity { get; set; } = 0;
    public double Length { get; set; } = 0;
    public CollectionItemType ItemType { get; set; } = CollectionItemType.None;

    public ExcelCollectionItem() { }

    public ExcelCollectionItem(SearchModel model)
    {
        Name = model.Name;
        
        if (model is ExcelData)
        {
            ItemType = CollectionItemType.Single;
        }
        else if (model is ExcelCollection)
        {
            ItemType = CollectionItemType.Collection;
        }
    }
}
