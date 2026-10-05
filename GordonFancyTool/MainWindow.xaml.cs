namespace GordonFancyTool;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        // This makes sure that the import folder are present for the user, when they open the app or they delete it.
        _ = FileController.GetImportFolder();

        InitializeComponent();
        MainFrame.Navigate(new ProjectPage());
    }
}