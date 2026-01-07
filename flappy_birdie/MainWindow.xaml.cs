using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace flappy_birdie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            Normal.Visibility = Visibility.Collapsed;
            Rainy_day.Visibility = Visibility.Collapsed;
            TFI.Visibility = Visibility.Collapsed;
        }

		private void Start_Click(object sender, RoutedEventArgs e)
		{
            Start.Visibility = Visibility.Collapsed;
			Normal.Visibility = Visibility.Visible;
			Rainy_day.Visibility = Visibility.Visible;
			TFI.Visibility = Visibility.Visible;

		}

		private void Normal_Click(object sender, RoutedEventArgs e)
		{
			Normal.Visibility = Visibility.Collapsed;
			Rainy_day.Visibility = Visibility.Collapsed;
			TFI.Visibility = Visibility.Collapsed;
			StartGame();
		}

		private void Rainy_day_Click(object sender, RoutedEventArgs e)
		{
			Normal.Visibility = Visibility.Collapsed;
			Rainy_day.Visibility = Visibility.Collapsed;
			TFI.Visibility = Visibility.Collapsed;
			StartGame();
		}

		private void TFI_Click(object sender, RoutedEventArgs e)
		{
			Normal.Visibility = Visibility.Collapsed;
			Rainy_day.Visibility = Visibility.Collapsed;
			TFI.Visibility = Visibility.Collapsed;
			StartGame();
		}

		public void StartGame()
		{

		}
	}
}