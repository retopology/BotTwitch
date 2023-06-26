using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RetopBot.Windows
{
    /// <summary>
    /// Логика взаимодействия для streamersettings.xaml
    /// </summary>
    public partial class streamersettings : Window
    {
        public streamersettings()
        {
            InitializeComponent();
            streamerTxt.Text = Properties.Settings.Default.streamer;
            twitchIdTxt.Text = Properties.Settings.Default.streamerid;
            dotabuffTxt.Text = Properties.Settings.Default.dotabuff;
        }

        private void dragger(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void savesett(object sender, MouseButtonEventArgs e)
        {
            Properties.Settings.Default.streamer = streamerTxt.Text;
            Properties.Settings.Default.streamerid = twitchIdTxt.Text;
            Properties.Settings.Default.dotabuff = dotabuffTxt.Text;

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void standart(object sender, MouseButtonEventArgs e)
        {
            streamerTxt.Text = "witchblvde";
            twitchIdTxt.Text = "698924516";
            dotabuffTxt.Text = "https://ru.dotabuff.com/players/173843946";
        }
    }
}
