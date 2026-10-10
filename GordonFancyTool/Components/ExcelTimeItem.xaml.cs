namespace GordonFancyTool.Components;

public partial class ExcelTimeItem : UserControl
{
    public ExcelData ExcelData { get; set; }

    public ExcelTimeItem(ExcelData excelData)
    {
        ExcelData = excelData;
        InitializeComponent();

        Title.Text = excelData.Name;
        Time.ValueString = excelData.MinToInstall.ToString();
    }

    public void TimeUpdated(object sender, string e)
    {
        ExcelData.MinToInstall = Time.Value ?? 0;
    }
}
