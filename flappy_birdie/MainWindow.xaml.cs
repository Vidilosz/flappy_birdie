using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace flappy_birdie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private List<Rectangle> Obstacles;

		public MainWindow()
        {
            InitializeComponent();
			Obstacles= new List<Rectangle> { Obstacle_1, Obstacle_2, Obstacle_3, Obstacle_4, Obstacle_5, Obstacle_6, Obstacle_7, Obstacle_8, Obstacle_9, Obstacle_10, Obstacle_11, Obstacle_12, Obstacle_13, Obstacle_14 };
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
			double speed = 100;

			foreach (var i in Obstacles)
			{
				double startLeft = Canvas.GetLeft(i);
				double distance = startLeft - 0;

				var o_animation = new DoubleAnimation
				{
					To = 0,
					Duration = TimeSpan.FromSeconds(distance / speed),
					FillBehavior = FillBehavior.HoldEnd
				};

				i.BeginAnimation(Canvas.LeftProperty, o_animation);
			}

			var madar_animation = new DoubleAnimation
			{
				To = 175,
				Duration = TimeSpan.FromSeconds(1),
				EasingFunction = new QuadraticEase
				{
					EasingMode = EasingMode.EaseIn
				}
			};

			madar.BeginAnimation(Canvas.TopProperty, madar_animation);
		}

		private void jump_2_Click(object sender, RoutedEventArgs e)
		{
			double currentTop = Canvas.GetTop(madar);

			var madar_animation = new DoubleAnimation
			{
				To = 175,
				Duration = TimeSpan.FromSeconds(1),
				EasingFunction = new QuadraticEase
				{
					EasingMode = EasingMode.EaseIn
				}
			};

			madar.BeginAnimation(Canvas.TopProperty, null);

			Canvas.SetTop(madar, currentTop - 12);

			madar.BeginAnimation(Canvas.TopProperty, madar_animation);
		}

		private void jump_1_Click_1(object sender, RoutedEventArgs e)
		{
			double currentTop = Canvas.GetTop(madar);

			var madar_animation = new DoubleAnimation
			{
				To = 175,
				Duration = TimeSpan.FromSeconds(1),
				EasingFunction = new QuadraticEase
				{
					EasingMode = EasingMode.EaseIn
				}
			};

			madar.BeginAnimation(Canvas.TopProperty, null);

			Canvas.SetTop(madar, currentTop - 12);

			madar.BeginAnimation(Canvas.TopProperty, madar_animation);
		}
	}
}