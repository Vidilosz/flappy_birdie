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
    class PipePair
    {
        public Rectangle TopPipe;
        public Rectangle BottomPipe;

        public double X;
        public bool Scored = false;

        public PipePair(Rectangle top, Rectangle bottom, double startX)
        {
            TopPipe = top;
            BottomPipe = bottom;
            X = startX;

            TopPipe.Visibility = Visibility.Collapsed;
            BottomPipe.Visibility = Visibility.Collapsed;
        }

        public void SetPosition(double x)
        {
            X = x;
            Canvas.SetLeft(TopPipe, X);
            Canvas.SetLeft(BottomPipe, X);
        }

        public void Show()
        {
            TopPipe.Visibility = Visibility.Visible;
            BottomPipe.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            TopPipe.Visibility = Visibility.Collapsed;
            BottomPipe.Visibility = Visibility.Collapsed;
            Scored = false;
        }

        public Rect GetTopRect() => new Rect(Canvas.GetLeft(TopPipe), Canvas.GetTop(TopPipe), TopPipe.Width, TopPipe.Height);
        public Rect GetBottomRect() => new Rect(Canvas.GetLeft(BottomPipe), Canvas.GetTop(BottomPipe), BottomPipe.Width, BottomPipe.Height);
    }

    public partial class MainWindow : Window
    {
        bool gameRunning = false;
        DateTime lastFrame;

        double velocityY = 0;
        double gravity = 500;
        double jumpStrength = -130;

        double groundY = 190;
        DateTime lastUpdate;

        int score = 0;

        List<PipePair> PipePairs;
        double pipeSpacing = 200;
        double pipeSpeed = 100;

        public MainWindow()
        {
            InitializeComponent();

            PipePairs = new List<PipePair>
            {
                new PipePair(Obstacle_1, Obstacle_2, 665),
                new PipePair(Obstacle_3, Obstacle_4, 865),
                new PipePair(Obstacle_5, Obstacle_6, 1065),
                new PipePair(Obstacle_7, Obstacle_8, 1265),
                new PipePair(Obstacle_9, Obstacle_10, 1465),
                new PipePair(Obstacle_11, Obstacle_12, 1665),
                new PipePair(Obstacle_13, Obstacle_14, 1865)
            };

            Normal.Visibility = Visibility.Collapsed;
            Rainy_day.Visibility = Visibility.Collapsed;
            TFI.Visibility = Visibility.Collapsed;
        }
        Rect GetRect(FrameworkElement element)
        {
            return new Rect(
                Canvas.GetLeft(element),
                Canvas.GetTop(element),
                element.Width,
                element.Height
            );
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.Visibility = Visibility.Collapsed;
            Normal.Visibility = Visibility.Visible;
            Rainy_day.Visibility = Visibility.Visible;
            TFI.Visibility = Visibility.Visible;

        }

        private void Normal_Click(object sender, RoutedEventArgs e) => StartMode(1);
        private void Rainy_day_Click(object sender, RoutedEventArgs e) => StartMode(2);
        private void TFI_Click(object sender, RoutedEventArgs e) => StartMode(3);

        void StartMode(int a)
        {
            Normal.Visibility = Visibility.Collapsed;
            Rainy_day.Visibility = Visibility.Collapsed;
            TFI.Visibility = Visibility.Collapsed;

            if (a == 2)
            {
                rain.Visibility = Visibility.Visible;
                jumpStrength = -80;
            }
            else if (a == 3)
            {
                kod.Visibility = Visibility.Visible;
                kod2.Visibility = Visibility.Visible;
            }

            StartGame();
        }

        public void StartGame()
        {
            ResetGame();

            lastFrame = DateTime.Now;
            gameRunning = true;
            CompositionTarget.Rendering += GameLoop;
        }


        void GameLoop(object sender, EventArgs e)
        {
            if (!gameRunning) return;

            double deltaTime = (DateTime.Now - lastFrame).TotalSeconds;
            lastFrame = DateTime.Now;

            UpdateBird(deltaTime);
            UpdatePipes(deltaTime);

            if (CheckCollision() || CheckBorderCollision())
            {
                GameOver();
                return;
            }

            CheckScore();
        }

        bool CheckCollision()
        {
            double birdX = Canvas.GetLeft(madar);
            double birdY = Canvas.GetTop(madar);
            double birdW = madar.Width;
            double birdH = madar.Height;

            foreach (var pair in PipePairs)
            {
                if (!pair.TopPipe.IsVisible) continue;

                if (Intersect(birdX, birdY, birdW, birdH,
                              Canvas.GetLeft(pair.TopPipe),
                              Canvas.GetTop(pair.TopPipe),
                              pair.TopPipe.Width,
                              pair.TopPipe.Height))
                    return true;

                if (Intersect(birdX, birdY, birdW, birdH,
                              Canvas.GetLeft(pair.BottomPipe),
                              Canvas.GetTop(pair.BottomPipe),
                              pair.BottomPipe.Width,
                              pair.BottomPipe.Height))
                    return true;
            }

            return false;
        }

        bool Intersect(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2)
        {
            return !(x2 > x1 + w1 || x2 + w2 < x1 || y2 > y1 + h1 || y2 + h2 < y1);
        }

        bool CheckBorderCollision()
        {
            Rect birdRect = GetRect(madar);

            Rect bottomRect = GetRect(bottom_border);

            return birdRect.IntersectsWith(bottomRect);
        }

        void CheckScore()
        {
            double birdRight = Canvas.GetLeft(madar) + madar.Width;

            foreach (var pair in PipePairs)
            {
                if (!pair.Scored && birdRight > pair.X + pair.TopPipe.Width)
                {
                    pair.Scored = true;
                    score++;
                    Title = $"Score: {score}";
                }
            }
        }


        void UpdateBird(double dt)
        {
            velocityY += gravity * dt;

            double newY = Canvas.GetTop(madar) + velocityY * dt;
            Canvas.SetTop(madar, newY);
        }

        void UpdatePipes(double dt)
        {
            double leftBorderX = Canvas.GetLeft(left_border);
            double rightBorderX = Canvas.GetLeft(right_border);

            double furthestX = PipePairs.Max(p => p.X);

            foreach (var pair in PipePairs)
            {
                pair.X -= pipeSpeed * dt;
                pair.SetPosition(pair.X);

                if (!pair.TopPipe.IsVisible && pair.X < rightBorderX)
                    pair.Show();

                if (pair.X + pair.TopPipe.Width < leftBorderX)
                {
                    pair.SetPosition(furthestX + pipeSpacing);
                    pair.Hide();
                    furthestX = pair.X;
                }
            }
        }

        void GameOver()
        {
            if (!gameRunning) return;

            gameRunning = false;
            CompositionTarget.Rendering -= GameLoop;

            velocityY = 0;

            GameOverText.Visibility = Visibility.Visible;
            Restart.Visibility = Visibility.Visible;
        }


        void ResetGame()
        {
            GameOverText.Visibility = Visibility.Collapsed;
            Restart.Visibility = Visibility.Collapsed;

            velocityY = 0;
            Canvas.SetLeft(madar, 188);
            Canvas.SetTop(madar, 45);

            double startX = 665;
            for (int i = 0; i < PipePairs.Count; i++)
            {
                PipePairs[i].SetPosition(startX + i * pipeSpacing);
                PipePairs[i].Hide();
                PipePairs[i].Scored = false;
            }

            score = 0;
            Title = "Score: 0";

            lastFrame = DateTime.Now;
            gameRunning = true;
            CompositionTarget.Rendering += GameLoop;
        }


        void Jump()
        {
            velocityY = jumpStrength;
        }

        private void jump_1_Click_1(object sender, RoutedEventArgs e) => Jump();
        private void jump_2_Click(object sender, RoutedEventArgs e) => Jump();

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }
    }
}