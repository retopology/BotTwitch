using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchLib.Client.Extensions;

namespace RetopBot.Pages.PagesFuncs
{
    /// <summary>
    /// Логика взаимодействия для mainfunc.xaml
    /// </summary>
    public partial class mainfunc : Page
    {
        public mainfunc()
        {
            InitializeComponent();
        }
        
        
        public void WriteMessage(Classes.MessageClass ms, bool moder)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                
                string endmsg = MainWindow.mainwindow.GetCurrentStr(55, ms.message.ToString());
                
                Grid allchatGrid = new Grid();
                BrushConverter converter = new BrushConverter();
                allchatGrid.Background = (Brush)converter.ConvertFrom("#FF181818");
                allchatGrid.Background = null;
                allchatGrid.Margin = new Thickness(5, 0, 5, 5);

                Label allchatName = new Label();
                allchatName.Content = ms.username;
                if (moder) allchatName.Foreground = Brushes.Green;
                else allchatName.Foreground = Brushes.Red;
                allchatName.FontSize = 16;
                allchatName.Margin = new Thickness(10, 0, 0, 0);

                allchatGrid.Children.Add(allchatName);

                Label allchatMessage = new Label();
                allchatMessage.Content = endmsg;
                allchatMessage.Foreground = Brushes.White;
                allchatMessage.FontSize = 12;
                allchatMessage.Margin = new Thickness(10, 25, 0, 0);

                allchatGrid.Children.Add(allchatMessage);
                allmsgsparrent.Children.Add(allchatGrid);
                if(!allmsgsscroll.IsFocused) allmsgsscroll.ScrollToEnd();
                if (allmsgsparrent.Children.Count == 150) allmsgsparrent.Children.Remove(allmsgsparrent.Children[0]);

               // if (ms.username == "kotoshmyak") GenerateEvaChat(ms, moder);
                if (ms.message.Contains("@" + MainWindow.mainwindow.myName) |
                ms.message.Contains(MainWindow.mainwindow.myName)) GenerateMyChat(ms, moder);

                if (ms.username == nickdudetxt.Text) SearchDudePar(ms, moder);
            }));

        }
        //SearchDudePar
        public void SearchDudePar(Classes.MessageClass ms, bool moder)
        {
            string endmsg = MainWindow.mainwindow.GetCurrentStr(35, ms.message.ToString());

            Grid allchatGrid = new Grid();
            BrushConverter converter = new BrushConverter();
            allchatGrid.Background = (Brush)converter.ConvertFrom("#FF181818");
            allchatGrid.Background = null;
            allchatGrid.Margin = new Thickness(5, 0, 5, 5);

            Label allchatName = new Label();
            allchatName.Content = ms.username;
            if (moder) allchatName.Foreground = Brushes.Green;
            else allchatName.Foreground = Brushes.Red;
            allchatName.FontSize = 16;
            allchatName.Margin = new Thickness(10, 0, 0, 0);

            allchatGrid.Children.Add(allchatName);

            Label allchatMessage = new Label();
            allchatMessage.Content = endmsg;
            allchatMessage.Foreground = Brushes.White;
            allchatMessage.FontSize = 12;
            allchatMessage.Margin = new Thickness(10, 25, 0, 0);

            allchatGrid.Children.Add(allchatMessage);
            searchdude.Children.Add(allchatGrid);
            if (!searchdude_sroll.IsFocused) searchdude_sroll.ScrollToEnd();
            if (searchdude.Children.Count == 150) searchdude.Children.Remove(searchdude.Children[0]);

        }
        //GenerateMyChat
        public void GenerateMyChat(Classes.MessageClass ms, bool moder)
        {
            string endmsg = MainWindow.mainwindow.GetCurrentStr(30, ms.message.ToString());

            Grid allchatGrid = new Grid();
            BrushConverter converter = new BrushConverter();
            allchatGrid.Background = (Brush)converter.ConvertFrom("#FF181818");
            allchatGrid.Background = null;
            allchatGrid.Margin = new Thickness(5, 0, 5, 5);

            Label allchatName = new Label();
            allchatName.Content = ms.username;
            if (moder) allchatName.Foreground = Brushes.Green;
            else allchatName.Foreground = Brushes.Red;
            allchatName.FontSize = 16;
            allchatName.Margin = new Thickness(10, 0, 0, 0);

            allchatGrid.Children.Add(allchatName);

            Label allchatMessage = new Label();
            allchatMessage.Content = endmsg;
            allchatMessage.Foreground = Brushes.White;
            allchatMessage.FontSize = 12;
            allchatMessage.Margin = new Thickness(10, 25, 0, 0);

            allchatGrid.Children.Add(allchatMessage);
            memsgs.Children.Add(allchatGrid);
            if (!memsgs_sroll.IsFocused) memsgs_sroll.ScrollToEnd();
            if (memsgs.Children.Count == 150) memsgs.Children.Remove(memsgs.Children[0]);

        }

        private void getchatmsg(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                MessageBox.Show("da");
            }
        }



        //public void GenerateEvaChat(Classes.MessageClass ms, bool moder)
        //{
        //    string endmsg = MainWindow.mainwindow.GetCurrentStr(30, ms.message.ToString());

        //    Grid allchatGrid = new Grid();
        //    BrushConverter converter = new BrushConverter();
        //    allchatGrid.Background = (Brush)converter.ConvertFrom("#FF181818");
        //    allchatGrid.Background = null;
        //    allchatGrid.Margin = new Thickness(5, 0, 5, 5);

        //    Label allchatName = new Label();
        //    allchatName.Content = ms.username;
        //    if (moder) allchatName.Foreground = Brushes.Green;
        //    else allchatName.Foreground = Brushes.Red;
        //    allchatName.FontSize = 16;
        //    allchatName.Margin = new Thickness(10, 0, 0, 0);

        //    allchatGrid.Children.Add(allchatName);

        //    Label allchatMessage = new Label();
        //    allchatMessage.Content = endmsg;
        //    allchatMessage.Foreground = Brushes.White;
        //    allchatMessage.FontSize = 12;
        //    allchatMessage.Margin = new Thickness(10, 25, 0, 0);

        //    allchatGrid.Children.Add(allchatMessage);
        //    eva_msgs.Children.Add(allchatGrid);
        //    if (!eva_scroll.IsFocused) eva_scroll.ScrollToEnd();

        //}
    }
}
