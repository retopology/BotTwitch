using RetopBot.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
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

namespace RetopBot.Pages
{
    /// <summary>
    /// Логика взаимодействия для startbot.xaml
    /// </summary>
    public partial class startbot : Page
    {
        public startbot()
        {
            InitializeComponent();
            namestreamer.Content = Properties.Settings.Default.streamer;
            
        }

        private void start(object sender, MouseButtonEventArgs e)
        {
            StartBot();
            

        }
        public async void StartBot()
        {
            settingsbtn.IsEnabled = false;
            startBtn.IsEnabled = false;
            userSettings.IsEnabled = false;
            

            gifawait.Visibility = Visibility.Visible;
            bool end = false;
            await Task.Run(() =>
            {
                end = MainWindow.mainwindow.StartUsing();
                

            });
            gifawait.Visibility = Visibility.Hidden;
            settingsbtn.IsEnabled = true;
            startBtn.IsEnabled = true;
            userSettings.IsEnabled = true;
            if (end) MainWindow.mainwindow.frame.Navigate(new Pages.Main());
            else MessageBox.Show("Проверьте настройки подключения");

        }

        private void settings(object sender, MouseButtonEventArgs e)
        {
            Windows.settingsWindow settings = new Windows.settingsWindow();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            settings.ShowDialog();
            namestreamer.Content = Properties.Settings.Default.streamer;
        }

        private void usersett(object sender, MouseButtonEventArgs e)
        {
            Windows.userSettings settings = new Windows.userSettings();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            settings.ShowDialog();
        }
    }
}
