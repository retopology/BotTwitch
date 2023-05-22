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
using Variables;

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
            for (int i = 0; i < ValuesProject.Commands.Count; i++)
            {
                if (ValuesProject.Commands[i].type != "Off")
                    commandsCb.Items.Add(i + "." + ValuesProject.Commands[i].header);
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
                    TimersClass newTimer = new TimersClass(ValuesProject.Commands[Convert.ToInt32(mas[0])].text, 
                        Convert.ToInt32(timespamcommand.Text), mas[1]);
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
                grid.Margin = new Thickness(10, 10, 10, 10);

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
            string text = ValuesProject.Commands.Find(x => x.header == mas[1]).text;
            commandLb.Content = text;
        }


        private void clicked(object sender, RoutedEventArgs e)
        {
            bool end = true;
            if (cbALl.IsChecked == true)
            {
                ValuesProject.CB_FIND_TRACK = true;
                ValuesProject.CB_COLORED = true;
                ValuesProject.CB_COMMANDS_LIST = true;
                ValuesProject.CB_WIN_LOSE = true;
                ValuesProject.CB_AUTO_QUESTION = true;
                ValuesProject.CB_LAST_GAME = true;
                ValuesProject.CB_STATA = true;
                ValuesProject.CB_COUNT_MSGS = true;
                ValuesProject.CB_GOOGLE = true;
                end = true;
            }
            else
            {
                ValuesProject.CB_FIND_TRACK = false;
                ValuesProject.CB_COLORED = false;
                ValuesProject.CB_COMMANDS_LIST = false;
                ValuesProject.CB_WIN_LOSE = false;
                ValuesProject.CB_AUTO_QUESTION = false;
                ValuesProject.CB_LAST_GAME = false;
                ValuesProject.CB_STATA = false;
                ValuesProject.CB_COUNT_MSGS = false;
                ValuesProject.CB_GOOGLE = false;
                end = false;
            }
            foreach (CheckBox item in cbgrid.Children)
            {
                item.IsChecked = end;
            }
        }

        private void googleClick(object sender, RoutedEventArgs e)
        {
            if (cbgoogleit.IsChecked == true) ValuesProject.CB_GOOGLE = true;
            else ValuesProject.CB_GOOGLE = false;
        }



        private void countmsgsCLick(object sender, RoutedEventArgs e)
        {
            if (cbcountmsgs.IsChecked == true) ValuesProject.CB_COUNT_MSGS = true;
            else ValuesProject.CB_COUNT_MSGS = false;
        }

        private void stataClick(object sender, RoutedEventArgs e)
        {
            if (cbstatehero.IsChecked == true) ValuesProject.CB_STATA = true;
            else ValuesProject.CB_STATA = false;
        }

        private void lastgameClick(object sender, RoutedEventArgs e)
        {
            if (cblastgame.IsChecked == true) ValuesProject.CB_LAST_GAME = true;
            else ValuesProject.CB_LAST_GAME = false;
        }

        private void winloseClick(object sender, RoutedEventArgs e)
        {
            if (cbwinlose.IsChecked == true) ValuesProject.CB_WIN_LOSE = true;
            else ValuesProject.CB_WIN_LOSE = false;
        }

        private void mostQuestionClick(object sender, RoutedEventArgs e)
        {
            if (cbMostQuestions.IsChecked == true) ValuesProject.CB_AUTO_QUESTION = true;
            else ValuesProject.CB_AUTO_QUESTION = false;
        }

        private void commandsClick(object sender, RoutedEventArgs e)
        {
            if (commandslistcb.IsChecked == true) ValuesProject.CB_COMMANDS_LIST = true;
            else ValuesProject.CB_COMMANDS_LIST = false;
        }

        private void tagMeClick(object sender, RoutedEventArgs e)
        {
            if (cbtagme.IsChecked == true) ValuesProject.CB_TAG_MESSAGE = true;
            else ValuesProject.CB_TAG_MESSAGE = false;
            
        }

        private void colorNickClick(object sender, RoutedEventArgs e)
        {
            if (cbcolornick.IsChecked == true) ValuesProject.CB_COLORED = true;
            else ValuesProject.CB_COLORED = false;
        }

        private void findtrack(object sender, RoutedEventArgs e)
        {
            if (cbfindtrack.IsChecked == true) ValuesProject.CB_FIND_TRACK = true;
            else ValuesProject.CB_FIND_TRACK = false;
        }
    }
}
