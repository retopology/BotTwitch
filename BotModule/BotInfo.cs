using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Variables;
using DataBaseModule;
using TwitchLib.Client.Models;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using HelpModule;
using TwitchLib.Api.Helix.Models.Moderation.GetModerators;
using ClassesModule;
using TwitchLib.PubSub.Enums;
using HttpModule;
using TwitchLib.Client.Models.Internal;
using TwitchLib.Api.Helix.Models.Chat.Emotes;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using System.Timers;
using TwitchLib.Api.Helix.Models.Search;
using TwitchLib.PubSub;

namespace BotModule
{
    public class BotInfo
    {

        public delegate void MessageSender(MessageClass message, bool track, bool moder);

        public event MessageSender onMessage;



        internal static ConnectData ConnectData;
        private ConnectionCredentials cred;
        internal static TwitchClient client = new TwitchClient();
        public BotInfo(ConnectData _ConnectData)
        {
            ConnectData = _ConnectData;
        }
        public void PushMsgFromWindow(string msgid, string msg)
        {
            CustomFuncs.SendRpl(msgid, msg);
            
        }
        public void SendManyMsgs(string msg, int count)
        {
            CustomFuncs.SendMsg(msg, count);
        }
        public bool GenerateBot()
        {
            try
            {

                ValuesProject.ActualColor = 0;
                ConnectData.FillUserInfo();
                if (ValuesProject.Moderators.Count == 0) ValuesProject.ActualBanSession = 0;
                else ValuesProject.ActualBanSession = ValuesProject.Moderators[ValuesProject.Moderators.Count - 1].session + 1;
                ValuesProject.IdBanMods = ValuesProject.Moderators.Count;

                cred = new ConnectionCredentials(ValuesProject.ActualUser.username, ValuesProject.ActualUser.token);
                
                client.Initialize(cred, ValuesProject.StreamerName);
                client.Connect();
                
                client.OnMessageReceived += Client_OnMessageReceived; // Бот читает каждое сообщение
                client.OnNewSubscriber += Client_OnNewSubscriber;
                client.OnUserTimedout += Client_OnUserTimedout;
                client.OnUserBanned += Client_OnUserBanned;
                client.OnJoinedChannel += Client_OnJoinedChannel;
                //client.OnWhisperReceived
                return true;

            }
            catch
            {
                return false;
            }
        }



        private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            ValuesProject.CountViewrs++;
        }


        private void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
        {
            MessageClass msg = new MessageClass
            {
                id = ValuesProject.IdMessages,
                username = e.ChatMessage.Username,
                message = e.ChatMessage.Message.Replace("'", ""),
                date = HelpMethods.GenerateDate(),
                time = HelpMethods.GenerateTime(),
                userid = e.ChatMessage.UserId,
                streamerNick = ValuesProject.StreamerName,
                hashCode = e.ChatMessage.Id

            };
            ConnectData.InstertSql(msg);
            ValuesProject.IdMessages++;
            
            
            bool NeedTrack = false;
            bool Moderator = e.ChatMessage.IsModerator ? true : false;

            //Buttons.CheckForMove(msg);
            if (msg.username == ValuesProject.ActualUser.username)
            {
                if (msg.message.Contains("!убратьвсех"))CustomCommands.BanWave(msg);
                if (msg.message.Contains("!убратьограничение")) CustomCommands.CancleBanWave(msg);
                if (msg.message.Contains("!стоп")) CustomCommands.StopGiveAway();
                if (msg.message.Contains("!розыгрыш") && GiveAway.localgive == false) CustomCommands.StartGiveAway(msg);
                CustomCommands.IsMyCommand(msg);
                if (ValuesProject.CB_COLORED) CustomFuncs.SetColorChat();
            }
            if(GiveAway.findtogive) CustomCommands.IsGiveAwayMsg(msg,e.ChatMessage.IsSubscriber);
            if(ValuesProject.CB_COUNT_MSGS && msg.message.Contains("!сообщения")) CustomCommands.SendCountMsgs(msg);
            if(ValuesProject.CB_GOOGLE && msg.message.Contains("!гугл")) CustomCommands.SendGoogle(msg);
            if (msg.message.Contains("!трек")) NeedTrack = true;
            if (ValuesProject.CB_STATA && msg.message.Contains("!стата")) CustomCommands.SendHeroStatistic(msg);
            if (ValuesProject.CB_LAST_GAME && msg.message.Contains("!ласт")) CustomCommands.SendLastGame();
            if (ValuesProject.CB_WIN_LOSE && msg.message.Contains("!wl")) CustomCommands.SendWinLose();
            if (ValuesProject.CB_COMMANDS_LIST && msg.message.Contains("!команды")) CustomCommands.SendCommands();
            if (ValuesProject.CB_TAG_MESSAGE && msg.message.Contains(ValuesProject.ActualUser.username)) CustomCommands.IsTagMe(msg);
            if (ValuesProject.CB_AUTO_QUESTION && ValuesProject.StreamerName == "witchblvde") CustomCommands.IsFAQ(msg);
            onMessage(msg, NeedTrack, Moderator);

        }
        private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            ValuesProject.SubsInStream++;
        }
        private void Client_OnUserTimedout(object sender, OnUserTimedoutArgs e)
        {

            ValuesProject.TimeoutedStream++;
            ModeratorsClass moderator = new ModeratorsClass
            {
                 id = 0,
                 username = e.UserTimeout.Username.ToString(),
                 type = "Timeout",
                 duration = e.UserTimeout.TimeoutDuration.ToString(),
                 date = HelpMethods.GenerateDate(),
                 time = HelpMethods.GenerateTime(),
                 description = e.UserTimeout.TimeoutReason == null ? "None" : e.UserTimeout.TimeoutReason,
                 session = ValuesProject.ActualBanSession,
                 streamerNick = ValuesProject.StreamerName



            };
            ConnectData.InstertSql(moderator);
        }
        private void Client_OnUserBanned(object sender, OnUserBannedArgs e)
        {
            ValuesProject.BansStream++;
            ModeratorsClass moderator = new ModeratorsClass
            {
                id = 0,
                username = e.UserBan.Username,
                type = "Permanent",
                duration = "Ban",
                date = HelpMethods.GenerateDate(),
                time = HelpMethods.GenerateTime(),
                description = e.UserBan.BanReason == null ? "None" : e.UserBan.BanReason,
                session = ValuesProject.ActualBanSession,
                streamerNick = ValuesProject.StreamerName



            };
            ConnectData.InstertSql(moderator);

        }
    }
}
