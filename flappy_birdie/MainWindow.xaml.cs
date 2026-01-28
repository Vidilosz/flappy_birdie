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
			double m_speed = 100;
			double o_speed = 50;

			foreach (var i in Obstacles)
			{
				double startLeft = Canvas.GetLeft(i);
				double o_distance = startLeft - 0;

				var o_animation = new DoubleAnimation
				{
					To = 0,
					Duration = TimeSpan.FromSeconds(o_distance / o_speed),
					FillBehavior = FillBehavior.HoldEnd
				};

				i.BeginAnimation(Canvas.LeftProperty, o_animation);
			}


			double currentTop = Canvas.GetTop(madar);
			double targetTop = 175;
			double distance = targetTop - currentTop;

			if (distance <= 0)
				return;

			var anim = new DoubleAnimation
			{
				From = currentTop,
				To = targetTop,
				Duration = TimeSpan.FromSeconds(distance / m_speed),
				FillBehavior = FillBehavior.HoldEnd
			};

			madar.BeginAnimation(Canvas.TopProperty, anim);
		}

		private void jump_2_Click(object sender, RoutedEventArgs e)
		{
			madar.BeginAnimation(Canvas.TopProperty, null);

			double currentTop = Canvas.GetTop(madar);
			Canvas.SetTop(madar, currentTop - 12);

			StartFall();
		}

		private void jump_1_Click_1(object sender, RoutedEventArgs e)
		{
			madar.BeginAnimation(Canvas.TopProperty, null);

			double currentTop = Canvas.GetTop(madar);
			Canvas.SetTop(madar, currentTop - 12);

			StartFall();
		}

		void StartFall()
		{
			double m_speed = 50;

			madar.BeginAnimation(Canvas.TopProperty, null);

			double currentTop = Canvas.GetTop(madar);
			double targetTop = 175;
			double distance = targetTop - currentTop;

			if (distance <= 0)
				return;

			var anim = new DoubleAnimation
			{
				From = currentTop,
				To = targetTop,
				Duration = TimeSpan.FromSeconds(distance / m_speed),
				FillBehavior = FillBehavior.HoldEnd
			};

			madar.BeginAnimation(Canvas.TopProperty, anim);
		}
	}
}