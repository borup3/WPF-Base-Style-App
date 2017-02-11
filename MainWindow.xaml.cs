using WPF_Base_Styling_App.UI;
using WPF_Base_Styling_App.UI.Theme;

namespace WPF_Base_Styling_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ThemeManager.SetColorScheme(new VanillaColorScheme());
        }
    }
}
