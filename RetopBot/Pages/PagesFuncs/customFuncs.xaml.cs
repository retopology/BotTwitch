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
using MySql.Data.MySqlClient;
using RetopBot.Classes;
using System.Collections.Specialized;
using TwitchLib.Api.Helix.Models.Charity.GetCharityCampaign;

namespace RetopBot.Pages.PagesFuncs
{
    /// <summary>
    /// Логика взаимодействия для customFuncs.xaml
    /// </summary>
    public partial class customFuncs : Page
    {
        public customFuncs()
        {
            InitializeComponent();
            for (int i = 0; i < MainWindow.mainwindow.commands.Count; i++)
            {
                commandsCb.Items.Add(i + "." + MainWindow.mainwindow.commands[i].header);
            }
            

        }
        
        public List<Classes.TimersClass> timerslist = new List<TimersClass>();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(commandsCb.SelectedIndex != -1)
            {
                if(timespamcommand.Text != "")
                {
                    string[] mas = commandsCb.SelectedValue.ToString().Split('.');
                    TimersClass newTimer = new TimersClass(MainWindow.mainwindow.commands[Convert.ToInt32(mas[0])].text, Convert.ToInt32(timespamcommand.Text), mas[1]);
                    timerslist.Add(newTimer);
                    FillCommandsList();
                }
            }
        }
        public void FillCommandsList()
        {
            parrStack.Children.Clear();
            for (int i = 0; i < timerslist.Count; i++)
            {
                Grid grid = new Grid();
                BrushConverter brh = new BrushConverter();
                grid.Background = (Brush)brh.ConvertFromString("#FF242424");
                grid.Margin = new Thickness(5, 5, 5, 5);

                Label header = new Label() {
                    Content = timerslist[i].header,
                    Foreground = Brushes.White,
                    FontSize = 20
                };
                grid.Children.Add(header);
                Label textcomm = new Label()
                {
                    Content = timerslist[i].text,
                    Foreground = Brushes.White,
                    FontSize = 10,
                    Margin = new Thickness(0,35,0,0)
                };
                grid.Children.Add(textcomm);
                Button delete = new Button()
                {
                    Content = "Remove",
                    Tag = i.ToString(),
                    Height = 20,
                    Width = 100,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(10,10,10,10),
                    Background = null,
                    BorderBrush = Brushes.White,
                    Foreground = Brushes.White
                };
                delete.Click += delegate
                {
                    timerslist[Convert.ToInt32(delete.Tag)].StopTimer();
                    timerslist[Convert.ToInt32(delete.Tag)] = null;
                    timerslist.RemoveAt(Convert.ToInt32(delete.Tag));
                    FillCommandsList();

                    long totalMemory = GC.GetTotalMemory(false);

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                };
                grid.Children.Add(delete);
                Button pause = new Button()
                {
                    Content = "||",
                    Tag = i.ToString(),
                    Height = 20,
                    FontSize = 7,
                    Width = 20,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(10, 10, 120, 10),
                    Background = null,
                    BorderBrush = Brushes.White,
                    Foreground = Brushes.White
                };
                pause.Click += delegate
                {
                    if(timerslist[Convert.ToInt32(pause.Tag)].ActiveTimer == true)
                    {
                        timerslist[Convert.ToInt32(pause.Tag)].StopTimer();
                        pause.Content = "[>";
                        timerslist[Convert.ToInt32(pause.Tag)].ActiveTimer = false;
                    }
                    else
                    {
                        timerslist[Convert.ToInt32(pause.Tag)].ResumeTimer();
                        pause.Content = "||";
                        timerslist[Convert.ToInt32(pause.Tag)].ActiveTimer = true;
                    }
                };
               // < Button Content = "||" Height = "20" Width = "20" VerticalAlignment = "Top" FontSize = "7"
                //                HorizontalAlignment = "Right" Margin = "10,10,120,10"
                //                BorderBrush = "White" Background = "{x:Null}" Foreground = "White" />
                grid.Children.Add(pause);
                parrStack.Children.Add(grid);

            }
        }
        private void selectcommand(object sender, SelectionChangedEventArgs e)
        {
            string[] mas = commandsCb.SelectedValue.ToString().Split('.');
            string text = MainWindow.mainwindow.commands.Find(x => x.header == mas[1]).text;
            commandLb.Content = text;
        }


        private void clicked(object sender, RoutedEventArgs e)
        {
            bool end = true;
            if (cbALl.IsChecked == true) end = true;
            else end = false;
            foreach (CheckBox item in cbgrid.Children)
            {
                item.IsChecked = end;
            }
        }
    }
}
