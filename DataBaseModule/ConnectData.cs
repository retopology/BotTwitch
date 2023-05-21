using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesModule;
using Variables;

namespace DataBaseModule
{
    public class ConnectData
    {
        internal enum Tabels
        {
            messages, commands, report, moderators, token
        }
        internal static MySqlConnection MYconnect;
        public ConnectData()
        {
            MYconnect = new MySqlConnection(ValuesProject.ConnectionString);
        }

        // Запросы в базу данных
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
        public void GetLastIds()
        {
            // Сообщения
            MySqlDataReader db_documents = Connection(
                $"SELECT * FROM {Tabels.messages} ORDER BY ID DESC LIMIT 1");
            while (db_documents.Read())
            {
                int num = Convert.ToInt32(db_documents.GetValue(0).ToString());
                if (num == 0) ValuesProject.IdMessages = 0;
                else ValuesProject.IdMessages = Convert.ToInt32(db_documents.GetValue(0).ToString()) + 1;
            }

            // Баны и таймауты
             db_documents = Connection(
                $"SELECT * FROM {Tabels.moderators} ORDER BY ID DESC LIMIT 1");
            while (db_documents.Read())
            {
                int num = Convert.ToInt32(db_documents.GetValue(0).ToString());
                if (num == 0) ValuesProject.IdBanMods = 0;
                else ValuesProject.IdBanMods = Convert.ToInt32(db_documents.GetValue(0).ToString()) + 1;
            }

            // Команды
            db_documents = Connection(
               $"SELECT * FROM {Tabels.commands} ORDER BY ID DESC LIMIT 1");
            while (db_documents.Read())
            {
                int num = Convert.ToInt32(db_documents.GetValue(0).ToString());
                if (num == 0) ValuesProject.IdCommands = 0;
                ValuesProject.IdCommands = Convert.ToInt32(db_documents.GetValue(0).ToString()) + 1;
            }

            // Отчеты
            db_documents = Connection(
               $"SELECT * FROM {Tabels.report} ORDER BY ID DESC LIMIT 1");
            while (db_documents.Read())
            {
                int num = Convert.ToInt32(db_documents.GetValue(0).ToString());
                if (num == 0) ValuesProject.IdReports = 0;
                ValuesProject.IdReports = Convert.ToInt32(db_documents.GetValue(0).ToString()) + 1;
            }

            // ID сессии
            db_documents = Connection(
               $"SELECT COUNT(*) FROM {Tabels.moderators}");
            while (db_documents.Read())
            {
                int num = Convert.ToInt32(db_documents.GetValue(0).ToString());
                if (num == 0) ValuesProject.ActualBanSession = 0;
                ValuesProject.ActualBanSession = Convert.ToInt32(db_documents.GetValue(0).ToString()) + 1;
            }

            db_documents.Close();

        }
        public void FillMessages()
        {
            MySqlDataReader db_documents = Connection($"SELECT * FROM {Tabels.messages} WHERE streamerNick ='" + ValuesProject.StreamerName + "'");

            ValuesProject.AllMessages.Clear();
            while (db_documents.Read())
            {
                MessageClass newitem = new MessageClass();
                newitem.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                newitem.username = db_documents.GetValue(1).ToString();
                newitem.message = db_documents.GetValue(2).ToString();
                newitem.date = db_documents.GetValue(3).ToString();
                newitem.time = db_documents.GetValue(4).ToString();
                newitem.userid = db_documents.GetValue(5).ToString();
                newitem.streamerNick = db_documents.GetValue(6).ToString();
                ValuesProject.AllMessages.Add(newitem);
            }

            db_documents.Close();


        }
        public void FillModerators()
        {
            MySqlDataReader db_documents = Connection($"SELECT * FROM {Tabels.moderators} WHERE streamerNick ='" + ValuesProject.StreamerName + "'");
            ValuesProject.Moderators.Clear();
            while (db_documents.Read())
            {

                ModeratorsClass newBot = new ModeratorsClass();
                newBot.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                newBot.username = Convert.ToString(db_documents.GetValue(1).ToString());
                newBot.type = Convert.ToString(db_documents.GetValue(2).ToString());
                newBot.duration = Convert.ToString(db_documents.GetValue(3).ToString());
                newBot.date = Convert.ToString(db_documents.GetValue(4).ToString());
                newBot.time = Convert.ToString(db_documents.GetValue(5).ToString());
                newBot.description = Convert.ToString(db_documents.GetValue(6).ToString());
                newBot.session = Convert.ToInt32(db_documents.GetValue(7).ToString());
                newBot.streamerNick = db_documents.GetValue(8).ToString();


                ValuesProject.Moderators.Add(newBot);
            }
            db_documents.Close();
        }
        public void FillCommands()
        {
            MySqlDataReader db_documents = Connection($"SELECT * FROM {Tabels.commands} WHERE streamerNick ='" + ValuesProject.StreamerName + "'");
            ValuesProject.Commands.Clear();
            while (db_documents.Read())
            {

                CommandsClass newBot = new CommandsClass();
                newBot.id = Convert.ToInt32(db_documents.GetValue(0).ToString());
                newBot.text = Convert.ToString(db_documents.GetValue(1).ToString());
                newBot.header = Convert.ToString(db_documents.GetValue(2).ToString());
                newBot.type = Convert.ToString(db_documents.GetValue(3).ToString());
                newBot.tag = Convert.ToString(db_documents.GetValue(4).ToString());
                newBot.hotkey = Convert.ToString(db_documents.GetValue(5).ToString());
                newBot.streamerNick = Convert.ToString(db_documents.GetValue(6).ToString());


                ValuesProject.Commands.Add(newBot);
            }
            db_documents.Close();
        }
        public void FillReports()
        {
            MySqlDataReader db_documents = Connection($"SELECT * FROM {Tabels.report} WHERE streamerNick ='" + ValuesProject.StreamerName + "'");
            ValuesProject.Reports.Clear();
            while (db_documents.Read())
            {

                rerportclass newBot = new rerportclass();
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
                newBot.streamerNick = db_documents.GetValue(12).ToString();


                ValuesProject.Reports.Add(newBot);
            }
            db_documents.Close();
        }
        public void FillUserInfo()
        {
            MySqlDataReader db_documents = Connection($"SELECT * FROM {Tabels.token}");
            while (db_documents.Read())
            {
                tokenUser user = new tokenUser();
                user.username = db_documents.GetValue(0).ToString();
                user.token = db_documents.GetValue(1).ToString();
                user.clientId = db_documents.GetValue(2).ToString();
                user.userId = db_documents.GetValue(3).ToString();
                ValuesProject.ActualUser = user;

            }

            db_documents.Close();
        }
        public string GetCountMsgUser(string username)
        {
            MySqlDataReader db_documents = Connection($"SELECT COUNT(*) FROM `messages` WHERE username = '{username}' AND streamerNick ='{ValuesProject.StreamerName}'");
            int count = 0;
            while (db_documents.Read())
            {
                count = Convert.ToInt32(db_documents.GetValue(0).ToString());

            }
            return count.ToString();
        }
        public void InstertSql(MessageClass message)
        {
            var end = Connection($"INSERT INTO {Tabels.messages} VALUES({ValuesProject.IdMessages}," +
                                                    $"'{message.username}'," +
                                                    $"'{message.message}'," +
                                                    $"'{message.date}'," +
                                                    $"'{message.time}'," +
                                                    $"'{message.userid}'," +
                                                    $"'{message.streamerNick}')");
            if (end != null)
            {
                ValuesProject.LocalChat.Add(message);
                ValuesProject.IdMessages++;
            }


        }
        public void InstertSql(CommandsClass command)
        {
            var end = Connection($"INSERT INTO {Tabels.commands} VALUES({ValuesProject.IdCommands}," +
                                                    $"'{command.text}'," +
                                                    $"'{command.header}'," +
                                                    $"'{command.type}'," +
                                                    $"'{command.tag}'," +
                                                    $"'{command.hotkey}'," +
                                                    $"'{command.streamerNick}')");
            if (end != null) ValuesProject.IdCommands++;

        }
        public void InstertSql(ModeratorsClass moder)
        {
            var end = Connection($"INSERT INTO {Tabels.moderators} VALUES({ValuesProject.IdBanMods}," +
                                                    $"'{moder.username}'," +
                                                    $"'{moder.type}'," +
                                                    $"'{moder.duration}'," +
                                                    $"'{moder.date}'," +
                                                    $"'{moder.time}'," +
                                                    $"'{moder.description}'," +
                                                    $"{moder.session}," +
                                                    $"'{moder.streamerNick}')");
            if (end != null) ValuesProject.IdBanMods++;

        }
        public void InstertSql(rerportclass report)
        {
            var end = Connection($"INSERT INTO {Tabels.report} VALUES({ValuesProject.IdReports}," +
                                                    $"{report.count_msg}," +
                                                    $"{report.count_unic_users}," +
                                                    $"'{report.duration}'," +
                                                    $"{report.count_timeout}," +
                                                    $"{report.count_ban}," +
                                                    $"{report.msgs_per_min}," +
                                                    $"{report.count_subs}," +
                                                    $"'{report.top_slovo}'," +
                                                    $"'{report.top_user}'," +
                                                    $"'{report.date}'," +
                                                    $"'{report.time}'," +
                                                    $"'{report.streamerNick}')");
            if (end != null) ValuesProject.IdReports++;

        }





    }
}
