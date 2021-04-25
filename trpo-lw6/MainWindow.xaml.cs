using System.Windows;

namespace trpo_lw6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new AllRouteViewModel();
        }
    }
}