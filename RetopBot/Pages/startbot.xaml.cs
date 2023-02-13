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
            namestreamer.Content = MainWindow.mainwindow.channelname;
            
        }

        private void start(object sender, MouseButtonEventArgs e)
        {
           bool end =  MainWindow.mainwindow.GenerateBot();
            if (end) MainWindow.mainwindow.frame.Navigate(new Pages.Main());
            else MessageBox.Show("Не удалость подключится, попробуйте повторить попытку или перезапустить приложение");
        }
    }
}
