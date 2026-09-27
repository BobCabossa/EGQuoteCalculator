namespace GordonFancyTool;

public class ElementValue
{
    public string Title { get; set; } = string.Empty;
    public double NormalHours { get; set; }
    public double StudentHours { get; set; }
    public double AdultStudentHours { get; set; }

    public List<ElementItemValue> ElementItemValues { get; set; } = [];
}
