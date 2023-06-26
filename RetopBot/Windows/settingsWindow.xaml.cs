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
    /// Логика взаимодействия для settingsWindow.xaml
    /// </summary>
    public partial class settingsWindow : Window
    {
        public settingsWindow()
        {
            InitializeComponent();
            serverTxt.Text = Properties.Settings.Default.server;
            portTxt.Text = Properties.Settings.Default.port;
            userTxt.Text = Properties.Settings.Default.user;
            passwordTxt.Text = Properties.Settings.Default.password;
            databaseTxt.Text = Properties.Settings.Default.database;
        }

        private void dragger(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void savesettings(object sender, MouseButtonEventArgs e)
        {
            Properties.Settings.Default.server = serverTxt.Text;
            Properties.Settings.Default.port = portTxt.Text;
            Properties.Settings.Default.user = userTxt.Text;
            Properties.Settings.Default.password = passwordTxt.Text;
            Properties.Settings.Default.database = databaseTxt.Text;

            Properties.Settings.Default.Save();
            this.Close();

        }


        private void standartsettings(object sender, MouseButtonEventArgs e)
        {
            serverTxt.Text = "localhost";
            portTxt.Text = "3312";
            userTxt.Text = "root";
            passwordTxt.Text = "";
            databaseTxt.Text = "witch";
        }

        private void dragme(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
