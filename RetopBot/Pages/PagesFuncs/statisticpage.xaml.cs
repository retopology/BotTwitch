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

namespace RetopBot.Pages.PagesFuncs
{
    /// <summary>
    /// Логика взаимодействия для statisticpage.xaml
    /// </summary>
    public partial class statisticpage : Page
    {
        public statisticpage()
        {
            InitializeComponent();
        }

        public bool generetaed = false;

        public int count_msg = 0;
        public int count_unic_users = 0;
        public string duration = "";
        public int count_timeout = 0;
        public int count_ban = 0;
        public int msgs_per_min = 0;
        public int count_subs = 0;
        public string top_slovo = "";
        public string top_user = "";

        public void GenerateVanish()
        {
            List<Classes.MessageClass> dublicate = MainWindow.mainwindow.chatmessage;
            // Топ слов
            #region
            string allword = "";

            for (int i = 0; i < dublicate.Count; i++)
            {
                if (i != dublicate.Count - 1) allword += dublicate[i].message + " ";
                else allword += dublicate[i].message;
            }
            var mas = allword.Split(' ').GroupBy(x => x).Select(x => new
            {
                KeyField = x.Key,
                Count = x.Count()
            }).OrderByDescending(x => x.Count).Take(11);
            int placeSlova = 1;
            foreach (var item in mas)
            {
                if (placeSlova == 1 && item.KeyField != "")
                {
                    top_slovo = item.KeyField;
                    break;
                }
            }
            #endregion

            // Топ юзеров
            #region
            string allusers = "";
            for (int i = 0; i < dublicate.Count; i++)
            {
                if (i != dublicate.Count - 1) allusers += dublicate[i].username + " ";
                else allusers += dublicate[i].username;
            }
            var mas1 = allusers.Split(' ').GroupBy(x => x).Select(x => new {
                KeyField = x.Key,
                Count = x.Count()
            }).OrderByDescending(x => x.Count).Take(10);
            int placeUser = 1;
            foreach (var item in mas1)
            {
                if (placeUser == 1)
                {
                    top_user = item.KeyField;
                    break;
                }
            }

            #endregion
            count_msg = dublicate.Count;
                int countUnicusers = 0;
                List<string> unicusers = new List<string>();
                foreach (var item in dublicate)
            {
                bool est = false;
                foreach (var user in unicusers)
                {
                    if (user == item.username)
                    {
                        est = true;
                        break;
                    }

                }
                if (!est)
                {
                    countUnicusers++;
                    unicusers.Add(item.username);
                }
            }
            count_unic_users = countUnicusers;
                DateTime endstream = DateTime.Now;
                string raznizatime = MainWindow.mainwindow.RaznizaTime(MainWindow.mainwindow.startstream, endstream);
            duration = raznizatime;
            count_timeout = MainWindow.mainwindow.timeoutedstream;
            count_ban = MainWindow.mainwindow.bansstream;
            int countMins = 0;
            string[] mastime = raznizatime.Split(':');
            char[] arrhoures = mastime[0].ToCharArray();
            if (arrhoures[0] == 0)
            {
                int houers = Convert.ToInt32(arrhoures[1]) * 60;
                countMins += houers;
            }
            else
            {
                int houers = Convert.ToInt32(mastime[0]) * 60;
                countMins += houers;
            }

            char[] arrMins = mastime[1].ToCharArray();
            if (arrMins[0] == 0) countMins += Convert.ToInt32(arrMins[1]);
            else countMins += Convert.ToInt32(mastime[1]);
            int msgsmin = 0;
            if (countMins == 0) msgsmin = 0;
            else msgsmin = dublicate.Count / countMins;

            msgs_per_min = msgsmin;
            count_subs = MainWindow.mainwindow.subsStream;

    }

        public void GenerateOtchet()
        {
            MainWindow.mainwindow.LoadData(MainWindow.tabels.report);
            Classes.rerportclass lastreport;
            if(MainWindow.mainwindow.reports.Count != 0) lastreport = MainWindow.mainwindow.reports[MainWindow.mainwindow.reports.Count - 1];
            else
            {
                lastreport = new Classes.rerportclass();
                lastreport.time = "00:00:00";
                lastreport.count_msg = 0;
                lastreport.count_unic_users = 0;
                lastreport.duration = "00:00:00";
                lastreport.count_timeout = 0;
                lastreport.count_ban = 0;
                lastreport.msgs_per_min = 0;
                lastreport.count_subs = 0;
                lastreport.top_slovo = "ba4ebar";
                lastreport.top_user = "ba4ebar";
                lastreport.date = "01.11.2022";

    }
            List<Classes.MessageClass> dublicate = MainWindow.mainwindow.chatmessage;

            // Топ слов
            #region
            string allword = "";

            for (int i = 0; i < dublicate.Count; i++)
            {
                if(i != dublicate.Count - 1)allword += dublicate[i].message + " ";
                else allword += dublicate[i].message;
            }
            var mas = allword.Split(' ').GroupBy(x => x).Select(x => new
            {
                KeyField = x.Key,
                Count = x.Count()
            }).OrderByDescending(x => x.Count).Take(11);
            int placeSlova = 1;
            foreach (var item in mas)
            {
                if (item.KeyField != "") {
                    if (placeSlova == 1) top_slovo = item.KeyField.ToString();
                    Grid slovagrid = new Grid();
                    slovagrid.Margin = new Thickness(5, 5, 5, 5);
                    BrushConverter conv = new BrushConverter();
                    slovagrid.Background = (Brush)conv.ConvertFrom("#FF2F2F2F");

                    Label slovo = new Label();
                    slovo.Content = placeSlova + ") " + item.KeyField.ToString();
                    slovo.Foreground = Brushes.White;
                    slovo.FontSize = 16;
                    slovagrid.Children.Add(slovo);

                    Label countslov = new Label();
                    countslov.HorizontalAlignment = HorizontalAlignment.Right;
                    countslov.Foreground = Brushes.White;
                    countslov.FontSize = 10;
                    countslov.Margin = new Thickness(0, 25, 0, 0);
                    countslov.Content = item.Count.ToString();

                    slovagrid.Children.Add(countslov);
                    parrenttopslova.Children.Add(slovagrid);
                    placeSlova++;
                }
            }
            #endregion

            // Топ юзеров
            #region
            string allusers = "";
            for (int i = 0; i < dublicate.Count; i++)
            {
                if (i != dublicate.Count - 1) allusers += dublicate[i].username + " ";
                else allusers += dublicate[i].username;
            }
            var mas1 = allusers.Split(' ').GroupBy(x => x).Select(x => new {
                KeyField = x.Key,
                Count = x.Count()
            }).OrderByDescending(x => x.Count).Take(10);
            int placeUser = 1;
            foreach (var item in mas1)
            {
                if (placeUser == 1) top_user = item.KeyField.ToString();
                Grid usergrid = new Grid();
                usergrid.Margin = new Thickness(5, 5, 5, 5);
                BrushConverter conv = new BrushConverter();
                usergrid.Background = (Brush)conv.ConvertFrom("#FF2F2F2F");

                Label usernam = new Label();
                usernam.Content = placeUser + ") " + item.KeyField.ToString();
                usernam.Foreground = Brushes.White;
                usernam.FontSize = 16;
                usergrid.Children.Add(usernam);

                Label countslovuser = new Label();
                countslovuser.HorizontalAlignment = HorizontalAlignment.Right;
                countslovuser.Foreground = Brushes.White;
                countslovuser.FontSize = 10;
                countslovuser.Margin = new Thickness(0, 25, 0, 0);
                countslovuser.Content = "Сообщений: " + item.Count.ToString();

                usergrid.Children.Add(countslovuser);
                parrenttopusers.Children.Add(usergrid);
                placeUser++;

            }
            #endregion

            countmsgslb.Content = dublicate.Count.ToString();
            int raz_countmsgs = dublicate.Count - lastreport.count_msg;
            if (raz_countmsgs > 0)
            {
                minus_count_msgs.Content = $"(+{raz_countmsgs})";
                minus_count_msgs.Foreground = Brushes.Lime;
            }
            else
            {
                minus_count_msgs.Content = $"({raz_countmsgs})";
                minus_count_msgs.Foreground = Brushes.Red;
            }
            count_msg = dublicate.Count;

            countsubs.Content = MainWindow.mainwindow.subsStream.ToString();
            int raz_subs = MainWindow.mainwindow.subsStream - lastreport.count_subs;
            if (raz_subs > 0)
            {
                minus_count_subs.Content = $"(+{raz_subs})";
                minus_count_subs.Foreground = Brushes.Lime;
            }
            else
            {
                minus_count_subs.Content = $"({raz_subs})";
                minus_count_subs.Foreground = Brushes.Red;
            }
            count_subs = MainWindow.mainwindow.subsStream;

            countbans.Content = MainWindow.mainwindow.bansstream.ToString();
            int raz_buns = MainWindow.mainwindow.bansstream - lastreport.count_ban;
            if (raz_buns > 0)
            {
                minus_count_bans.Content = $"(+{raz_buns})";
                minus_count_bans.Foreground = Brushes.Lime;
            }
            else
            {
                minus_count_bans.Content = $"({raz_buns})";
                minus_count_bans.Foreground = Brushes.Red;
            }
            count_ban = MainWindow.mainwindow.bansstream;


            countotsraneniy.Content = MainWindow.mainwindow.timeoutedstream.ToString();
            int raz_timeout = MainWindow.mainwindow.timeoutedstream - lastreport.count_timeout;
            if (raz_timeout > 0)
            {
                minus_count_timeoits.Content = $"(+{raz_timeout})";
                minus_count_timeoits.Foreground = Brushes.Lime;
            }
            else
            {
                minus_count_timeoits.Content = $"({raz_timeout})";
                minus_count_timeoits.Foreground = Brushes.Red;
            }
            count_timeout = MainWindow.mainwindow.timeoutedstream;


            int countUnicusers = 0;
            List<string> unicusers = new List<string>();
            foreach (var item in dublicate)
            {
                bool est = false;
                foreach (var user in unicusers)
                {
                    if(user == item.username)
                    {
                        est = true;
                        break;
                    }
                    
                }
                if (!est)
                {
                    countUnicusers++;
                    unicusers.Add(item.username);
                }
            }
            countunikusers.Content = countUnicusers.ToString();
            int raz_unic = countUnicusers - lastreport.count_unic_users;
            if (raz_unic > 0)
            {
                minus_unic_users.Content = $"(+{raz_unic})";
                minus_unic_users.Foreground = Brushes.Lime;
            }
            else
            {
                minus_unic_users.Content = $"({raz_unic})";
                minus_unic_users.Foreground = Brushes.Red;
            }
            count_unic_users = countUnicusers;


            DateTime endstream = DateTime.Now;
            string raznizatime = MainWindow.mainwindow.RaznizaTime(MainWindow.mainwindow.startstream, endstream);
            dlitenostlb.Content = raznizatime;
            duration = raznizatime;

            // Сообщений в минуту
            #region

            int countMins = 0;
            string[] mastime = raznizatime.Split(':');
            char[] arrhoures = mastime[0].ToCharArray();
            if (arrhoures[0] == 0)
            {
                int houers = Convert.ToInt32(arrhoures[1]) * 60;
                countMins += houers;
            }
            else
            {
                int houers = Convert.ToInt32(mastime[0]) * 60;
                countMins += houers;
            }

            char[] arrMins = mastime[1].ToCharArray();
            if (arrMins[0] == 0)countMins += Convert.ToInt32(arrMins[1]);
            else countMins += Convert.ToInt32(mastime[1]);
            int msgsmin = 0;
            if (countMins != 0)
            {
                msgsmin = dublicate.Count / countMins;
                countmsgsmin.Content = msgsmin.ToString();
            }
            else
            {
                countmsgsmin.Content = "0";
            }
            

            int raz_msg_per_sec = msgsmin - lastreport.msgs_per_min;
            if (raz_msg_per_sec > 0)
            {
                minus_msgs_per_Min.Content = $"(+{raz_msg_per_sec})";
                minus_msgs_per_Min.Foreground = Brushes.Lime;
            }
            else
            {
                minus_msgs_per_Min.Content = $"({raz_msg_per_sec})";
                minus_msgs_per_Min.Foreground = Brushes.Red;
            }
            msgs_per_min = msgsmin;


            generetaed = true;

            #endregion


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void generateotchet(object sender, MouseButtonEventArgs e)
        {
            otchetgrid.Visibility = Visibility.Visible;
            otchetgrid.IsEnabled = true;

            refreshotch.Visibility = Visibility.Visible;
            refreshotch.IsEnabled = true;

            generation.Visibility = Visibility.Hidden;
            generation.IsEnabled = false;
            GenerateOtchet();
        }

        private void refreshotchet(object sender, MouseButtonEventArgs e)
        {
            parrenttopusers.Children.Clear();
            parrenttopslova.Children.Clear();
            GenerateOtchet();
        }
    }
}
