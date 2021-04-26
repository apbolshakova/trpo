using System.Windows;
using System.Windows.Controls;

namespace trpo_lw6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new AllRouteViewModel();
        }

        private void HeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            AddBtn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            DeleteBtn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
            SaveBtn.IsEnabled = Validation.GetHasError(tb) == true ? false : true;
        }
    }
}