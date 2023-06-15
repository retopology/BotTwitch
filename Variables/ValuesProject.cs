using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesModule;

namespace Variables
{
    public class ValuesProject
    {
        // Int
        public static int ActualColor = 0; // Цвет ника 
        public static int TimeoutedStream = 0; // Сколько пользователей отстранили за стрим
        public static int BansStream = 0; // Сколько пользователей забанили за стрим
        public static int CountViewrs = 0;
        public static int ActualBanSession = 0; // ID текущей сессии

        public static int IdBanMods = 0; // 
        public static int IdMessages = 0; // ID последних
        public static int IdCommands = 0; // данных в базе
        public static int IdReports = 0; //


        public static int SubsInStream = 0; // Сабов за стрим


        // Bool
        public static bool CB_COUNT_MSGS = false; // Количество сообщений (!сообщения)
        public static bool CB_COLORED = true; // Цветной ник в чате
        public static bool CB_GOOGLE = false; // Гугл слова (!гугл)
        public static bool CB_FIND_TRACK = false; // Поиск трека (!трек)
        public static bool CB_STATA = false; // Стата персонажа (!стата)
        public static bool CB_LAST_GAME = false; // Послденяя игра (!ласт)
        public static bool CB_WIN_LOSE = false; // Винрейт за последние 10 игр (!wl)
        public static bool CB_TAG_MESSAGE = false; // Уведомление о тэге
        public static bool CB_AUTO_QUESTION = false; // Автоматические ответы
        public static bool CB_COMMANDS_LIST = false; // Списк команд (!команды)
        public static bool NEEDSAVEREPORT = true; // Списк команд (!команды)
        public static bool findTrack = true; // Поиск трека


        // String
        public static string ConnectionString = ""; // Строка подключения к базе
        public static string StreamerName = ""; // Никнейм канала стримера
        public static string DotaBuffUrl = "https://ru.dotabuff.com/players/173843946"; // Ссылка на дотабаф аккаунт
        public static string StreamerId = ""; // ID стримера
        public static string localpath = ""; // ID стримера

        // List
        public static List<MessageClass> AllMessages = new List<MessageClass>(); // Список всех сообщений в базе
        public static List<MessageClass> LocalChat = new List<MessageClass>(); // Локальный чат
        public static List<ModeratorsClass> Moderators = new List<ModeratorsClass>(); // Список действий модераторов
        public static List<CommandsClass> Commands = new List<CommandsClass>(); // Список команд
        public static List<rerportclass> Reports = new List<rerportclass>(); // Список отчетов

        // Class
        public static tokenUser ActualUser = new tokenUser(); // Текущий пользователь

        // DateTime
        public static DateTime startstream;
        

    }
}
