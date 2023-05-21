using ClassesModule;
using HelpModule;
using HttpModule;
using MySql.Data.MySqlClient;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Variables;

namespace RetopBot.Pages.PagesFuncs
{
    /// <summary>
    /// Логика взаимодействия для allChatPage.xaml
    /// </summary>
    public partial class allChatPage : Page
    {
        public allChatPage()
        {
            InitializeComponent();
        }
        List<MessageClass> localmsg = new List<MessageClass>();

        public void FillData(string query)
        {
            MySqlDataReader db_documents = MainWindow.mainwindow.database.Connection(query);

            while (db_documents.Read())
            {
                MessageClass newitem = new MessageClass();
                newitem.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                newitem.username = db_documents.GetValue(1).ToString();
                newitem.message = db_documents.GetValue(2).ToString();
                newitem.date = db_documents.GetValue(3).ToString();
                newitem.time = db_documents.GetValue(4).ToString();
                newitem.userid = db_documents.GetValue(5).ToString();
                localmsg.Add(newitem);

            }
        }
        public void GenerateTabesal()
        {

           // try
           // {
                localmsg.Clear();
                parrent.Children.Clear();
                int ends = 0;
                    
                // Определение границ
                switch (endscb.SelectedIndex)
                {
                    case 0:
                        ends = 100;
                        break;
                    case 1:
                        ends = 300;
                        break;
                    case 2:
                        ends = 500;
                        break;
                    case 3:
                        ends = 5000;
                        break;
                    case 4:
                        ends = 10000;
                        break;
                    case 5:
                        ends = -1;
                        break;
                }
                // SELECT * FROM Table ORDER BY ID DESC LIMIT 1
                // ORDER BY id DESC LIMIT 1
                MySqlDataReader countMsgs = MainWindow.mainwindow.database.Connection($"SELECT * FROM `messages` WHERE streamerNick = '{ValuesProject.StreamerName}' ORDER BY ID DESC LIMIT 1");
                int countMAx = 0;
                while (countMsgs.Read())
                {

                    countMAx = Convert.ToInt32(countMsgs.GetValue(0).ToString());

                }
                int count = ends == -1 ? ends : countMAx - ends - 1;
                string tartext = searchtxt.Text;
                switch (targetcb.SelectedIndex)
                {
                    case 0:
                        FillData($"SELECT * FROM `messages` WHERE id > {count} AND username = '{tartext}' AND streamerNick = '{ValuesProject.StreamerName}' OR " +
                            $"id > {count} AND message LIKE '%{tartext}%' AND streamerNick = '{ValuesProject.StreamerName}' OR " +
                            $"id > {count} AND date LIKE '%{tartext}%' AND streamerNick = '{ValuesProject.StreamerName}' OR " +
                            $"id > {count} AND time LIKE '%{tartext}%' AND streamerNick = '{ValuesProject.StreamerName}'");
                        break;
                    case 1:
                        FillData($"SELECT * FROM `messages` WHERE id > {count} AND username = '{tartext}' AND streamerNick = '{ValuesProject.StreamerName}'");
                        break;
                    case 2:
                        FillData($"SELECT * FROM `messages` WHERE id > {count} AND message = '{tartext}' AND streamerNick = '{ValuesProject.StreamerName}'");
                        break;
                    case 3:
                        FillData($"SELECT * FROM `messages` WHERE id > {count} AND date = '{tartext}'  AND streamerNick = '{ValuesProject.StreamerName}'");
                        break;
                    case 4:
                        FillData($"SELECT * FROM `messages` WHERE id > {count} AND time = '{tartext}' AND streamerNick = '{ValuesProject.StreamerName}'");
                        break;

                }


                for (int i = 0; i < localmsg.Count; i++)
                {

                    Grid grid = new Grid();
                    grid.Margin = new Thickness(10, 10, 10, 10);
                    BrushConverter conert = new BrushConverter();
                    grid.Background = (Brush)conert.ConvertFrom("#FF4E4E4E");

                    Label time = new Label();
                    time.Content = localmsg[i].time;
                    grid.Children.Add(time);

                    Label date = new Label();
                    date.Margin = new Thickness(70, 0, 0, 0);
                    date.Content = localmsg[i].date;
                    grid.Children.Add(date);

                    Label name = new Label();
                    name.Foreground = Brushes.Red;
                    name.Margin = new Thickness(150, 0, 0, 0);
                    name.Content = localmsg[i].username;
                    grid.Children.Add(name);

                    Label msg = new Label();
                    msg.Margin = new Thickness(10, 25, 0, 0);
                    msg.FontSize = 18;
                    msg.Content = HelpMethods.GetCurrentStr(70, localmsg[i].message);
                    grid.Children.Add(msg);

                    Button copyname = new Button();
                    copyname.Tag = localmsg[i].username.ToString();
                    copyname.VerticalAlignment = VerticalAlignment.Top;
                    copyname.HorizontalAlignment = HorizontalAlignment.Right;
                    copyname.Content = "Copy Name";
                    copyname.Width = 100;
                    copyname.Margin = new Thickness(5, 5, 5, 5);
                    copyname.Click += delegate
                    {
                        Clipboard.SetText(copyname.Tag.ToString());
                    };
                    grid.Children.Add(copyname);

                    Button coptext = new Button();
                    coptext.Tag = localmsg[i].message.ToString();
                    coptext.VerticalAlignment = VerticalAlignment.Top;
                    coptext.HorizontalAlignment = HorizontalAlignment.Right;
                    coptext.Content = "Copy Text";
                    coptext.Width = 100;
                    coptext.Margin = new Thickness(5, 30, 5, 5);
                    coptext.Click += delegate
                    {
                        Clipboard.SetText(coptext.Tag.ToString());
                    };
                    grid.Children.Add(coptext);

                    Button timeout = new Button();
                    timeout.Tag = localmsg[i].userid.ToString();
                    timeout.VerticalAlignment = VerticalAlignment.Top;
                    timeout.HorizontalAlignment = HorizontalAlignment.Right;
                    timeout.Content = "Timeout 10";
                    timeout.Width = 100;
                    timeout.Margin = new Thickness(5, 30, 110, 5);
                    timeout.Click += delegate
                    {
                        POST.TimeOutUserCustom(600,timeout.Tag.ToString());
                    };
                    grid.Children.Add(timeout);

                    Button banuser = new Button();
                    banuser.Tag = localmsg[i].userid.ToString();
                    banuser.VerticalAlignment = VerticalAlignment.Top;
                    banuser.HorizontalAlignment = HorizontalAlignment.Right;
                    banuser.Content = "Ban";
                    banuser.Width = 100;
                    banuser.Margin = new Thickness(5, 5, 110, 5);
                    banuser.Click += delegate
                    {
                        POST.TimeOutUserCustom(1000000,banuser.Tag.ToString());
                    };
                    grid.Children.Add(banuser);

                    parrent.Children.Add(grid);



                        
                }
                allmsgsscroll.ScrollToEnd();


           // }
           // catch
           // {
           //     MessageBox.Show("Ошибка, не удалось отобразить данные");
           /// }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            GenerateTabesal();
        }
    }
}
