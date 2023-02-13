using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace RetopBot.Pages.PagesFuncs
{
    /// <summary>
    /// Логика взаимодействия для giveaway.xaml
    /// </summary>
    public partial class giveaway : Page
    {
        public giveaway()
        {
            InitializeComponent();
            endRoulleteTimer.Elapsed += EndRoulleteTimer_Elapsed;
            roulettetimer.Elapsed += Roulettetimer_Elapsed;
        }

        private void EndRoulleteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerCountEnd--;
            if (timerCountEnd == 0)
            {
                endRoulleteTimer.Stop();
                roulettetimer.Stop();
                EndRoulette();
            }


        }
        Timer roulettetimer = new Timer(1000);
        Timer endRoulleteTimer = new Timer(1000);

        int lastTime = 15;
        int timerCount = 15;
        int timerCountEnd = 11;


        int n = 0;
        private void selectedtime(object sender, SelectionChangedEventArgs e)
        {
            if (n != 0)
            {
                switch (timercb.SelectedIndex)
                {
                    case 0: timerCount = 15;break;
                    case 1: timerCount = 30;break;
                    case 2: timerCount = 45;break;
                    case 3: timerCount = 60;break;
                    case 4: timerCount = 120;break;
                    case 5: timerCount = 180;break;
                    case 6: timerCount = 300;break;
                    case 7: timerCount = 600;break;
                    case 8: timerCount = 900;break;
                }
                timerlb.Content = timerCount.ToString();
            }
            else n = 1;
        }
        private void Roulettetimer_Elapsed(object sender, ElapsedEventArgs e)
        {

            timerCount--;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                timerlb.Content = timerCount.ToString();
            }));
            if (timerCount == 0)
            {
                MainWindow.mainwindow.Roulette = false;
                roulettetimer.Stop();
                endRoulleteTimer.Start();
                timerCount = 0;
                StartRoulette();
            }
        }
        public void EndRoulette()
        {
            timerCount = lastTime;
            timerCountEnd = 11;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                timerlb.Content = lastTime;
            
                WinGrid.Visibility = Visibility.Visible;
                WinGrid.IsEnabled = true;
                winnerlb.Content = winner;
            }));
        }
        string winner = "";
        public void StartRoulette()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (MainWindow.mainwindow.RouletteMembers.Count > 1)
                {
                    parrent.Children.Clear();
                    Random rnd = new Random();
                    int winNum = rnd.Next(0, MainWindow.mainwindow.RouletteMembers.Count);
                    winner = MainWindow.mainwindow.RouletteMembers[winNum];

                    //3541 3542 3543 3544 3545
                    //3642 3643 3644 3645 3646 3647
                    int g = 0;
                    int randomspeed = rnd.Next(9737, 10187);
                    if (randomspeed == 9885) randomspeed = 9884;
                    if (randomspeed == 9886) randomspeed = 9884;
                    if (randomspeed == 9887) randomspeed = 9889;
                    if (randomspeed == 9888) randomspeed = 9889;
                    if (randomspeed == 10036) randomspeed = 10035;
                    if (randomspeed == 10037) randomspeed = 10035;
                    if (randomspeed == 10038) randomspeed = 10041;
                    if (randomspeed == 10039) randomspeed = 10041;
                    if (randomspeed == 10040) randomspeed = 10041;


                    int winnerpgridid = 0;
                    if (randomspeed >= 9737 && randomspeed <= 9884) winnerpgridid = 61;
                    if (randomspeed >= 9889 && randomspeed <= 10035) winnerpgridid = 62;
                    if (randomspeed >= 10041 && randomspeed <= 10186) winnerpgridid = 63;

                    Random rand = new Random();
                    for (int i = 0; i < 67; i++)
                    {

                        Grid da = new Grid();
                        da.Width = 150;
                        da.Height = 150;
                        da.HorizontalAlignment = HorizontalAlignment.Left;
                        da.Margin = new Thickness(1);
                        da.Background = Brushes.Black;


                        int imgrand = rand.Next(1, 11);
                        Image img = new Image();
                        img.Source = new BitmapImage(new Uri(MainWindow.mainwindow.localpath +
                            "/ImgsRoulette/" + imgrand + ".png"));

                        da.Children.Add(img);

                        Grid backgroundlb = new Grid();
                        backgroundlb.Height = 30;
                        backgroundlb.Margin = new Thickness(0, 115, 0, 0);
                        backgroundlb.Opacity = 0.5;
                        backgroundlb.Background = Brushes.Black;

                        da.Children.Add(backgroundlb);

                        Label winnerlb = new Label();
                        winnerlb.HorizontalAlignment = HorizontalAlignment.Center;
                        winnerlb.VerticalAlignment = VerticalAlignment.Center;
                        winnerlb.Foreground = Brushes.White;
                        winnerlb.FontSize = 16;
                        winnerlb.Margin = new Thickness(0, 115, 0, 0);


                        g = rnd.Next(0, MainWindow.mainwindow.RouletteMembers.Count);
                        if (i == winnerpgridid) winnerlb.Content = i + "." + winner;
                        else winnerlb.Content = i + "." + MainWindow.mainwindow.RouletteMembers[g];

                        da.Children.Add(winnerlb);
                        parrent.Children.Add(da);

                    }
                    //   int speed = rnd.Next(3500, 4001);
                    //randomspeed = Convert.ToInt32(speedtxt.Text);
                    randomspeed = randomspeed * -1;
                    TranslateTransform go = new TranslateTransform();
                    STARTroul.RenderTransform = go;
                    DoubleAnimation ani = new DoubleAnimation(0, randomspeed, TimeSpan.FromSeconds(10));
                    ani.EasingFunction = new QuadraticEase();
                    go.BeginAnimation(TranslateTransform.XProperty, ani);
                }
                else
                {
                    endRoulleteTimer.Stop();
                    roulettetimer.Stop();
                    MessageBox.Show("Мало участников");
                    timerCount = lastTime;
                    timerCountEnd = 11;
                    timerlb.Content = lastTime;
                    winnerlb.Content = winner;
                }
            }));
        }

        

        private void clickstartroulet(object sender, MouseButtonEventArgs e)
        {
            
            lastTime = timerCount;
            timerlb.Content = timerCount.ToString();
            roulettetimer.Start();
            MainWindow.mainwindow.slovoRoulette = slovotxt.Text;
            MainWindow.mainwindow.Roulette = true;
        }




        private void clearreulst(object sender, MouseButtonEventArgs e)
        {
            MainWindow.mainwindow.Roulette = false;
            MainWindow.mainwindow.RouletteMembers.Clear();
            countmembers.Content = "Участников: 0";
            parrent.Children.Clear();
            WinGrid.Visibility = Visibility.Hidden;
            WinGrid.IsEnabled = false;
            endRoulleteTimer.Stop();
            roulettetimer.Stop();
            timerCount = lastTime;
            timercb.SelectedIndex = 0;
            timerCountEnd = 11;
            timerlb.Content = lastTime;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(winner);
        }
    }
}
