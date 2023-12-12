using HtmlAgilityPack;
using ParserModule.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Variables;

namespace ParserModule
{
    public class DotaBuff
    {
        private static List<String> HeroesList = new List<String>();
        private static List<Games> games = new List<Games>();
        public static void SetSite()
        {
            string htmlcode = $"http://192.168.0.3";
            HtmlWeb ws = new HtmlWeb();
            ws.OverrideEncoding = Encoding.UTF8;
            HtmlDocument doc = ws.Load(htmlcode);
            foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                    "//div[contains(@class, 'kek')]"))
            {
                
                HtmlNode div = doc.CreateElement("b");
                div.InnerHtml = "Hello world";
                item.AppendChild(div);
                
                


            }
            
            doc.Save("C:/ospanel/domains/server/index.html");
            doc.LoadHtml(htmlcode);


        }
        public static string ParseRank()
        {
            try
            {

                


                string htmlcode = ValuesProject.DotaBuffUrl;
                HtmlWeb ws = new HtmlWeb();
                ws.OverrideEncoding = Encoding.UTF8;
                HtmlDocument doc = ws.Load(htmlcode);


                var lik = doc.DocumentNode.SelectNodes(
                    "//div[contains(@class, 'header-content-container')]" +
                    "//div[contains(@class, 'header-content')]" +
                    "//div[contains(@class, 'header-content-secondary')]" +
                    "//div[contains(@class, 'rank-tier-wrapper')]").First();

                return lik.Attributes["title"].Value.ToString().Replace("Место: ", "");
                 
                //bool findChar = false;
                //foreach (var item in codeArray)
                //{

                //    if (!findChar)
                //    {

                //    }
                //}


                //string end = "";
                //foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                //    "//div[contains(@class, 'header-content-container')]" +
                //    "//div[contains(@class, 'header-content')]" +
                //    "//div[contains(@class, 'header-content-secondary')]" +
                //    "//div[contains(@class, 'rank-tier-wrapper')]"))
                //{

                //    // end+= item.InnerText.ToString().Replace("Rank: ","");
                //    end += item.Attributes["oldtitle"].Value.ToString().Replace("Rank: ", "");

                //}

                //return lol;
            }
            catch(Exception exp)
            {
                return exp.Message.ToString();
            }
        }

        public static string ParserHeroes(string hero)
        {
            try
            {
                hero = hero.ToLower();
                HeroesList.Clear();
                string htmlcode = $"{ValuesProject.DotaBuffUrl}/heroes";
                HtmlWeb ws = new HtmlWeb();
                ws.OverrideEncoding = Encoding.UTF8;
                HtmlDocument doc = ws.Load(htmlcode);


                foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                    "//div[contains(@class, 'content-inner')]" +
                    "//table[contains(@class, 'sortable')]" +
                    "//td"))
                {
                    
                    if (item.InnerText.ToString().Length > 0) HeroesList.Add(item.InnerText.ToString().Replace("&#39;s", ""));

                }
                List<Hero> fineded = new List<Hero>();
                // Создание списка найденых героев
                for (int i = 0; i < HeroesList.Count; i++)
                {
                    try
                    {
                        string a = HeroesList[i];
                        Match match = Regex.Match(a, @"\d\d[.]\d\d[.]\d\d\d\d");
                        if (match.Success)
                        {
                            int leg = HeroesList[i].Length - 10;
                            string newgor = HeroesList[i].Substring(0, leg).ToLower();
                            if (newgor == hero)
                            {

                                return newgor + ", Матчей - " + HeroesList[i + 1] + ", Винрейт - " + HeroesList[i + 2];
                            }
                            else
                            {
                                if (newgor.Contains(hero))
                                {
                                    Hero newel = new Hero();
                                    newel.name = newgor.ToLower();
                                    newel.winrate = HeroesList[i + 2];
                                    newel.matches = HeroesList[i + 1];
                                    fineded.Add(newel);
                                }
                            }
                        }

                    }
                    catch
                    {
                        return "";
                    }
                }

                if (fineded.Count > 0)
                {
                    return fineded[0].name + ", Матчей - " + fineded[0].matches + ", Винрейт - " + fineded[0].winrate;
                }
                else
                {
                    List<String> noPlayerHero = new List<String>();
                    foreach (HtmlNode item in doc.DocumentNode.SelectNodes(
                    "//div[contains(@class, 'hero-grid')]" +
                    "//div[contains(@class, 'tile-container tile-container-hero')]" +
                    "//div[contains(@class, 'name')]"))
                    {

                        if (item.InnerText.ToString().Length > 0) noPlayerHero.Add(item.InnerText.ToString().Replace("&#39;s", ""));

                    }
                    foreach (var item in noPlayerHero)
                    {
                        if (item.ToLower().Contains(hero.ToLower())) return "У стримера нету игр на " + item;
                    }
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        public static string ParserProfileDotaBuff(bool last)
        {
            try
            {
                games.Clear();
                string htmlcode = ValuesProject.DotaBuffUrl;
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



                    if (hero.Contains(" ") | hero.Contains("-") | hero.Contains("'"))
                    {


                        string[] mas = hero.Split(' ');
                        string end = "";
                        for (int i = 0; i < mas.Length; i++) end += mas[i] + " ";
                        end = end.Substring(0, end.Length - 1);
                        games[select].hero = end;
                        select++;
                    }
                    else
                    {
                        games[select].hero = hero;
                        select++;
                    }




                }
                // Вывод
                if (!last)
                {
                    int countwinsdf = 0;
                    for (int i = 0; i < 10; i++) if (games[i].result != "Поражение") countwinsdf++;
                    countwinsdf = countwinsdf * 10;
                    return "Винрейт за последние 10 игр на основном аккаунте - " + countwinsdf + "%";
                }
                else
                {
                    foreach (var item in games)
                    {
                        if(!item.hero.Contains("Нет героя"))
                            return item.hero + ", Результат - " + item.result
                                + ", КДА - " + item.KDA + ". Информация берется с основного аккаунта Ромы.";
                    }
                    return "";
                    
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }


        }
    }
}
