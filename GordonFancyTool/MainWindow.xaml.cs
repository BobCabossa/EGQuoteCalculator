namespace GordonFancyTool;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new ElementPage());
    }
}