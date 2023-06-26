using ClassesModule;
using MySql.Data.MySqlClient;
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
        //MySqlConnection MYconnect;
        //string ConnectString = "";
        private void usersett(object sender, MouseButtonEventArgs e)
        {
            //MySqlDataReader db_documents = Connection($"SELECT * FROM token");
            //if (db_documents != null)
            //{
            //    MYconnect.Close();
                Windows.userSettings settings = new Windows.userSettings();
            if (settings.canExist == true)
            {
                settings.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

                settings.ShowDialog();
            }
            else MessageBox.Show("Отустствует подключение! Проверьте настройки сети");

            // }
            // else
            // {
            //     MessageBox.Show("Отустствует подключение! Проверьте настройки сети");
            //     MYconnect.Close();
            // }

        }

        private void settingsstreamer(object sender, MouseButtonEventArgs e)
        {
            Windows.streamersettings settings = new Windows.streamersettings();
            settings.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            settings.ShowDialog();
            namestreamer.Content = Properties.Settings.Default.streamer;
        }
        //public void SetBase()
        //{
        //    ConnectString = $"server={Properties.Settings.Default.server};" +
        //            $"user={Properties.Settings.Default.user};" +
        //            $"password={Properties.Settings.Default.password};" +
        //            $"port={Properties.Settings.Default.port};" +
        //            $"database={Properties.Settings.Default.database};";

        //    MYconnect = new MySqlConnection(ConnectString);
        //}
        //public MySqlDataReader Connection(string query)
        //{
        //    try
        //    {
        //        MYconnect.Close();
        //        MYconnect.Open();

        //        MySqlCommand command = new MySqlCommand(query, MYconnect);
        //        MySqlDataReader reader = command.ExecuteReader();

        //        return reader;
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}
    }
}
