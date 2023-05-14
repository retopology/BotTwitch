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
    /// Логика взаимодействия для killDemaLLL.xaml
    /// </summary>
    public partial class killDemaLLL : Page
    {
        public killDemaLLL()
        {
            InitializeComponent();
        }
        string[] array;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.LoadData(MainWindow.tabels.messages);
            string[] mas = new string[MainWindow.mainwindow.data_base_chatmessage.Count];
            for (int i = 0; i < mas.Length; i++)
                mas[i] = MainWindow.mainwindow.data_base_chatmessage[i].username;
            array = mas.Distinct().ToArray();
            userslb.Content = array.Length + " - ЮЗЕРОВ";
            for (int i = 0; i < array.Length; i++)
                userslist.Items.Add(array[i]);

            banbtn.Visibility = Visibility.Visible;
            banbtn.IsEnabled = true;
        }

        private void banbtn_Click(object sender, RoutedEventArgs e)
        {
            //int num = 1000;
            //if (twenty.IsChecked == true) num = 200;
            //for (int i = 0; i < num; i++)
            //{
            //    MainWindow.mainwindow.TimeOutUserCustom(array[i], 1);
            //}
        }
    }
}
