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
        public void GenerateTabesal()
        {
                
                    try
                    {
                        parrent.Children.Clear();
                        MainWindow.mainwindow.LoadData(MainWindow.tabels.messages);



                        int ends = 0;
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
                                ends = MainWindow.mainwindow.data_base_chatmessage.Count;
                                break;
                        }


                        for (int i = MainWindow.mainwindow.data_base_chatmessage.Count - ends; i < MainWindow.mainwindow.data_base_chatmessage.Count; i++)
                        {
                            bool filter = true;
                            if (searchtxt.Text != "")
                            {
                                switch (targetcb.SelectedIndex)
                                {
                                    case 0:
                                        {
                                            if (!MainWindow.mainwindow.data_base_chatmessage[i].message.Contains(searchtxt.Text) &&
                                                !MainWindow.mainwindow.data_base_chatmessage[i].username.Contains(searchtxt.Text) &&
                                                !MainWindow.mainwindow.data_base_chatmessage[i].date.Contains(searchtxt.Text) &&
                                                !MainWindow.mainwindow.data_base_chatmessage[i].time.Contains(searchtxt.Text)) filter = false;
                                        }
                                        break;
                                    case 1:
                                        if (!MainWindow.mainwindow.data_base_chatmessage[i].username.Contains(searchtxt.Text)) filter = false;
                                        break;
                                    case 2:
                                        if (!MainWindow.mainwindow.data_base_chatmessage[i].message.Contains(searchtxt.Text)) filter = false;
                                        break;
                                    case 3:
                                        if (!MainWindow.mainwindow.data_base_chatmessage[i].date.Contains(searchtxt.Text)) filter = false;
                                        break;
                                    case 4:
                                        if (!MainWindow.mainwindow.data_base_chatmessage[i].time.Contains(searchtxt.Text)) filter = false;
                                        break;
                                }
                            }

                            if (filter)
                            {
                                //    < Grid Background = "#FF4E4E4E" Margin = "10" >
                                //    < Label Content = "00:00:00" />
                                //    < Label Margin = "70,0,0,0" Content = "12.02.2022" />
                                //    < Label Margin = "150,0,0,0" Content = "Name" />
                                //    < Label Margin = "10,25,0,0" Content = "Text" />
                                //    < Button Content = "Copy Name" VerticalAlignment = "Top" HorizontalAlignment = "Right" Margin = "5" Width = "100" />
                                //    < Button Content = "Copy Text" VerticalAlignment = "Top" HorizontalAlignment = "Right" Margin = "5,30,5,5" Width = "100" />
                                //    < Button Content = "Timeout 10" VerticalAlignment = "Top" HorizontalAlignment = "Right" Margin = "5,30,110,5" Width = "100" />
                                //    < Button Content = "Ban" VerticalAlignment = "Top" HorizontalAlignment = "Right" Margin = "5,5,110,5" Width = "100" />
                                //</ Grid >
                                Grid grid = new Grid();
                                grid.Margin = new Thickness(10, 10, 10, 10);
                                BrushConverter conert = new BrushConverter();
                                grid.Background = (Brush)conert.ConvertFrom("#FF4E4E4E");

                                Label time = new Label();
                                time.Content = MainWindow.mainwindow.data_base_chatmessage[i].time;
                                grid.Children.Add(time);

                                Label date = new Label();
                                date.Margin = new Thickness(70, 0, 0, 0);
                                date.Content = MainWindow.mainwindow.data_base_chatmessage[i].date;
                                grid.Children.Add(date);

                                Label name = new Label();
                                name.Foreground = Brushes.Red;
                                name.Margin = new Thickness(150, 0, 0, 0);
                                name.Content = MainWindow.mainwindow.data_base_chatmessage[i].username;
                                grid.Children.Add(name);

                                Label msg = new Label();
                                msg.Margin = new Thickness(10, 25, 0, 0);
                                msg.FontSize = 18;
                                msg.Content = MainWindow.mainwindow.GetCurrentStr(70, MainWindow.mainwindow.data_base_chatmessage[i].message);
                                grid.Children.Add(msg);

                                Button copyname = new Button();
                                copyname.Tag = MainWindow.mainwindow.data_base_chatmessage[i].username.ToString();
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
                                coptext.Tag = MainWindow.mainwindow.data_base_chatmessage[i].message.ToString();
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
                                timeout.Tag = MainWindow.mainwindow.data_base_chatmessage[i].username.ToString();
                                timeout.VerticalAlignment = VerticalAlignment.Top;
                                timeout.HorizontalAlignment = HorizontalAlignment.Right;
                                timeout.Content = "Timeout 10";
                                timeout.Width = 100;
                                timeout.Margin = new Thickness(5, 30, 110, 5);
                                timeout.Click += delegate
                                {
                                    MainWindow.mainwindow.TimeOutUser(timeout.Tag.ToString(), 10);
                                };
                                grid.Children.Add(timeout);

                                Button banuser = new Button();
                                banuser.Tag = MainWindow.mainwindow.data_base_chatmessage[i].username.ToString();
                                banuser.VerticalAlignment = VerticalAlignment.Top;
                                banuser.HorizontalAlignment = HorizontalAlignment.Right;
                                banuser.Content = "Ban";
                                banuser.Width = 100;
                                banuser.Margin = new Thickness(5, 5, 110, 5);
                                banuser.Click += delegate
                                {
                                    MainWindow.mainwindow.BanUserMethod(banuser.Tag.ToString());
                                };
                                grid.Children.Add(banuser);

                                parrent.Children.Add(grid);



                            }
                        }
                        allmsgsscroll.ScrollToEnd();


                    }
                    catch
                    {
                        MessageBox.Show("Ошибка, не удалось отобразить данные");
                    }
                
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            GenerateTabesal();
        }
    }
}
