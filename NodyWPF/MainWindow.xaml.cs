namespace NodyWPF;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new EditorViewModel(MainEditor);
    }
}