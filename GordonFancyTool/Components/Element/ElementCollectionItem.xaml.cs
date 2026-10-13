using static GordonFancyTool.Pages.ElementPage;

namespace GordonFancyTool.Components;

public partial class ElementCollectionItem : UserControl
{
    public ElementItemValue ElementItemValue { get; set; }
    public ExcelCollection ExcelCollection { get; set; }

    public event EventHandler<ElementItemValue> DeleteItem;
    public event EventHandler<string> ValuesChanged;
    public Func<double> GetFullPrice;

    public ElementCollectionItem(ElementItemValue itemValue, ExcelCollection collection, ElementPageLists elementPageLists, EventHandler<string> valuesChanged, EventHandler<ElementItemValue> deleteItem, Func<double> getFullPrice)
    {
        ElementItemValue = itemValue;
        ExcelCollection = collection;

        ValuesChanged = valuesChanged;
        DeleteItem = deleteItem;
        GetFullPrice = getFullPrice;

        InitializeComponent();

        QuantityChangeField.ValueString = itemValue.CollectionQuantity.ToString();

        CreateItems(elementPageLists);
    }

    public ElementCollectionItem(ElementItemValue itemValue, ExcelCollection collection, ElementPageLists elementPageLists, Func<double> getFullPrice)
    {
        ElementItemValue = itemValue;
        ExcelCollection = collection;

        DeleteItem = null!;
        ValuesChanged = null!;
        GetFullPrice = getFullPrice;

        InitializeComponent();

        RemoveButton.IsEnabled = false;
        RemoveButton.Visibility = Visibility.Collapsed;

        QuantityChange.IsEnabled = false;
        QuantityChange.Visibility = Visibility.Collapsed;

        Prices.Visibility = Visibility.Collapsed;

        CreateItems(elementPageLists);
    }

    private void CreateItems(ElementPageLists elementPageLists)
    {
        foreach (var item in ExcelCollection.Items)
        {
            if (item.ItemType == ExcelCollectionItem.CollectionItemType.Single)
            {
                ExcelData? excelData = elementPageLists.ExcelDatas.FirstOrDefault(e => e.Name == item.Name);
                if (excelData == null)
                    continue;

                ElementCollectionItemPart newItemPart = new(item, excelData, GetFullPrice);
                Items.Children.Add(newItemPart);
                continue;
            }
            else if (item.ItemType == ExcelCollectionItem.CollectionItemType.Collection)
            {
                ExcelCollection? excelCollection = elementPageLists.ExcelCollections.FirstOrDefault(e => e.Name == item.Name);
                if (excelCollection == null)
                    continue;

                ElementCollectionItem newItemCollection = new(ElementItemValue, excelCollection, elementPageLists, GetFullPrice);
                Items.Children.Add(newItemCollection);
            }
        }
    }

    public void PriceUpdated()
    {
        (double fullPrice, double companyProfit, double totalMinToInstall) = GetPricesForItems();

        TotalMinToInstall.Value = DoubleCal.Round(totalMinToInstall);
        FullPrice.Value = DoubleCal.Round(fullPrice);
        CompanyProfit.Value = DoubleCal.Round(companyProfit);
    }

    public void UpdateItemsQuantityAndPrices(double newQuantity)
    {
        foreach (var item in Items.Children)
        {
            if (item is ElementCollectionItem collectionItem)
            {
                collectionItem.UpdateItemsQuantityAndPrices(newQuantity);
            }
            else if (item is ElementCollectionItemPart part)
            {
                part.UpdatePrices(newQuantity);
            }
        }
    }

    public void QuantityChanged(object sender, string e)
    {
        if (sender is not DoubleTextBox quantityChangeField)
            return;
        
        double newQuantity = quantityChangeField.Value ?? 0;
        if (QuantityChangeField.Value != null)
        {
            ElementItemValue.CollectionQuantity = newQuantity;
        }

        UpdateItemsQuantityAndPrices(newQuantity);
        PriceUpdated();
        ValuesChanged.Invoke(this, e);
    }

    private (double fullPrice, double companyProfit, double totalMinToInstall) GetPricesForItems()
    {
        double fullPrice = 0;
        double companyProfit = 0;
        double totalMinToInstall = 0;
        foreach (var item in Items.Children)
        {
            if (item is ElementCollectionItem collectionItem)
            {
                (double addFullPrice, double addCompanyProfit, double addTotalMinToInstall) = collectionItem.GetPricesForItems();
                fullPrice += addFullPrice;
                companyProfit += addCompanyProfit;
                totalMinToInstall += addTotalMinToInstall;
            }
            else if (item is ElementCollectionItemPart part)
            {
                fullPrice += part.FullPrice.Value;
                companyProfit += part.CompanyProfit.Value;
                totalMinToInstall += part.ExcelData.MinToInstall * ElementItemValue.CollectionQuantity;
            }
        }

        return (fullPrice, companyProfit, totalMinToInstall);
    }

    public void CalculateProcentageOfOfferOfChilrens()
    {
        foreach (var item in Items.Children)
        {
            if (item is ElementCollectionItem collectionItem)
            {
                collectionItem.CalculateProcentageOfOfferOfChilrens();
                collectionItem.CalculateProcentageOfOffer();
            }
            else if (item is ElementCollectionItemPart part)
            {
                part.CalculateProcentageOfOffer();
            }
        }
    }

    public void CalculateProcentageOfOffer()
    {
        double totalFullPrice = GetFullPrice.Invoke();
        double percentageOfOffers = DoubleCal.Round(FullPrice.Value / totalFullPrice * 100);

        PercentageOfOffers.Value = percentageOfOffers;
    }

    private void RemoveClick(object sender, RoutedEventArgs e)
    {
        DeleteItem.Invoke(this, ElementItemValue);
    }
}
