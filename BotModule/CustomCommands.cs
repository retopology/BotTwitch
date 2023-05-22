using HelpModule;
using HttpModule;
using ParserModule;
using ClassesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TwitchLib.Api.Helix;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using Variables;
using Microsoft.Toolkit.Uwp.Notifications;
using TwitchLib.Client.Models.Internal;

namespace BotModule
{
    public class CustomCommands
    {
        // !роызыгрыш 
        public static void StartGiveAway(MessageClass msg)
        {
            try
            {
                GiveAway.findtogive = true;
                GiveAway.localgive = true;

                string[] mas = msg.message.Split(' ');
                GiveAway.actualGiveAway = new GiveAways();

                for (int i = 2; i < mas.Length; i++)
                {
                    if (mas[i].Contains("int"))
                    {
                        string[] findMsg = mas[i].Split('=');
                        GiveAway.actualGiveAway.TargetInt = findMsg[1];
                        GiveAway.actualGiveAway.IsNumber = true;
                        continue;
                    }
                    if (mas[i].Contains("msg"))
                    {
                        string[] findMsg = mas[i].Split('=');
                        GiveAway.actualGiveAway.TargetMsg = findMsg[1];
                        GiveAway.actualGiveAway.IsString = true;
                        continue;
                    }
                    if (mas[i].Contains("subs"))
                    {
                        GiveAway.actualGiveAway.IsSubs = true;
                        continue;
                    }

                }

                if (mas.Length == 3) GiveAway.maskGiveAway = true;
                else GiveAway.maskGiveAway = false;
                GiveAway.localtimer = new System.Timers.Timer(1000);
                GiveAway.localcounttick = Convert.ToInt32(mas[1]);
                GiveAway.StartGiveAwayTimer();
                string msgToGive = "Стартовал розыгрыш! Для участия нужно написать ";
                if (GiveAway.actualGiveAway.IsString) msgToGive += "слово/фразу - " + GiveAway.actualGiveAway.TargetMsg;
                if (GiveAway.actualGiveAway.IsNumber) msgToGive += " число, которое имеет подобный вид - " + GiveAway.actualGiveAway.TargetInt;
                if (!GiveAway.actualGiveAway.IsNumber && !GiveAway.actualGiveAway.IsString) msgToGive += "1 любое сообщение";
                msgToGive += " .Спамить не надо, учитывается только одно сообщение. Конец через " + GiveAway.localcounttick + " сек.";
                if (GiveAway.actualGiveAway.IsSubs) msgToGive += " Участниками могут быть только платные подписчики";
                CustomFuncs.SendMsg(msgToGive);

            }
            catch
            {
                GiveAway.localcounttick = 0;
                GiveAway.moment = "1";
                GiveAway.localwinners.Clear();
                GiveAway.localgivelist.Clear();
                GiveAway.maskGiveAway = false;
                GiveAway.localgive = false;
                GiveAway.findtogive = false;
                GiveAway.localtimer.Stop();
                CustomFuncs.SendMsg("Не удалось начать розыгрыш");
            }
        }
        // !сообщения
        public static void SendCountMsgs(MessageClass msg)
        {
            string count = BotInfo.ConnectData.GetCountMsgUser(msg.username);
            CustomFuncs.SendMsg(msg.username + ", у тебя " + count + " сообщений. Стата не точная");
        }
        // !гугл
        public static void SendGoogle(MessageClass msg)
        {
            string[] mas = msg.message.Split(' ');
            if (mas[0] == "!гугл" && mas.Length > 1)
            {
                string target = "";
                for (int i = 1; i < mas.Length; i++)
                {
                    if (i == 1) target += mas[i];
                    else target += " " + mas[i];
                }
                string otvet = Google.GoogleIt(target);
                if (otvet != "" && HelpMethods.CheckBanWord(otvet) == false) CustomFuncs.SendMsg(otvet);
            }
        }
        // !стата
        public static void SendHeroStatistic(MessageClass msg)
        {
            string[] mas12 = msg.message.Split(' ');
            if (mas12[0] == "!стата")
            {
                Random rnd = new Random();
                int numRnd = rnd.Next(1, 101);
                if (numRnd == 2)
                {
                    POST.TimeOutUserCustom(60, msg.userid);
                    CustomFuncs.SendMsg(msg.userid + ", вместо ответа ты получаешь таймаут на 1 минуту! Не " +
                        "расстраивайся, ведь шанс на это всего 1%, тебе везет! А если везет, то заходи на GETX по промокоду вичблейда!");
                }
                else
                {
                    if (mas12.Length == 2 | mas12.Length == 3)
                    {


                        string msg1 = "";
                        if (mas12.Length == 3) msg1 = DotaBuff.ParserHeroes(mas12[1] + " " + mas12[2]);
                        else msg1 = DotaBuff.ParserHeroes(mas12[1]);
                        if (msg1 != "") CustomFuncs.SendMsg(msg1);


                    }
                }
            }
        }
        // !ласт
        public static void SendLastGame()
        {
            try
            {
                string end = DotaBuff.ParserProfileDotaBuff(true);
                CustomFuncs.SendMsg(end);

            }
            catch
            {
                CustomFuncs.SendMsg("Не удалось получить результат. Мой автор хуесос");
            }
        }
        // !wl
        public static void SendWinLose()
        {
            try
            {
                string end = DotaBuff.ParserProfileDotaBuff(false);
                CustomFuncs.SendMsg(end);

            }
            catch
            {
                CustomFuncs.SendMsg("Не удалось получить результат. Мой автор хуесос");
            }
        }
        // !команды
        public static void SendCommands()
        {
            CustomFuncs.SendMsg("Команды - ласт, wl, трек, сообщения, гугл + фраза, стата + герой");

        }
        // !убратьвсех
        public static void BanWave(MessageClass msg)
        {
            try
            {       // 0      1          n              count-2 count-1
                    //!убратьвсех кто сказал что это не так 600     1000
                string str = "";
                string[] mas = msg.message.Split(' ');
                int count = Convert.ToInt32(mas[mas.Length - 1]);
                int duration = Convert.ToInt32(mas[mas.Length - 2]);
                for (int i = 1; i < mas.Length - 2; i++)
                {
                    if (i != mas.Length - 3) str += mas[i] + " ";
                    else str += mas[i];
                }
                int countBansInline = 0;
                for (int i = ValuesProject.LocalChat.Count - 1; i > -1; i--)
                {
                    if (count > 0)
                    {
                        if (ValuesProject.LocalChat[i].message.Contains(str))
                        {
                            POST.TimeOutUserCustom(duration, ValuesProject.LocalChat[i].userid);
                            countBansInline++;
                        }
                        count--;
                    }
                    else break;
                }
                CustomFuncs.SendMsg("На " + duration + " сек. ушли в отпуск " + countBansInline + " юзеров");
            }
            catch
            {
                CustomFuncs.SendMsg("@ba4ebar, иди нахуй, формулировка не та");
            }
        }
        // !убратьограничение
        public static void CancleBanWave(MessageClass msg)
        {
            try
            {
                string[] mas = msg.message.Split(' ');
                if (mas.Length == 3)
                {
                    int sessi = 0;
                    if (mas[1] == "curr") sessi = ValuesProject.ActualBanSession;
                    else sessi = Convert.ToInt32(mas[1]);

                    int count = 0;
                    bool all = false;

                    if (mas[2] == "all") all = true;
                    else
                    {
                        all = false;
                        count = Convert.ToInt32(mas[2]);
                    }
                    BotInfo.ConnectData.FillModerators();
                    for (int i = ValuesProject.Moderators.Count - 1; i > -1; i--)
                    {
                        if (ValuesProject.Moderators[i].session == sessi)
                        {
                            if (all)
                            {
                                if (ValuesProject.Moderators[i].type == "Timeout") CustomFuncs.SendMsg("/untimeout " + ValuesProject.Moderators[i].username);
                                if (ValuesProject.Moderators[i].type == "Ban") CustomFuncs.SendMsg("/unban " + ValuesProject.Moderators[i].username);
                            }
                            else
                            {
                                if (count > -1)
                                {
                                    if (ValuesProject.Moderators[i].type == "Timeout") CustomFuncs.SendMsg("/untimeout " + ValuesProject.Moderators[i].username);
                                    if (ValuesProject.Moderators[i].type == "Ban") CustomFuncs.SendMsg("/unban " + ValuesProject.Moderators[i].username);
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
                CustomFuncs.SendMsg("@ba4ebar, иди нахуй, формулировка не та");
            }
        }
        // Уведомления о тэге
        public static void IsTagMe(MessageClass msg)
        {
            var windowBox = new ToastContentBuilder();
            windowBox.AddText("Тебя тегнул " + msg.username);
            windowBox.Show();
        }
        // Часто задаваемые вопросы
        public static void IsFAQ(MessageClass msg)
        {
           CustomFuncs.MostQuestion(msg.message, msg.username);
        }
        // Мои команды
        public static void IsMyCommand(MessageClass msg)
        {
            string headercomm = "";
            bool needcom = false;
            foreach (var item in ValuesProject.Commands)
            {
                if (msg.message.Contains("!" + item.header) && item.type == "On")
                {
                    headercomm = item.text;
                    needcom = true;
                    break;
                }
            }
            if (needcom)
            {
                int countmsgs = 1;
                string[] mas = msg.message.Split(' ');
                if (HelpMethods.NumberOrNot(mas[mas.Length - 1])) countmsgs = Convert.ToInt32(mas[mas.Length - 1]);
                for (int i = 0; i < countmsgs; i++)
                {
                    CustomFuncs.SendMsg(headercomm);
                }

            }
        }
        // Проверка на участие в розыгрыше
        public static void IsGiveAwayMsg(MessageClass msg, bool subscriber)
        {
            var n = GiveAway.localgivelist.Find(x => x == msg.username);
            if (n == null)
            {
                bool exist = true;
                if (GiveAway.actualGiveAway.IsNumber == true)
                {
                    if (!HelpMethods.NumberOrNot(msg.message) | msg.message.Length != GiveAway.actualGiveAway.TargetInt.Length)
                        exist = false;
                }
                if (GiveAway.actualGiveAway.IsString == true && exist == true)
                {
                    if (msg.message != GiveAway.actualGiveAway.TargetMsg)
                        exist = false;
                }
                if (GiveAway.actualGiveAway.IsSubs == true && exist == true)
                {
                    if (subscriber != true)
                        exist = false;
                }
                if (exist)
                {
                    GiveAway.localgivelist.Add(msg.username);
                }

            }
        }
        // Остановка розыгрыша
        public static void StopGiveAway()
        {
            GiveAway.localcounttick = 0;
            GiveAway.moment = "1";
            GiveAway.localwinners.Clear();
            GiveAway.localgivelist.Clear();
            GiveAway.localgive = false;
            GiveAway.findtogive = false;
            GiveAway.localtimer.Stop();
        }
    }
}
