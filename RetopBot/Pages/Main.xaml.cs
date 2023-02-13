using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetopBot.Pages.PagesFuncs;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchLib.Client.Models;

namespace RetopBot.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public static Main main;
        
        public Main()
        {
            InitializeComponent();
            main = this;
            mainframe.Navigate(MainWindow.mainwindow.mainfuncpage);
            //mainframe.Navigate(MainWindow.mainwindow.commandspage);
        }

        private void mainbuttonclick(object sender, KeyEventArgs e)
        {
            
        }

        private void ClickMain(object sender, MouseButtonEventArgs e)
        {
            mainframe.Navigate(MainWindow.mainwindow.mainfuncpage);
        }
        private void ClickParty(object sender, MouseButtonEventArgs e)
        {
            mainframe.Navigate(MainWindow.mainwindow.allchatpage);

        }

        private void giveawaybtn(object sender, MouseButtonEventArgs e)
        {
            mainframe.Navigate(MainWindow.mainwindow.commandspage);
        }

        private void bordermouse(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                MainWindow.mainwindow.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime nw = DateTime.Now;
            string otv = MainWindow.mainwindow.RaznizaTime(MainWindow.mainwindow.startstream, nw);
            MessageBox.Show(otv);
        }

        private void gotostats(object sender, MouseButtonEventArgs e)
        {
            mainframe.Navigate(MainWindow.mainwindow.statisitc);
        }
        //class Predict
        //{
        //    public string data { get; set; }
        //    public string topic { get; set; }
        //    public string message { get; set; }
        //    public string Type { get; set; }
        //    public string Id { get; set; }
        //    public string ChannelId { get; set; }
        //    public string CreatedAt { get; set; }
        //    public string LockedAt { get; set; }
        //    public string EndedAt { get; set; }
        //    public List<string> Outcomes { get; set; }
        //    public string Status { get; set; }
        //    public string Title { get; set; }
        //    public string WinningOutcomeId { get; set; }
        //    public string PredictionTime { get; set; }
        //}

        private void goToSpecFunc(object sender, MouseButtonEventArgs e)
        {
                mainframe.Navigate(MainWindow.mainwindow.customfuncs);
        }
    }
}
