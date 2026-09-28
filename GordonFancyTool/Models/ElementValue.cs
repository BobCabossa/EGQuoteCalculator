namespace GordonFancyTool.Models;

public class ElementValue
{
    public int Id { get; set; } = -1;
    public string Title { get; set; } = string.Empty;
    public double NormalHours { get; set; }
    public double StudentHours { get; set; }
    public double AdultStudentHours { get; set; }

    public List<ElementItemValue> ElementItemValues { get; set; } = [];
}
