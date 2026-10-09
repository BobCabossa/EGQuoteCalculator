namespace GordonFancyTool.Components;

public partial class ProjectItem : UserControl
{
    public readonly ElementValue ElementValue;
    private readonly EventHandler<string> OnOpenElement;
    private readonly EventHandler<ElementValue> OnEditItem;
    private readonly EventHandler<ElementValue> OnDeleteItem;

    public ProjectItem(ElementValue elementValue, EventHandler<string> onOpenElement, EventHandler<ElementValue> onEditItem, EventHandler<ElementValue> onDeleteItem)
    {
        ElementValue = elementValue;
        OnOpenElement = onOpenElement;
        OnEditItem = onEditItem;
        OnDeleteItem = onDeleteItem;
        
        InitializeComponent();

        Title.Text = elementValue.Title;
        PriceExclusiveVAT.Value = elementValue.TotalPriceExclusiveVAT;
        Profit.Value = elementValue.TotalProfit;
        ContributionMarginRatio.Value = DoubleCal.Round(elementValue.TotalProfit / elementValue.TotalPriceExclusiveVAT * 100);
        NormalStaffingTime.Value = elementValue.NormalHours;
        ApprenticeStaffingTime.Value = DoubleCal.Round(elementValue.StudentHours + elementValue.AdultStudentHours);
    }

    private void OpenElementView(object sender, RoutedEventArgs e)
    {
        OnOpenElement.Invoke(this, "");
    }

    private void EditItem(object sender, RoutedEventArgs e)
    {
        OnEditItem.Invoke(this, ElementValue);
    }

    private void DeleteItem(object sender, RoutedEventArgs e)
    {
        OnDeleteItem.Invoke(sender, ElementValue);
    }
}
