using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using Variables;
using BotModule;
using HttpModule;
using HelpModule;
using ParserModule;

namespace BotModule
{
    public class CustomFuncs
    {
        public static void SendMsg(string str, int count)
        {
            if (!HelpMethods.CheckBanWord(str)) ;
            {
                for (int i = 0; i < count; i++)
                {
                    BotInfo.client.SendMessage(ValuesProject.StreamerName, str);
                }
            }

        }
        public static void SendMsg(string str)
        {
            if (!HelpMethods.CheckBanWord(str)) 
            {

                BotInfo.client.SendMessage(ValuesProject.StreamerName, str);
            }

        }
        public static void SendRpl(string msgid, string msg)
        {
            if (!HelpMethods.CheckBanWord(msg))
            {

                BotInfo.client.SendReply(ValuesProject.StreamerName, msgid, msg);
            }
        }
        public static void SetColorChat()
        {
            if (ValuesProject.ActualColor == 10) ValuesProject.ActualColor = 1;
            else ValuesProject.ActualColor++;
            string color = "";
            switch (ValuesProject.ActualColor)
            {
                case 1: color = "blue"; break;
                case 2: color = "blue_violet"; break;
                case 3: color = "cadet_blue"; break;
                case 4: color = "chocolate"; break;
                case 5: color = "coral"; break;
                case 6: color = "dodger_blue"; break;
                case 7: color = "firebrick"; break;
                case 8: color = "golden_rod"; break;
                case 9: color = "green"; break;
                case 10: color = "hot_pink"; break;
                case 11: color = "orange_red"; break;
                case 12: color = "red"; break;
                case 13: color = "sea_green"; break;
                case 14: color = "spring_green"; break;
                case 15: color = "yellow_green"; break;


            }
            PUT.HttpColorChatAsync(color);



        }
        public static void MostQuestion(string msg, string user)
        {

           
            if (msg == "!настройки" | msg == "!settings")
            {
                SendMsg("https://clips.twitch.tv/BitterCourageousHorseradishGivePLZ-Fz6s7zE5LEkRV6gA");
                return;
            }
            if (msg.ToLower().Contains("сборка") | msg.ToLower().Contains("руководство") | msg.ToLower().Contains("гайд")
                | msg.ToLower().Contains("сборку"))
            {
                if (msg.ToLower().Contains("наге") | msg.ToLower().Contains("нага") | msg.ToLower().Contains("нагу"))
                {
                    SendMsg("Руководство по игре на наге от стримера - https://steamcommunity.com/sharedfiles/filedetails/?id=2915454065");
                    return;
                }
            }
            if (msg.ToLower().Contains("трек") | msg.ToLower().Contains("песня") | msg.ToLower().Contains("музык"))
            {
                if (msg.ToLower().Contains("донат") | msg.ToLower().Contains("подписк") | msg.ToLower().Contains("саб"))
                {
                    SendMsg("Трек с доната/сабки - " +
                        "Marc Acardipane - C.S.W.K.");
                    return;
                }
            }
            if (msg.ToLower().Contains("плейлист") | msg.ToLower().Contains("плэйлист"))
            {
                if (msg.ToLower().Contains("где") | msg.ToLower().Contains("его")
                    | msg.ToLower().Contains("witchblvde"))
                {
                    SendMsg("Плейлист в закрепе тг https://t.me/witchblvdes ");
                    return;
                }
            }
            if (msg.ToLower().Contains("настройки") | msg.ToLower().Contains("настройками") | msg.ToLower().Contains("бинд"))
            {
                if (msg.ToLower().Contains("клип") | msg.ToLower().Contains("где")
                    | msg.ToLower().Contains("его") | msg.ToLower().Contains("witchblvde"))
                {
                    SendMsg("https://clips.twitch.tv/BitterCourageousHorseradishGivePLZ-Fz6s7zE5LEkRV6gA");
                    return;
                }
            }
            if (msg.ToLower().Contains("универ") | msg.ToLower().Contains("учился"))
            {
                if (msg.ToLower().Contains("где") | msg.ToLower().Contains("стример")
                    | msg.ToLower().Contains("ром") | msg.ToLower().Contains("witchblvde"))
                {
                    SendMsg("Стример закончил КНУ им.Шевченка. Программирование начал изучать в 9 классе.");
                    return;
                }
            }
            if (msg.ToLower().Contains("зарабатывае") | msg.ToLower().Contains("зарплата"))
            {
                if (msg.ToLower().Contains("какая") | msg.ToLower().Contains("сколько")
                    | msg.ToLower().Contains("ром") | msg.ToLower().Contains("стример")
                    | msg.ToLower().Contains("witchblvde"))
                {
                    SendMsg("Стример не разглашает информацию о своем заработке");
                    return;
                }
            }
            if (msg.ToLower().Contains("сколько") && msg.ToLower().Contains("лет"))
            {
                if (msg.ToLower().Contains("тебе") | msg.ToLower().Contains("ему")
                    | msg.ToLower().Contains("ром") | msg.ToLower().Contains("стример")
                    | msg.ToLower().Contains("witchblvde"))
                {
                    SendMsg("Стримеру 25.");
                    return;
                }
            }
            if (msg.ToLower().Contains("живешь") | msg.ToLower().Contains("район"))
            {
                if (msg.ToLower().Contains("в каком") | msg.ToLower().Contains("где")

                    | msg.ToLower().Contains("witchblvde"))
                {
                    SendMsg("Стример живет в Одессе. Более точный адресс не расскажет!");
                    return;
                }
            }
            if (msg.ToLower() == ("!mmr") | msg.ToLower() == ("!medal"))
            {
                string rank = DotaBuff.ParseRank();
                if(rank != "") SendMsg("Ранг стримера - " + rank);
                return;
            }
            if (msg.ToLower().Contains("птс") | msg.ToLower().Contains("ммр") | msg.ToLower().Contains("mmr"))
            {
                if (msg.ToLower().Contains("сколько") | msg.ToLower().Contains("макс")

                    | msg.ToLower().Contains("witchblvde"))
                {
                    string rank = DotaBuff.ParseRank();
                    if (rank != "") SendMsg("Ранг стримера - " + rank);
                    return;
                }
            }

        }
    }
}
