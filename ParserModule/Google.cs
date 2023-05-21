using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserModule
{
    public class Google
    {
        public static string GoogleIt(string msg1)
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
        public static List<string> GoogleItParser(string msg)
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
    }
}
