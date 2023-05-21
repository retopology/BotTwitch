using ClassesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Extensions;

namespace BotModule
{
    public class GiveAway
    {

        internal static List<string> localgivelist = new List<string>();
        internal static List<string> localwinners = new List<string>();
        internal static bool localgive = false;
        internal static string moment = "1";
        internal static System.Timers.Timer localtimer = new System.Timers.Timer(1000);
        internal static int localcounttick = 0;
        internal static bool findtogive = false;
        internal static bool maskGiveAway = false;
        internal static GiveAways actualGiveAway = new GiveAways();

        public static void StartGiveAwayTimer()
        {
            localtimer.Elapsed += Localtimer_Elapsed;
            localtimer.Start();
        }

        private static void Localtimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            localcounttick--;
            if (localcounttick == 0)
            {
                findtogive = false;
                localtimer.Stop();
                switch (moment)
                {
                    case "1":
                        if (localgivelist.Count > 4)
                        {
                            while (localwinners.Count != 5)
                            {
                                Random rnd = new Random();
                                int winer = rnd.Next(0, localgivelist.Count);
                                var est = localwinners.Find(x => x == localgivelist[winer]);
                                if (est == null) localwinners.Add(localgivelist[winer]);
                                else
                                {

                                }


                            }
                            CustomFuncs.SendMsg("Время вышло!");
                            //client.SubscribersOnlyOn(channelname);
                            string msgtotw = "";
                            for (int i = 0; i < localwinners.Count; i++)
                            {
                                if (i != localwinners.Count - 1) msgtotw += localwinners[i] + ", ";
                                else msgtotw += localwinners[i];
                            }
                            CustomFuncs.SendMsg(msgtotw);
                            localcounttick = 2;
                            moment = "2";
                            localtimer.Start();
                        }
                        else
                        {
                            CustomFuncs.SendMsg("Недостаточно участников");
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
                            CustomFuncs.SendMsg(msgtotw);

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
                            CustomFuncs.SendMsg(msgtotw);

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
                            CustomFuncs.SendMsg(msgtotw);

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
                            CustomFuncs.SendMsg(localwinners[0] + " - Победитель!");
                            //client.SubscribersOnlyOff(channelname);
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
    }
}
