﻿using ClassesModule;
using HelpModule;
using MySql.Data.MySqlClient;
using RetopBot.Classes;
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
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using Variables;
using Windows.ApplicationModel.VoiceCommands;
using Windows.UI.Xaml.Media;

namespace RetopBot.Pages.PagesFuncs
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
            //01.11.2022
            //01.12.2022
            //01.01.2023
            //13.02.2023
            string nowDate = HelpMethods.GenerateDate();
            bool end = true;
            int startMount = 11;
            int startYear = 2022;

            List<string> mounts = new List<string>();
            mounts.Add("Ноябрь 2022");
            string[] mas = nowDate.Split('.');

            int nowMounth = 0;
            try
            {
                nowMounth = Convert.ToInt32(mas[1]);
            }
            catch
            {
                string newm = mas[1].Substring(0, 1);
                nowMounth = Convert.ToInt32(newm);
            }
            int nowYear = Convert.ToInt32(mas[2]);

            while (end)
            {
                startMount++;
                if (startMount == 13)
                {
                    mounts.Add(startYear.ToString());
                    startMount = 1;
                    startYear++;

                }
                if (nowMounth == startMount && startYear == nowYear) end = false;
                else
                {
                    switch (startMount)
                    {
                        case 1: mounts.Add("Январь " + startYear); break;
                        case 2: mounts.Add("Февраль " + startYear); break;
                        case 3: mounts.Add("Март " + startYear); break;
                        case 4: mounts.Add("Апрель " + startYear); break;
                        case 5: mounts.Add("Май " + startYear); break;
                        case 6: mounts.Add("Июнь " + startYear); break;
                        case 7: mounts.Add("Июль " + startYear); break;
                        case 8: mounts.Add("Август " + startYear); break;
                        case 9: mounts.Add("Сентябрь " + startYear); break;
                        case 10: mounts.Add("Октябрь " + startYear); break;
                        case 11: mounts.Add("Ноябрь " + startYear); break;
                        case 12: mounts.Add("Декабрь " + startYear); break;

                    }
                }
            }
            mounts.Reverse();
            foreach (var item in mounts)
            {
                mounthCb.Items.Add(item);
            }
        }
        public class ReportMsg
        {
            public string user { get; set; }
            public int count { get; set; }
        }

        public async void LaunchCalculate(string target)
        {
            awaitgrid.Visibility = Visibility.Visible;
            awaitgrid.IsEnabled = true;
            await Task.Run(() =>
            {

                
                if (target.Length != 4)
                {
                    string[] mas = target.Split(' ');
                    switch (mas[0])
                    {
                        case "Январь": target = ".01." + mas[1]; break;
                        case "Февраль": target = ".02." + mas[1]; break;
                        case "Март": target = ".03." + mas[1]; break;
                        case "Апрель": target = ".04." + mas[1]; break;
                        case "Май": target = ".05." + mas[1]; break;
                        case "Июнь":; target = ".06." + mas[1]; break;
                        case "Июль": target = ".07." + mas[1]; break;
                        case "Август": target = ".08." + mas[1]; break;
                        case "Сентябрь": target = ".09." + mas[1]; break;
                        case "Октябрь": target = ".10." + mas[1]; break;
                        case "Ноябрь": target = ".11." + mas[1]; break;
                        case "Декабрь": target = ".12." + mas[1]; break;
                    }
                }
                else target = "." + target;

                MySqlDataReader db_documents = MainWindow.mainwindow.database.Connection($"SELECT * FROM messages WHERE date LIKE '%{target}%' AND streamerNick = '{ValuesProject.StreamerName}'");
                List<MessageClass> localmsg = new List<MessageClass>();
                while (db_documents.Read())
                {
                    MessageClass newitem = new MessageClass();
                    newitem.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                    newitem.username = db_documents.GetValue(1).ToString();
                    newitem.message = db_documents.GetValue(2).ToString();
                    newitem.date = db_documents.GetValue(3).ToString();
                    newitem.time = db_documents.GetValue(4).ToString();
                    localmsg.Add(newitem);

                }
                int countmsgs = localmsg.Count;
                List<ReportMsg> usersMsgs = new List<ReportMsg>();
                for (int i = 0; i < localmsg.Count; i++)
                {

                    ReportMsg newit = new ReportMsg();
                    newit.count = localmsg.Where(x => x.username == localmsg[i].username).Count();
                    newit.user = localmsg[i].username;
                    usersMsgs.Add(newit);
                    localmsg.RemoveAll(x => x.username == newit.user);

                }
                var endusersMsgs = usersMsgs.OrderBy(x => x.count).Reverse();


                int mseto = 1;
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Label countmsgsLb = new Label()
                    {
                        Content = "Количество сообщений - " + countmsgs,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Foreground = Brushes.White,
                        FontSize = 22

                    };
                    parrent.Children.Add(countmsgsLb);
                    foreach (var item in endusersMsgs)
                    {
                        if (mseto == 11)
                        {
                            Label reportLb = new Label()
                            {
                                Content = "отчет за " + mounthCb.SelectedValue.ToString(),
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Foreground = Brushes.White,
                                FontSize = 12

                            };
                            parrent.Children.Add(reportLb);
                            break;
                        }
                        else
                        {
                            Label userLb = new Label()
                            {
                                Content = mseto + ". " + item.user + " - " + item.count + " сообщений",
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Foreground = Brushes.White,
                                FontSize = 16

                            };
                            parrent.Children.Add(userLb);   
                            mseto++;
                        }

                    }
                }));


            });
            awaitgrid.Visibility = Visibility.Hidden;
            awaitgrid.IsEnabled = false;

        }

        private void generatereport(object sender, MouseButtonEventArgs e)
        {
            if (mounthCb.SelectedIndex != -1)
            {
                parrent.Children.Clear();
                LaunchCalculate(mounthCb.SelectedValue.ToString());

            }
        }
    }
}
