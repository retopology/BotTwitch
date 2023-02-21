// я сосу хуй

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
using RetopBot.Classes;
using SqlHelper;
using MySql.Data.MySqlClient;
using TwitchLib.Client.Extensions;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Linq;
using TwitchLib.Api.Helix.Models.Search;
using System.Diagnostics;
using TwitchLib.PubSub.Events;

using XamlAnimatedGif.Decoding;
using HtmlAgilityPack;
using System.Text;
using TwitchLib.Api.Helix;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Interop;

namespace RetopBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class heroesclass
        {
            public string name { get; set; }
            public string winrate { get; set; }
            public string matches { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();

            mainwindow = this;

            
            startstream = DateTime.Now;
            localpath = Directory.GetCurrentDirectory();
            MYconnect = new MySqlConnection(ConnectString);
            LoadData(tabels.messages);
            LoadData(tabels.report);
            LoadData(tabels.commands);
            LoadData(tabels.moderators);
            actualColor = 0;
            commandspage = new Pages.PagesFuncs.CommandsPage();
            customfuncs = new Pages.PagesFuncs.customFuncs();
            CountIndex = data_base_chatmessage[data_base_chatmessage.Count - 1].id + 1;
            actualbansession = moderation[moderation.Count - 1].session + 1;
            idBanMods = moderation.Count;
            localtimer.Elapsed += Localtimer_Elapsed;
            frame.Navigate(new Pages.startbot());







        }

        // Переменные
        #region


            // Timer
            #region
            List<string> localgivelist = new List<string>();
            List<string> localwinners = new List<string>();
            public bool localgive = false;
            string moment = "1";
            System.Timers.Timer localtimer = new System.Timers.Timer(1000);
            int localcounttick = 0;
            bool findtogive = false;
            #endregion

        
            // Списки
            #region

            public List<Classes.MessageClass> chatmessage = new List<Classes.MessageClass>();
            public List<Classes.rerportclass> reports = new List<Classes.rerportclass>();
            public List<Classes.MessageClass> data_base_chatmessage = new List<Classes.MessageClass>();
            public List<Classes.CommandsClass> commands = new List<Classes.CommandsClass>();
            public List<Classes.ModeratorsClass> moderation = new List<Classes.ModeratorsClass>();
            public List<Classes.Games> games = new List<Classes.Games>();
            public List<String> heroeslist = new List<String>();
            public List<Classes.Countryes> countryes = new List<Countryes>();

            #endregion


            // Pages
            #region
            public Pages.PagesFuncs.mainfunc mainfuncpage = new Pages.PagesFuncs.mainfunc();
            public Pages.PagesFuncs.statisticpage statisitc = new Pages.PagesFuncs.statisticpage();
            public Pages.PagesFuncs.allChatPage allchatpage = new Pages.PagesFuncs.allChatPage();
            public Pages.PagesFuncs.customFuncs customfuncs;
            public Pages.PagesFuncs.CommandsPage commandspage;
            #endregion


            // Свалка
            #region
             public string channelname = "ba4ebar";
            //public string channelname = "witchblvde";
            // public string channelname = "nmplol";
            public string myName = "";
            public string ActualName;
            public string localpath = "";
            public static MainWindow mainwindow;
            public static TwitchClient client = new TwitchClient();
            public bool countryGame = false;
            public bool findTrack = true;
            public ConnectionCredentials cred;
            public int CountIndex = 0;
            int counterrors = 0;
            int countendprocess = 0;
            public int subsStream = 0;
            public int joinedusers = 0;
            public int bansstream = 0;
            public int timeoutedstream = 0;
            int idBanMods = 0;
            int actualbansession = 0;
            int actualColor;
            bool maskGiveAway = false;

            public DateTime startstream;
            int problems = 0;
            public bool Roulette = false;
            public string slovoRoulette = "";
            public List<string> RouletteMembers = new List<string>();

            string ConnectString = "server=localhost;user=root;port=3312;database=witch;";
            MySqlConnection MYconnect;
            #endregion


            // Таблицы
            public enum tabels
            {
                messages, commands, report, moderators
            }

        #endregion


        // Bot
        #region
        public void BanUserMethod(string user)
        {
            client.BanUser(channelname, user);
        }
        public void TimeOutUser(string user, int duration)
        {
            client.TimeoutUser(channelname, user, TimeSpan.FromMinutes(duration), "Ban");
        }
        public bool GenerateBot()
        {
            try
            {

                string token = "";
                MySqlDataReader db_documents = Connection($"SELECT * FROM token");
                while (db_documents.Read())
                {
                    Classes.MessageClass newitem = new MessageClass();
                    myName = db_documents.GetValue(0).ToString();
                    token = db_documents.GetValue(1).ToString();

                }
                
                cred = new ConnectionCredentials(myName, token);

                ActualName = myName;

                client.Initialize(cred, channelname);
                client.Connect();
                client.OnChatCommandReceived += Client_OnChatCommandReceived; // Бот читает КОМАНДЫ
                client.OnMessageReceived += Client_OnMessageReceived; // Бот читает каждое сообщение

                client.OnNewSubscriber += Client_OnNewSubscriber;


                client.OnUserTimedout += Client_OnUserTimedout;
                client.OnUserBanned += Client_OnUserBanned;
                client.OnMessageCleared += Client_OnMessageCleared;



                client.OnJoinedChannel += Client_OnJoinedChannel;

                return true;

            }
            catch
            {
                return false;
            }

        }
        public void SendMsg(string str, int count)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    client.SendMessage(channelname, str);
                }
                 
            }
            catch
            {
                problems++;
            }
        }
        public void SendRpl(string msgid, string msg)
        {
            client.SendReply(channelname, msgid, msg);
        }
        #endregion


        // Database
        #region

        public async void DataBaseFill()
        {
            try
            {
                LoadData(tabels.messages);
                //Gif start
                maingrid.IsEnabled = false;
                loadingend.IsEnabled = true;
                loadingend.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(() =>
                {
                    int countprocess = chatmessage.Count;

                    int lengt = data_base_chatmessage.Count;

                    foreach (var item in chatmessage)
                    {

                        string name = item.username.Replace("'", "");
                        string msg = item.message.Replace("'", "");

                        try
                        {
                            var end = Connection(SqlHelp.InsterSql(new string[] {
                        "messages", lengt.ToString(), name, msg, item.date, item.time
                        }));
                            lengt++;

                        }
                        catch
                        {
                            counterrors++;
                        }

                        countendprocess++;
                    }

                });
                if (counterrors > 0) MessageBox.Show($"Из {countendprocess} не удалось загрузить {counterrors}");
                //Gif end
                maingrid.IsEnabled = true;
                loadingend.IsEnabled = false;
                loadingend.Visibility = Visibility.Hidden;

            }
            catch
            {
                MessageBox.Show("Не удалось отрузить данные");
            }



        }
        public string FindId(string str)
        {
            string[] mas = str.Split(' ');
            foreach (var item in mas)
            {
                if (NumberOrNot(item) && item.Length == 10 | item.Length == 9)
                {
                    return item;
                }
            }
            return "";
        }
        public void LoadData(tabels Lock)
        {
            MySqlDataReader db_documents = Connection("SELECT * FROM " + Lock.ToString());
            switch (Lock.ToString())
            {
                case "messages":
                    {
                        data_base_chatmessage.Clear();
                        while (db_documents.Read())
                        {
                            Classes.MessageClass newitem = new MessageClass();
                            newitem.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                            newitem.username = db_documents.GetValue(1).ToString();
                            newitem.message = db_documents.GetValue(2).ToString();
                            newitem.date = db_documents.GetValue(3).ToString();
                            newitem.time = db_documents.GetValue(4).ToString();



                            data_base_chatmessage.Add(newitem);


                        }
                    }
                    break;

                case "report":
                    {
                        reports.Clear();
                        while (db_documents.Read())
                        {

                            Classes.rerportclass newBot = new Classes.rerportclass();
                            newBot.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                            newBot.count_msg = Convert.ToInt32(db_documents.GetValue(1).ToString());
                            newBot.count_unic_users = Convert.ToInt32(db_documents.GetValue(2).ToString());
                            newBot.duration = db_documents.GetValue(3).ToString();
                            newBot.count_timeout = Convert.ToInt32(db_documents.GetValue(4).ToString());
                            newBot.count_ban = Convert.ToInt32(db_documents.GetValue(5).ToString());
                            newBot.msgs_per_min = Convert.ToInt32(db_documents.GetValue(6).ToString());
                            newBot.count_subs = Convert.ToInt32(db_documents.GetValue(7).ToString());
                            newBot.top_slovo = db_documents.GetValue(8).ToString();
                            newBot.top_user = db_documents.GetValue(9).ToString();
                            newBot.date = db_documents.GetValue(10).ToString();
                            newBot.time = db_documents.GetValue(11).ToString();

                            reports.Add(newBot);
                        }
                    }
                    break;
                case "commands":
                    {
                        commands.Clear();
                        while (db_documents.Read())
                        {

                            Classes.CommandsClass newBot = new Classes.CommandsClass();
                            newBot.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                            newBot.text = Convert.ToString(db_documents.GetValue(1).ToString());
                            newBot.header = Convert.ToString(db_documents.GetValue(2).ToString());
                            newBot.type = Convert.ToString(db_documents.GetValue(3).ToString());
                            newBot.tag = Convert.ToString(db_documents.GetValue(4).ToString());
                            newBot.hotkey = Convert.ToString(db_documents.GetValue(5).ToString());


                            commands.Add(newBot);
                        }
                    }
                    break;
                case "moderators":
                    {
                        moderation.Clear();
                        while (db_documents.Read())
                        {

                            Classes.ModeratorsClass newBot = new Classes.ModeratorsClass();
                            newBot.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                            newBot.username = Convert.ToString(db_documents.GetValue(1).ToString());
                            newBot.type = Convert.ToString(db_documents.GetValue(2).ToString());
                            newBot.duration = Convert.ToString(db_documents.GetValue(3).ToString());
                            newBot.date = Convert.ToString(db_documents.GetValue(4).ToString());
                            newBot.time = Convert.ToString(db_documents.GetValue(5).ToString());
                            newBot.description = Convert.ToString(db_documents.GetValue(6).ToString());
                            newBot.session = Convert.ToInt32(db_documents.GetValue(7).ToString());


                            moderation.Add(newBot);
                        }
                    }
                    break;
            }
            db_documents.Close();


        }
        public MySqlDataReader Connection(string query)
        {
            try
            {
                MySqlConnection.ClearPool(MYconnect);
                MYconnect.Close();
                MYconnect.Open();

                MySqlCommand command = new MySqlCommand(query, MYconnect);
                MySqlDataReader reader = command.ExecuteReader();

                return reader;
            }
            catch
            {
                return null;
            }

        }


        #endregion


        // Twitch Methods
        #region
        private void Client_OnModeratorLeft(object sender, OnModeratorLeftArgs e)
        {
            //moderators.RemoveAll(x => x == e.Username);
            //Pages.Main.main.mainfuncpage.GenerateModers();
        }
        private void Client_OnModeratorJoined(object sender, OnModeratorJoinedArgs e)
        {
           // moderators.Add(e.Username);
           // Pages.Main.main.mainfuncpage.GenerateModers();
        }
        // Бот читает КОМАНДЫ
        private void Client_OnChatCommandReceived(object sender, TwitchLib.Client.Events.OnChatCommandReceivedArgs e)
        {
            //switch (e.Command.CommandText.ToLower())
            //{
            //    case "тест":
            //                  client.SendMessage(channelname, "Good");
            //        break;
            //}
        }
        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            subsStream++;
        }
        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            joinedusers++;
        }
        private void Client_OnUserBanned(object sender, OnUserBannedArgs e)
        {
            bansstream++;

            string date = GenerateDate();
            string time = GenerateTime();
            string reason = "";
            if (e.UserBan.BanReason == null) reason = "None";
            else reason = e.UserBan.BanReason;
            Connection(SqlHelp.InsterSql(new string[] {
                tabels.moderators.ToString(), idBanMods.ToString(), e.UserBan.Username, "Ban",
                "Permanent", date, time, reason, actualbansession.ToString()
            }));
            idBanMods++;
        }
        private void Client_OnUserTimedout(object sender, OnUserTimedoutArgs e)
        {
            
            timeoutedstream++;
            string date = GenerateDate();
            string time = GenerateTime();
            string reason = "";
            if (e.UserTimeout.TimeoutReason == null) reason = "None";
            else reason = e.UserTimeout.TimeoutReason;
            Connection(SqlHelp.InsterSql(new string[] {
                tabels.moderators.ToString(), idBanMods.ToString(), e.UserTimeout.Username.ToString(), "Timeout",
                e.UserTimeout.TimeoutDuration.ToString(), date, time, reason,
                actualbansession.ToString()
            }));
            idBanMods++;
        }
        private void Client_OnMessageCleared(object sender, OnMessageClearedArgs e)
        {
            string date = GenerateDate();
            string time = GenerateTime();
            Connection(SqlHelp.InsterSql(new string[] {
                tabels.moderators.ToString(), idBanMods.ToString(), e.Channel.ToString(), "Deleted",
                "0", date, time, "Deleted",
                actualbansession.ToString()
            }));
            idBanMods++;
        }
        private void Localtimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            localcounttick--;
            if(localcounttick == 0)
            {
                findtogive = false;
                localtimer.Stop();
                switch (moment)
                {
                    case "1":
                        if(localgivelist.Count > 4)
                        {
                            while(localwinners.Count != 5)
                            {
                                Random rnd = new Random();
                                int winer = rnd.Next(0, localgivelist.Count);
                                var est = localwinners.Find(x => x == localgivelist[winer]);
                                if (est == null) localwinners.Add(localgivelist[winer]);

                                
                            }
                            client.SendMessage(channelname, "Время вышло!");
                            client.SubscribersOnlyOn(channelname);
                            string msgtotw = "";
                            for (int i = 0; i < localwinners.Count; i++)
                            {
                                if (i != localwinners.Count - 1) msgtotw += localwinners[i] + ", ";
                                else msgtotw += localwinners[i];
                            }
                            client.SendMessage(channelname, msgtotw);
                            localcounttick = 2;
                            moment = "2";
                            localtimer.Start();
                        }
                        else
                        {
                            client.SendMessage(channelname, "Недостаточно участников");
                            localcounttick = 0;
                            moment = "1";
                            localwinners.Clear();
                            localgivelist.Clear();
                            localgive = false;
                            findtogive = false;
                            localtimer.Stop();
                        }
                        break;
                    case "2":
                        {
                            Random rnd = new Random();
                            int rndwin = rnd.Next(0, localwinners.Count);
                            localwinners.RemoveAt(rndwin);
                            string msgtotw = "";
                            for (int i = 0; i < localwinners.Count; i++)
                            {
                                if (i != localwinners.Count - 1) msgtotw += localwinners[i] + ", ";
                                else msgtotw += localwinners[i];
                            }
                            client.SendMessage(channelname, msgtotw);

                            localcounttick = 2;
                            moment = "3";
                            localtimer.Start();
                        }
                        break;
                    case "3":
                        {
                            Random rnd = new Random();
                            int rndwin = rnd.Next(0, localwinners.Count);
                            localwinners.RemoveAt(rndwin);
                            string msgtotw = "";
                            for (int i = 0; i < localwinners.Count; i++)
                            {
                                if (i != localwinners.Count - 1) msgtotw += localwinners[i] + ", ";
                                else msgtotw += localwinners[i];
                            }
                            client.SendMessage(channelname, msgtotw);

                            localcounttick = 2;
                            moment = "4";
                            localtimer.Start();
                        }
                        break;
                    case "4":
                        {
                            Random rnd = new Random();
                            int rndwin = rnd.Next(0, localwinners.Count);
                            localwinners.RemoveAt(rndwin);
                            string msgtotw = "";
                            for (int i = 0; i < localwinners.Count; i++)
                            {
                                if (i != localwinners.Count - 1) msgtotw += localwinners[i] + ", ";
                                else msgtotw += localwinners[i];
                            }
                            client.SendMessage(channelname, msgtotw);

                            localcounttick = 2;
                            moment = "5";
                            localtimer.Start();
                        }
                        break;
                    case "5":
                        {
                            Random rnd = new Random();
                            int rndwin = rnd.Next(0, localwinners.Count);
                            localwinners.RemoveAt(rndwin);
                            client.SendMessage(channelname, localwinners[0] + " - Победитель!");
                            client.SubscribersOnlyOff(channelname);
                            localcounttick = 0;
                            moment = "1";
                            localwinners.Clear();
                            localgivelist.Clear();
                            localgive = false;
                            findtogive = false;
                            localtimer.Stop();
                            maskGiveAway = false;

    }
                        break;
                }
            }
        }
        public void SetColorChat()
        {
            if (actualColor == 10) actualColor = 1;
            else actualColor++;
            switch (actualColor)
            {
                case 1: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.Chocolate); break;
                case 2: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.Green); break;
                case 3: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.GoldenRod); break;
                case 4: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.Firebrick); break;
                case 5: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.YellowGreen); break;
                case 6: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.CadetBlue); break;
                case 7: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.DodgerBlue); break;
                case 8: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.SpringGreen); break;
                case 9: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.Coral); break;
                case 10: client.ChangeChatColor(channelname, TwitchLib.Client.Enums.ChatColorPresets.Blue); break;

            }



        }
        public void MostQuestion(string msg, string chatId)
        {
            if(msg == "!настройки" | msg == "!settings")
            {
                client.SendReply(channelname, chatId, "https://clips.twitch.tv/BitterCourageousHorseradishGivePLZ-Fz6s7zE5LEkRV6gA");
                return;
            }
            if (msg.ToLower().Contains("сборка") | msg.ToLower().Contains("руководство") | msg.ToLower().Contains("гайд") 
                | msg.ToLower().Contains("сборку"))
            {
                if (msg.ToLower().Contains("наге") | msg.ToLower().Contains("нага") | msg.ToLower().Contains("нагу"))
                {
                    client.SendReply(channelname, chatId, "Руководство по игре на наге от стримера - https://steamcommunity.com/sharedfiles/filedetails/?id=2915454065");
                    return;
                }
            }
            if (msg.ToLower().Contains("трек") | msg.ToLower().Contains("песня") | msg.ToLower().Contains("музык"))
            {
                if(msg.ToLower().Contains("донат") | msg.ToLower().Contains("подписк") | msg.ToLower().Contains("саб"))
                {
                    client.SendReply(channelname, chatId, "Трек с доната/сабки - " +
                        "△Sco△ Current track: x Sensi - Die letzte Sonnex Sensi - Die letzte Sonne");
                    return;
                }
            }
            if (msg.ToLower().Contains("плейлист") | msg.ToLower().Contains("плэйлист"))
            {
                if(msg.ToLower().Contains("где") | msg.ToLower().Contains("его")
                    | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendReply(channelname, chatId, 
                        "Плейлист в закрепе тг https://t.me/witchblvdes ");
                    return;
                }
            }
            if (msg.ToLower().Contains("настройки") | msg.ToLower().Contains("настройками") | msg.ToLower().Contains("бинд"))
            {
                if (msg.ToLower().Contains("клип") | msg.ToLower().Contains("где") 
                    | msg.ToLower().Contains("его") | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendReply(channelname, chatId,
                        "https://clips.twitch.tv/BitterCourageousHorseradishGivePLZ-Fz6s7zE5LEkRV6gA");
                    return;
                }
            }
            if (msg.ToLower().Contains("переехал") | msg.ToLower().Contains("переезд"))
            {
                if (msg.ToLower().Contains("ты") | msg.ToLower().Contains("куда")
                    | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendReply(channelname, chatId,
                        "Стример не переехал, снял вторую квартиру для стримов");
                    return;
                }
            }
            if (msg.ToLower().Contains("универ") | msg.ToLower().Contains("учился"))
            {
                if (msg.ToLower().Contains("где") |  msg.ToLower().Contains("стример")
                    | msg.ToLower().Contains("ром") | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendMessage(channelname,
                        "Стример закончил КНУ им.Шевченка. Программирование начал изучать в 9 классе.");
                    return;
                }
            }
            if (msg.ToLower().Contains("работа"))
            {
                if (msg.ToLower().Contains("стример") |  msg.ToLower().Contains("рома")
                    | msg.ToLower().Contains("ром") | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendMessage(channelname,
                        "Стример работает в GameDev кампании. По условиям контракта, свою ЗП и саму кампанию он не разглашает");
                    return;
                }
            }
            if (msg.ToLower().Contains("зарабатывае") | msg.ToLower().Contains("зарплата"))
            {
                if (msg.ToLower().Contains("какая") | msg.ToLower().Contains("сколько") 
                    | msg.ToLower().Contains("ром") | msg.ToLower().Contains("стример")
                    | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendMessage(channelname,
                        "По условиям контракта, свою ЗП и саму кампанию он не разглашает");
                    return;
                }
            }
            if (msg.ToLower().Contains("сколько") && msg.ToLower().Contains("лет"))
            {
                if (msg.ToLower().Contains("тебе") | msg.ToLower().Contains("ему")
                    | msg.ToLower().Contains("ром") | msg.ToLower().Contains("стример")
                    | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendReply(channelname, chatId,
                        "Стримеру 24.");
                    return;
                }
            }
            if (msg.ToLower().Contains("живешь") | msg.ToLower().Contains("район"))
            {
                if (msg.ToLower().Contains("в каком") | msg.ToLower().Contains("где")
                    
                    | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendReply(channelname, chatId,
                        "Стример живет в Одессе. Более точный адресс не расскажет!");
                    return;
                }
            }
            if (msg.ToLower() == ("!mmr") | msg.ToLower() == ("!medal"))
            {
                client.SendReply(channelname, chatId,
                        "Сейчас у стримера +-5к птс. Макс птс - 7к");
            }
                if (msg.ToLower().Contains("птс") | msg.ToLower().Contains("ммр") | msg.ToLower().Contains("mmr"))
            {
                if (msg.ToLower().Contains("сколько") | msg.ToLower().Contains("макс")

                    | msg.ToLower().Contains("witchblvde"))
                {
                    client.SendReply(channelname, chatId,
                        "Сейчас у стримера +-5к птс. Макс птс - 7к");
                    return;
                }
            }

        }
        

        // Бот читает каждое сообщение
        private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
        {

            // Загрузка в базу
            if (channelname == "witchblvde")
            {
                try
                {
                    string dat = GenerateDate();
                    string time = GenerateTime();
                    string msg = e.ChatMessage.Message.Replace("'", "");
                    var end = Connection(SqlHelp.InsterSql(new string[] {
                        "messages", CountIndex.ToString(), e.ChatMessage.Username, msg, dat, time
                        }));
                    CountIndex++;

                }
                catch
                {
                    counterrors++;
                }
            }
            
            // Бан и таймаут
            if (e.ChatMessage.Username == myName)
            {
                string[] mas = e.ChatMessage.Message.Split(' ');
                if (mas[0] == "!таймаут")
                {
                    TimeOutUser(mas[1], 200);
                }
                if(mas[0] == "!бан")
                {
                    client.BanUser(channelname, mas[1]);
                }
            }

            // Отдать бота
            if (e.ChatMessage.Username == ActualName && e.ChatMessage.Message.Contains("!отдать"))
            {
                string[] mas = e.ChatMessage.Message.Split(' ');
                myName = mas[1];
            }

            

            // Загрузка в локальную базу и ответы
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {

                // Количество сообщений
                if (customfuncs.cbcountmsgs.IsChecked == true && e.ChatMessage.Message == "!сообщения")
                    {
                        //SELECT COUNT(*) FROM `messages` WHERE username = 'ba4ebar'
                        MySqlDataReader db_documents = MainWindow.mainwindow.Connection($"SELECT COUNT(*) FROM `messages` WHERE username = '{e.ChatMessage.Username}'");
                        int count = 0;
                        while (db_documents.Read())
                        {
                            count = Convert.ToInt32(db_documents.GetValue(0).ToString());

                        }
                        client.SendMessage(channelname, e.ChatMessage.Username + ", у тебя " + count + " сообщений. Стата не точная");
                    }

                // Поменять цвет
                if (customfuncs.cbcolornick.IsChecked == true && e.ChatMessage.Username == myName) SetColorChat();

                // Гугл
                if (customfuncs.cbgoogleit.IsChecked == true && e.ChatMessage.Message.Contains("!гугл"))
                    {
                        string[] mas = e.ChatMessage.Message.Split(' ');
                        if (mas[0] == "!гугл" && mas.Length > 1)
                        {
                            string target = "";
                            for (int i = 1; i < mas.Length; i++)
                            {
                                if (i == 1) target += mas[i];
                                else target += " " + mas[i];
                            }
                            string otvet = GoogleIt(target);
                            if (otvet != "" && CheckBanWord(otvet) == false) client.SendReply(channelname, e.ChatMessage.Id, otvet);
                        }
                    }

                // Найти трек
                if (customfuncs.cbfindtrack.IsChecked == true && e.ChatMessage.Message.Contains("!трек") && findTrack == true)
                {
                    findTrack = false;
                    FindMus(e.ChatMessage.Id);
                }

                // Стата героя
                if (customfuncs.cbstatehero.IsChecked == true && e.ChatMessage.Message.Contains("!стата"))
                    {
                        string[] mas12 = e.ChatMessage.Message.Split(' ');
                        if (mas12[0] == "!стата")
                        {
                            if (mas12.Length == 2 | mas12.Length == 3)
                            {


                                string msg1 = "";
                                if (mas12.Length == 3) msg1 = ParserHeroes(mas12[1] + " " + mas12[2]);
                                else msg1 = ParserHeroes(mas12[1]);
                                if (msg1 != "") client.SendMessage(channelname, msg1);


                            }
                        }
                    }

                // Команда !ласт 
                if (customfuncs.cblastgame.IsChecked == true && e.ChatMessage.Message.Contains("!ласт"))
                    {
                        try
                        {
                            games.Clear();
                            ParserProfileDotaBuff();
                            client.SendReply(channelname, e.ChatMessage.Id, games[0].hero + ", Результат - " + games[0].result
                                + ", КДА - " + games[0].KDA + ". Информация берется с основного аккаунта Ромы.");
                        }
                        catch
                        {
                            client.SendReply(channelname, e.ChatMessage.Id, "Не удалось получить результат. Мой автор хуесос");
                        }

                    }

                // Команда !wl 
                if (customfuncs.cbwinlose.IsChecked == true && e.ChatMessage.Message.Contains("!wl"))
                    {
                        try
                        {
                            games.Clear();
                            ParserProfileDotaBuff();
                            int countwinsdf = 0;
                            for (int i = 0; i < 10; i++) if (games[i].result != "Поражение") countwinsdf++;
                            countwinsdf = countwinsdf * 10;
                            client.SendReply(channelname, e.ChatMessage.Id, "Винрейт за последние 10 игр на " +
                                "основном аккаунте - " + countwinsdf + "%");
                        }
                        catch
                        {
                            client.SendReply(channelname, e.ChatMessage.Id, "Не удалось получить результат. Мой автор хуесос");
                        }
                    }

                // Функция Кто ты
                if (customfuncs.cbwhoareyou.IsChecked == true && e.ChatMessage.Username == myName && e.ChatMessage.Message.Contains("!ктоты"))
                    {
                        try
                        {
                            Random rnd = new Random();
                            Random rndnas = new Random();
                            Random rndZnak = new Random();

                            string[] mas = e.ChatMessage.Message.Split(' ');
                            string answer = mas[1] + " -";
                            Random randWords = new Random();
                            int words = randWords.Next(5, 11);
                            for (int i = 0; i < words; i++)
                            {

                                int numRnd = rnd.Next(0, chatmessage.Count);
                                string[] randMsg = chatmessage[numRnd].message.Split(' ');

                                int numrndnas = rndnas.Next(0, randMsg.Length);
                                if (!answer.Contains(randMsg[numrndnas]) && randMsg[numrndnas].Length > 1)
                                {
                                    answer += " " + randMsg[numrndnas];

                                    int numRndZnak = rndZnak.Next(0, 10);
                                    if (numRndZnak % 3 == 0) answer += ",";
                                }
                                else i--;
                            }
                            client.SendMessage(channelname, answer);
                        }
                        catch
                        {
                            client.SendMessage(channelname, "@ba4ebar, пошел нахуй, иди код переделывать");
                        }
                    }

                // Уведомления о теге
                if (customfuncs.cbtagme.IsChecked == true && e.ChatMessage.Message.Contains(myName))
                    {
                        var windowBox = new ToastContentBuilder();
                        windowBox.AddText("Тебя тегнул " + e.ChatMessage.Username);
                        windowBox.Show();
                    }

                // Частые вопросы
                if(customfuncs.cbMostQuestions.IsChecked == true) MostQuestion(e.ChatMessage.Message, e.ChatMessage.Id);


                // Загрузка в локальную базу
                #region
                Classes.MessageClass msg = new MessageClass();
                msg.username = e.ChatMessage.Username;
                msg.message = e.ChatMessage.Message;
                msg.date = GenerateDate();
                msg.time = GenerateTime();
                //     0          1              2        3         4    // LEN = 5
                // "ba4ebar|всем привет я | новенький|21.10.2022|23:10"
                StreamWriter wrt = new StreamWriter(localpath + "/sessionchat.txt", true);
                wrt.WriteLine(msg.username + "|" +
                              msg.message + "|" +
                              msg.date + "|" +
                              msg.time + "|");
                wrt.Close();

                #endregion

                // Запись чата
                #region
                chatmessage.Add(msg);
                if (e.ChatMessage.IsModerator) mainfuncpage.WriteMessage(msg, true);
                else mainfuncpage.WriteMessage(msg, false);
                #endregion



            }));

            // Бан всех кто писал что-то определенное
            if (e.ChatMessage.Username == myName && e.ChatMessage.Message.Contains("!убратьвсех"))
            {
                try
                {       // 0      1          n              count-2 count-1
                    //!убратьвсех кто сказал что это не так 600     1000
                    string str = "";
                    string[] mas = e.ChatMessage.Message.Split(' ');
                    int count = Convert.ToInt32(mas[mas.Length - 1]);
                    int duration = Convert.ToInt32(mas[mas.Length - 2]);
                    for (int i = 1; i < mas.Length - 2; i++)
                    {
                        if (i != mas.Length - 3) str += mas[i] + " ";
                        else str += mas[i];
                    }
                    int countBansInline = 0;
                    for (int i = chatmessage.Count - 1; i > -1; i--)
                    {
                        if (count > 0)
                        {
                            if (chatmessage[i].message.Contains(str))
                            {
                                TimeOutUser(chatmessage[i].username, duration);
                                countBansInline++;
                            }
                            count--;
                        }
                        else break;
                    }
                    client.SendMessage(channelname, "На " + duration + " сек. ушли в отпуск " + countBansInline + " юзеров");
                }
                catch
                {
                    client.SendMessage(channelname, "@ba4ebar, иди нахуй, формулировка не та");
                }
            }

            // Снять последние ограничения
            if (e.ChatMessage.Username == myName && e.ChatMessage.Message.Contains("!убратьограничение"))
            {
                //!убратьограничение curr all
                //!убратьограничение curr 100
                try
                {
                    string[] mas = e.ChatMessage.Message.Split(' ');
                    if (mas.Length == 3)
                    {
                        int sessi = 0;
                        if (mas[1] == "curr") sessi = actualbansession;
                        else sessi = Convert.ToInt32(mas[1]);

                        int count = 0;
                        bool all = false;

                        if (mas[2] == "all") all = true;
                        else
                        {
                            all = false;
                            count = Convert.ToInt32(mas[2]);
                        }
                        LoadData(tabels.moderators);
                        for (int i = moderation.Count - 1; i > -1; i--)
                        {
                            if (moderation[i].session == sessi)
                            {
                                if (all)
                                {
                                    if (moderation[i].type == "Timeout") client.SendMessage(channelname, "/untimeout " + moderation[i].username);
                                    if (moderation[i].type == "Ban") client.SendMessage(channelname, "/unban " + moderation[i].username);
                                }
                                else
                                {
                                    if (count > -1)
                                    {
                                        if (moderation[i].type == "Timeout") client.SendMessage(channelname, "/untimeout " + moderation[i].username);
                                        if (moderation[i].type == "Ban") client.SendMessage(channelname, "/unban " + moderation[i].username);
                                    }
                                    else break;
                                    count--;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    client.SendMessage(channelname, "@ba4ebar, иди нахуй, формулировка не та");
                }
            }

            // Отсановка розыгрыша
            if (e.ChatMessage.Username == myName && e.ChatMessage.Message == "!стоп")
            {
                localcounttick = 0;
                moment = "1";
                localwinners.Clear();
                localgivelist.Clear();
                localgive = false;
                findtogive = false;
                localtimer.Stop();
            }

            // Отправка команд из базы
            if (e.ChatMessage.Username == myName)
            {
                string headercomm = "";
                bool needcom = false;
                foreach (var item in commands)
                {
                    if (e.ChatMessage.Message.Contains("!" + item.header))
                    {
                        headercomm = item.text;
                        needcom = true; 
                        break;
                    }
                }
                if (needcom)
                {
                    int countmsgs = 1;
                    string[] mas = e.ChatMessage.Message.Split(' ');
                    if (NumberOrNot(mas[mas.Length - 1])) countmsgs = Convert.ToInt32(mas[mas.Length - 1]);
                    for (int i = 0; i < countmsgs; i++)
                    {
                        client.SendMessage(channelname, headercomm);
                    }

                }
            }

            // Заполнение участников розыгрыша
            if (findtogive)
            {
                // 76561198990303751
                var n = localgivelist.Find(x => x == e.ChatMessage.Username);
                if (n == null)
                {
                    if (maskGiveAway)
                    {
                        if (e.ChatMessage.Message.Length == 18 | e.ChatMessage.Message.Length == 17)
                        {
                            if (NumberOrNot(e.ChatMessage.Message))
                                localgivelist.Add(e.ChatMessage.Username);

                        }
                    }
                    else localgivelist.Add(e.ChatMessage.Username);
                }
            }

            // Старт розыгрыша
            if (e.ChatMessage.Username == myName && e.ChatMessage.Message.Contains("!розыгрыш") && localgive == false)
            {
                try
                {
                    findtogive = true;
                    localgive = true;
                    string[] mas = e.ChatMessage.Message.Split(' ');
                    if (mas.Length == 3) maskGiveAway = true;
                    else maskGiveAway = false;
                    localcounttick = Convert.ToInt32(mas[1]);
                    localtimer.Start();
                    if(maskGiveAway)client.SendMessage(channelname, "Стартовал розыгрыш! Для участия нужно написать свой SteamID." +
                        "Спамить не надо, учитывается только одно сообщение. Конец через " + localcounttick + " сек.");
                    else client.SendMessage(channelname, "Стартовал розыгрыш! Для участия нужно 1 сообщение после этого." +
                        "Спамить не надо, учитывается только одно сообщение. Конец через " + localcounttick + " сек.");
                    
                }
                catch
                {
                    localcounttick = 0;
                    moment = "1";
                    localwinners.Clear();
                    localgivelist.Clear();
                    maskGiveAway = false;
                    localgive = false;
                    findtogive = false;
                    localtimer.Stop();
                    MessageBox.Show("Не удалось начать розыгрыш");
                }

            }
            
        }
        #endregion


        // Help Methods
        #region  
        public string GoogleIt(string msg1)
        {
            List<string> msg = GoogleItParser(msg1);
            string text = "";
            if (msg != null)
            {
                foreach (var item in msg)
                {
                    if (item.Length > 2)
                    {
                        text += item;
                        if (text.Length > 20) break;
                    }
                }
                bool go = true;
                char[] arr = text.ToCharArray();
                int count = 0;
                string end = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == '(' && count == 0) { go = false; continue; }
                    if (arr[i] == ')' && count == 0) { go = true; count++; continue; }


                    if (go && Char.IsLetter(arr[i]) | arr[i] == ' '
                        | arr[i] == ',' | arr[i] == '-' | arr[i] == '.') end += arr[i];

                }
                for (int i = 0; i < 200; i++)
                {
                    end = end.Replace($"&#{i}", "");
                }
                string[] mas = end.Split('.');

                if (mas.Length == 1) return mas[0];
                else
                {
                    if (mas[0].Length > 100 | mas[0].Length + mas[1].Length > 200)
                    {
                        return mas[0];
                    }
                    else return mas[0] + ". " + mas[1];
                }


            }
            return "";
        }
        public bool CheckBanWord(string msg)
        {
            bool est = false;
            //Проверка
            #region
            if (msg.Contains("негр")) est = true;
            if (msg.Contains("пидар")) est = true;
            if (msg.Contains("пидор")) est = true;
            if (msg.Contains("гей")) est = true;
            if (msg.Contains("девственник")) est = true;
            if (msg.Contains("чурк")) est = true;
            if (msg.Contains("даун")) est = true;
            if (msg.Contains("аутист")) est = true;
            if (msg.Contains("куколд")) est = true;
            if (msg.Contains("хач")) est = true;
            if (msg.Contains("хохол")) est = true;
            if (msg.Contains("ниге")) est = true;
            if (msg.Contains("педик")) est = true;
            #endregion
            return est;
        }
        public List<string> GoogleItParser(string msg)
        {
            try
            {
                string htmlcode = "https://ru.wikipedia.org/wiki/";
                string[] mas = msg.Split(' ');

                for (int j = 0; j < mas.Length; j++)
                {
                    if (j != 0) htmlcode += "_" + mas[j];
                    else htmlcode += mas[j];
                }

                //b_WikipediaGoBigAnswer b_rc_gb_window
                //b_rc_gb_sub b_rc_gb_sub_hero
                //b_rc_gb_sub_cell b_rc_gb_sub_text
                HtmlWeb ws = new HtmlWeb();
                ws.OverrideEncoding = Encoding.UTF8;
                HtmlDocument doc = ws.Load(htmlcode);
                List<string> lis = new List<string>();
                var test = doc.DocumentNode.SelectNodes(
                        "//div[contains(@class, 'mw-parser-output')]" +
                        "//p");
                if (test != null)
                {

                    foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                        "//div[contains(@class, 'mw-parser-output')]" +
                        "//p"))
                    {
                        lis.Add(item.InnerText.ToString());

                    }


                    return lis;
                }
                else return null;
            }
            catch
            {
                return null;
            }
        }
        public void FindMus(string msgid)
        {
            ShazamFolder.MainShazam main = new ShazamFolder.MainShazam();
            main.GenetateMusicAsync(msgid);
            
        }
        public string ParserHeroes(string hero)
        {
            try
            {
                hero = hero.ToLower();
                heroeslist.Clear();
                string htmlcode = "https://ru.dotabuff.com/players/173843946/heroes";
                HtmlWeb ws = new HtmlWeb();
                ws.OverrideEncoding = Encoding.UTF8;
                HtmlDocument doc = ws.Load(htmlcode);


                foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                    "//div[contains(@class, 'content-inner')]" +
                    "//table[contains(@class, 'sortable')]" +
                    "//td"))
                {
                    if (item.InnerText.ToString().Length > 0) heroeslist.Add(item.InnerText.ToString().Replace("&#39;s", ""));

                }
                List<heroesclass> fineded = new List<heroesclass>();
                for (int i = 0; i < heroeslist.Count; i++)
                {
                    try
                    {
                        string a = heroeslist[i];
                        Match match = Regex.Match(a, @"\d\d[.]\d\d[.]\d\d\d\d");
                        if (match.Success)
                        {
                            int leg = heroeslist[i].Length - 10;
                            string newgor = heroeslist[i].Substring(0, leg).ToLower();
                            if (newgor == hero)
                            {

                                return newgor + ", Матчей - " + heroeslist[i + 1] + ", Винрейт - " + heroeslist[i + 2];
                            }
                            else
                            {
                                if (newgor.Contains(hero))
                                {
                                    heroesclass newel = new heroesclass();
                                    newel.name = newgor.ToLower();
                                    newel.winrate = heroeslist[i + 2];
                                    newel.matches = heroeslist[i + 1];
                                    fineded.Add(newel);
                                }
                            }
                        }


                        // return newgor + ", Матчей - " + heroeslist[i + 1] + ", Винрейт - " + heroeslist[i + 2];

                    }
                    catch
                    {
                        return "";
                    }
                }
                if(fineded.Count > 0)
                {
                     return fineded[0].name + ", Матчей - " + fineded[0].matches + ", Винрейт - " + fineded[0].winrate;
                }
                else return "";
            }
            catch
            {
                return "";
            }
        }
        public void ParserProfileDotaBuff()
        {
            string htmlcode = "https://ru.dotabuff.com/players/173843946";
            HtmlWeb ws = new HtmlWeb();
            ws.OverrideEncoding = Encoding.UTF8;
            HtmlDocument doc = ws.Load(htmlcode);
            int select = 0;

            // KDA
            foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                "//div[contains(@class, 'r-table r-only-mobile-5 performances-overview')]" +
                "//div[contains(@class, 'r-row ')]" +
                "//div[contains(@class, 'r-fluid r-125 r-line-graph')]" +
                "//div[contains(@class, 'r-body')]"))
            {
                if (!item.InnerText.ToString().Contains(':'))
                {
                    Classes.Games newgame = new Classes.Games();
                    newgame.KDA = item.InnerText.ToString();
                    games.Add(newgame);

                }
            }


            // Результат 
            foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                "//div[contains(@class, 'r-table r-only-mobile-5 performances-overview')]" +
                "//div[contains(@class, 'r-row ')]" +
                "//div[contains(@class, 'r-fluid r-175 r-text-only r-right r-match-result')]" +
                "//div[contains(@class, 'r-body')]"))
            {
                // 21.01.2022
                int leg = item.InnerText.ToString().Length - 10;
                games[select].result = item.InnerText.ToString().Substring(0, leg);
                select++;

            }
            select = 0;

            //Герой
            foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                "//div[contains(@class, 'r-table r-only-mobile-5 performances-overview')]" +
                "//div[contains(@class, 'r-row ')]" +
                "//div[contains(@class, 'r-fluid r-40 r-icon-text')]" +
                "//div[contains(@class, 'r-body')]"))
            {
                string hero = item.InnerText.ToString().Replace("Рекрут", "").Replace("Страж", "")
                    .Replace("Рыцарь", "").Replace("Герой", "").Replace("Легенда", "").Replace("Властелин", "")
                    .Replace("Божество", "").Replace("Титан", "");
                string[] mas = hero.Split(' ');
                string end = "";
                for (int i = 0; i < mas.Length - 1; i++) end += mas[i] + " ";
                end = end.Substring(0, end.Length - 1);
                games[select].hero = end;
                select++;
            }


        }
        public bool NumberOrNot(string str)
        {
            char[] arr = str.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!Char.IsDigit(arr[i])) return false;
            }
            return true;
        }
        public string GetCurrentStr(int target, string str)
        {
            char[] arr = str.ToCharArray();
            string end = "";
            int dlina = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (i + 1 != arr.Length && i + 1 == target && arr[i + 1] != ' ')
                {
                    end += arr[i] + "-";
                    end += "\n";
                    dlina = 0;
                }
                else
                {
                    end += arr[i];
                    if (dlina == target && i != 0)
                    {
                        end += "\n";
                        dlina = 0;
                    }
                    else dlina++;
                }
            }
            return end;
        }
            // Date Time
            #region
            public string GenerateDate()
            {
                try
                {
                    string date = DateTime.Now.Day + "."
                        + DateTime.Now.Month
                        + "." + DateTime.Now.Year;
                    string[] datemas = date.Split('.');
                    if (datemas[0].Length == 1) datemas[0] = "0" + datemas[0];
                    if (datemas[1].Length == 1) datemas[1] = "0" + datemas[1];
                    return $"{datemas[0]}.{datemas[1]}.{datemas[2]}";
                }
                catch
                {
                    return "null";
                }
            }
            public string RaznizaTime(DateTime timestart, DateTime timeend)
            {
                //00:00:01.123456
                TimeSpan razniza = timeend - timestart;
                return razniza.ToString().Substring(0, 8);
            }
            public string GenerateTime()
            {
                try
                {
                    string time = DateTime.Now.Hour + ":"
                        + DateTime.Now.Minute + ":"
                        + DateTime.Now.Second;
                    string[] timemas = time.Split(':');
                    if (timemas[0].Length == 1) timemas[0] = "0" + timemas[0];
                    if (timemas[1].Length == 1) timemas[1] = "0" + timemas[1];
                    if (timemas[2].Length == 1) timemas[2] = "0" + timemas[2];
                    return $"{timemas[0]}:{timemas[1]}:{timemas[2]}";
                }
                catch
                {
                    return "null";
                }
            }
            #endregion

        #endregion


        // Window
        #region
        private void bordermouseloadibg(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void hotkeys(object sender, KeyEventArgs e)
        {
            string word = e.Key.ToString();
            var est = commands.Find(x => x.hotkey == word && x.type != "Off");


            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && est != null)
            {
                SendMsg(est.text, 1);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (client.IsConnected == true)
            {
                //MessageBox.Show("Бот закончил работу. Нажми ОК для синхраницзации базы");
                if (customfuncs.dontprinreport.IsChecked == false)
                {
                    statisitc.GenerateVanish();
                    int id = reports.Count;
                    string date = GenerateDate();
                    string time = GenerateTime();
                    statisitc.top_slovo = statisitc.top_slovo.Replace("'", "");
                    statisitc.top_user = statisitc.top_user.Replace("'", "");
                    var otc = Connection($"INSERT into report VALUES({id}, {statisitc.count_msg}, " +
                        $"{statisitc.count_unic_users}, '{statisitc.duration}', {statisitc.count_timeout}, " +
                        $"{statisitc.count_ban}, {statisitc.msgs_per_min}, {statisitc.count_subs}, " +
                        $"'{statisitc.top_slovo}', '{statisitc.top_user}', '{date}', '{time}');");
                }
                client.Disconnect();

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
            Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        #endregion

        
    }
}
