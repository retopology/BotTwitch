
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Timers;

using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using TwitchLib.PubSub;
using ClassesModule;
using SqlHelper;
using MySql.Data.MySqlClient;
using TwitchLib.Client.Extensions;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Linq;
using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;
using TwitchLib.Api.Helix;
using TwitchLib.Api.Helix.Models.Clips.CreateClip;
using TwitchLib.Api.Helix.Models.Clips.GetClips;
using TwitchLib.Api.Core.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.ComponentModel;
using TwitchLib.Api.Core.Enums;
using System.Net.Http.Headers;
using System.Drawing;
using System.Windows.Markup;
using BotModule;
using DataBaseModule;
using Variables;

namespace RetopBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BotInfo botinfo;
        public ConnectData database;
        public bool existOnSystem = false;
        public MainWindow()
        {
            InitializeComponent();

            mainwindow = this;
            frame.Navigate(new Pages.startbot());
            
           
        }
        public void PushMsg(string msgid, string msg)
        {
            //if (msgid != ValuesProject.ActualUser.userId) botinfo.PushMsgFromWindow(msgid, msg);
            botinfo.SendManyMsgs(msg, 1);
        }
        private void Botinfo_onMessage(MessageClass message, bool track, bool moder)
        {
            if (track && ValuesProject.CB_FIND_TRACK && ValuesProject.findTrack)
            {
                ValuesProject.findTrack = false;
                //botinfo.SendManyMsgs("Ищу трек...", 1);
                FindMus(message.userid);
            }
            
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                mainfuncpage.WriteMessage(message, moder);
            }));
        }





            // Pages
            #region
            public Pages.PagesFuncs.mainfunc mainfuncpage = new Pages.PagesFuncs.mainfunc();
            public Pages.PagesFuncs.statisticpage statisitc = new Pages.PagesFuncs.statisticpage();
            public Pages.PagesFuncs.allChatPage allchatpage = new Pages.PagesFuncs.allChatPage();
            public Pages.PagesFuncs.ReportsPage reportspage;
            public Pages.PagesFuncs.customFuncs customfuncs;
            public Pages.PagesFuncs.CommandsPage commandspage;
            #endregion


            public static MainWindow mainwindow;
            
      

        public bool StartUsing()
        {
            try
            {

                ValuesProject.ConnectionString = $"server={Properties.Settings.Default.server};" +
                    $"user={Properties.Settings.Default.user};" +
                    $"password={Properties.Settings.Default.password};" +
                    $"port={Properties.Settings.Default.port};" +
                    $"database={Properties.Settings.Default.database};";

                ValuesProject.StreamerName = Properties.Settings.Default.streamer;
                ValuesProject.startstream = DateTime.Now;
                ValuesProject.StreamerId = Properties.Settings.Default.streamerid;
                ValuesProject.localpath = Directory.GetCurrentDirectory();
                ValuesProject.DotaBuffUrl = Properties.Settings.Default.dotabuff;

                ValuesProject.StreamerName = Properties.Settings.Default.streamer;
                ValuesProject.StreamerId = Properties.Settings.Default.streamerid;

                database = new ConnectData();
                botinfo = new BotInfo(database);
                botinfo.onMessage += Botinfo_onMessage;

                database.FillMessages();
                database.FillModerators();
                database.FillCommands();
                database.FillReports();



                database.GetLastIds();

                botinfo.GenerateBot();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    commandspage = new Pages.PagesFuncs.CommandsPage();
                    customfuncs = new Pages.PagesFuncs.customFuncs();
                    reportspage = new Pages.PagesFuncs.ReportsPage();
                });


                existOnSystem = true;
                return true;

            }
            catch
            {
                existOnSystem = false;
                return false;
            }

        }
        public void FindMus(string msgid)
        {
            ShazamFolder.MainShazam main = new ShazamFolder.MainShazam();
            main.GenetateMusicAsync(msgid);
            
        }

        // Window
        #region
        private void bordermouseloadibg(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void hotkeys(object sender, KeyEventArgs e)
        {
            //string word = e.Key.ToString();
            //var est = commands.Find(x => x.hotkey == word && x.type != "Off");


            //if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && est != null)
            //{
            //    SendMsg(est.text, 1);
            //}
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (existOnSystem == true)
            {
                if (ValuesProject.NEEDSAVEREPORT == true)
                {
                    statisitc.GenerateVanish();
                    string date = HelpModule.HelpMethods.GenerateDate();
                    string time = HelpModule.HelpMethods.GenerateTime();
                    statisitc.top_slovo = statisitc.top_slovo.Replace("'", "");
                    statisitc.top_user = statisitc.top_user.Replace("'", "");
                    database.InstertSql(new ClassesModule.rerportclass
                    {
                        id = 0,
                        count_msg = statisitc.count_msg,
                        count_unic_users = statisitc.count_unic_users,
                        duration = statisitc.duration,
                        count_timeout  = statisitc.count_timeout,
                        count_ban = statisitc.count_ban,
                        msgs_per_min = statisitc.msgs_per_min,
                        count_subs = statisitc.count_subs,
                        top_slovo = statisitc.top_slovo,
                        top_user = statisitc.top_user,
                        date = date,
                        time = time,
                        streamerNick = ValuesProject.StreamerName



                    });
                }

            }
            this.Close();
        }

        private void bordermouse(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            else Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }



        #endregion
    }
}
