using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpModule
{
    public class HelpMethods
    {
        public static bool NumberOrNot(string str)
        {
            char[] arr = str.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (!Char.IsDigit(arr[i])) return false;
            }
            return true;
        }
        public static string GetCurrentStr(int target, string str)
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
        public static string GenerateDate()
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
        public static string RaznizaTime(DateTime timestart, DateTime timeend)
        {
            //00:00:01.123456
            TimeSpan razniza = timeend - timestart;
            return razniza.ToString().Substring(0, 8);
        }
        public static string GenerateTime()
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
        public static bool CheckBanWord(string msg)
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
    }
}
