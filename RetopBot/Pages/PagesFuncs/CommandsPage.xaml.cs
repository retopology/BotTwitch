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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace RetopBot.Pages.PagesFuncs
{
    /// <summary>
    /// Логика взаимодействия для CommandsPage.xaml
    /// </summary>
    public partial class CommandsPage : Page
    {
        List<string> tagsCom = new List<string>();
        public CommandsPage()
        {
            InitializeComponent();
            
            GenerateTags();
            GenetateAddTags("");
            GenerateCommads();
            



        }
        int prov = 0;
        public void GenetateAddTags(string str)
        {
            if (prov == 0)
            {

                for (int i = 0; i < tagsCom.Count; i++)
                {
                    tagcb.Items.Add(tagsCom[i]);
                }

                filter_tagcb.Items.Add("Все");
                for (int i = 0; i < tagsCom.Count; i++)
                {
                    filter_tagcb.Items.Add(tagsCom[i]);
                }
                prov++;
            }
            else
            {
                tagcb.Items.Add(str);
                filter_tagcb.Items.Add(str);
            }
        }
        public void GenerateTags()
        {
            for (int i = 0; i < MainWindow.mainwindow.commands.Count; i++)
            {
                if (MainWindow.mainwindow.commands[i].type != "Off")
                {
                    bool est = false;
                    foreach (var item in tagsCom)
                    {
                        if (item == MainWindow.mainwindow.commands[i].tag)
                        {
                            est = true;
                            break;
                        }
                        
                    }
                    if (!est) tagsCom.Add(MainWindow.mainwindow.commands[i].tag);
                }
            }
        }
        public void GenerateCommads()
        {
            try
            {
                commandsparrent.Children.Clear();
                MainWindow.mainwindow.LoadData(MainWindow.tabels.commands);
                GenerateTags();

                for (int i = 0; i < MainWindow.mainwindow.commands.Count; i++)
                {
                    if (MainWindow.mainwindow.commands[i].type != "Off")
                    {
                        bool ecept = false;
                        if (filter_tagcb.SelectedIndex == 0) ecept = true;
                        else
                        {
                            if (filter_tagcb.SelectedValue.ToString() == MainWindow.mainwindow.commands[i].tag) ecept = true;
                            else ecept = false;
                        }
                        if (ecept)
                        {
                            Grid grid = new Grid();
                            grid.Margin = new Thickness(10, 10, 10, 10);
                            BrushConverter cnv = new BrushConverter();
                            grid.Background = (Brush)cnv.ConvertFrom("#FF383838");
                            grid.Height = 50;

                            Label header = new Label();
                            if (MainWindow.mainwindow.commands[i].hotkey == " " | MainWindow.mainwindow.commands[i].hotkey == "")
                                header.Content = MainWindow.mainwindow.commands[i].header;
                            else header.Content = $"({MainWindow.mainwindow.commands[i].hotkey}) " + MainWindow.mainwindow.commands[i].header;
                            header.Foreground = Brushes.White;
                            header.FontSize = 20;
                            header.Margin = new Thickness(5, 5, 5, 0);
                            grid.Children.Add(header);

                            

                            TextBox msg = new TextBox();
                            msg.Height = 35;
                            msg.Text = MainWindow.mainwindow.commands[i].text;
                            msg.Margin = new Thickness(5, 50, 125, 45);
                            msg.BorderBrush = null;
                            msg.Background = Brushes.DarkGray;
                            grid.Children.Add(msg);

                            Orientation orientation = Orientation.Horizontal;
                            StackPanel stc = new StackPanel();
                            stc.Orientation = orientation;
                            stc.HorizontalAlignment = HorizontalAlignment.Right;
                            stc.Margin = new Thickness(5, 5, 5, 5);


                            TextBox txt = new TextBox();
                            txt.Height = 35;
                            txt.Width = 35;
                            txt.HorizontalContentAlignment = HorizontalAlignment.Center;
                            txt.VerticalContentAlignment = VerticalAlignment.Center;
                            txt.VerticalAlignment = VerticalAlignment.Top;
                            txt.HorizontalAlignment = HorizontalAlignment.Right;
                            txt.Margin = new Thickness(0, 0, 5, 0);
                            txt.BorderBrush = null;
                            txt.Text = "1";
                            txt.PreviewTextInput += Txt_PreviewTextInput;
                            stc.Children.Add(txt);

                            Button edit = new Button();
                            edit.Width = 35;
                            edit.Content = "/";
                            edit.HorizontalAlignment = HorizontalAlignment.Right;
                            edit.Height = 35;
                            edit.Margin = new Thickness(0, 0, 5, 0);
                            edit.Tag = 1.ToString();
                            edit.Click += delegate
                            {
                                if (edit.Tag.ToString() == "1")
                                {
                                    grid.Height = 150;
                                    edit.Tag = 2.ToString();
                                }
                                else
                                {
                                    grid.Height = 50;
                                    edit.Tag = 1.ToString();
                                }
                            };
                            stc.Children.Add(edit);

                            Button send = new Button();
                            send.Content = "Send";
                            send.Height = 35;
                            send.Width = 100;
                            send.Background = (Brush)cnv.ConvertFrom("#FFEFFFDC");
                            send.Foreground = Brushes.Black;
                            send.Margin = new Thickness(0, 0, 5, 0);
                            send.Click += delegate
                            {
                                if (txt.Text != "")
                                    MainWindow.mainwindow.SendMsg(msg.Text, Convert.ToInt32(txt.Text));
                                else MainWindow.mainwindow.SendMsg(msg.Text.ToString(), 1);
                            };
                            stc.Children.Add(send);

                            grid.Children.Add(stc);





                            ComboBox tags_par = new ComboBox();
                            tags_par.Height = 25;
                            tags_par.Margin = new Thickness(5, 90, 200, 5);
                            tags_par.BorderBrush = null;
                            tags_par.Background = Brushes.DarkGray;
                            for (int j = 0; j < tagsCom.Count; j++)
                            {
                                tags_par.Items.Add(tagsCom[j]);

                                if (tagsCom[j] == MainWindow.mainwindow.commands[i].tag) tags_par.SelectedIndex = j;
                            }

                            grid.Children.Add(tags_par);



                            TextBox hotkey = new TextBox();
                            hotkey.Margin = new Thickness(0, 105, 75, 20);
                            hotkey.BorderBrush = null;
                            hotkey.Background = Brushes.DarkGray;
                            hotkey.Width = 100;
                            hotkey.HorizontalAlignment = HorizontalAlignment.Right;
                            hotkey.Text = MainWindow.mainwindow.commands[i].hotkey;
                            hotkey.VerticalContentAlignment = VerticalAlignment.Center;
                            hotkey.HorizontalContentAlignment = HorizontalAlignment.Center;
                            hotkey.MaxLength = 1;
                            hotkey.PreviewTextInput += Hotkey_PreviewTextInput;
                            grid.Children.Add(hotkey);

                            Button red = new Button();
                            red.HorizontalAlignment = HorizontalAlignment.Right;
                            red.Width = 100;
                            red.Height = 35;
                            red.Margin = new Thickness(0, 60, 15, 0);
                            red.Tag = MainWindow.mainwindow.commands[i].id.ToString();
                            red.Content = "Edit";
                            red.Click += delegate
                            {
                                string hot = hotkey.Text;
                                if (hot != "") hot = hot.ToUpper();
                                MainWindow.mainwindow.Connection($"UPDATE commands SET text = '{msg.Text}', tag = '{tags_par.SelectedValue.ToString()}', hotkey = '{hot}' WHERE id = " + red.Tag.ToString());
                                GenerateCommads();
                            };
                            grid.Children.Add(red);

                            Button delete = new Button();
                            delete.Height = 25;
                            delete.Width = 50;
                            delete.HorizontalAlignment = HorizontalAlignment.Right;
                            delete.Margin = new Thickness(0, 105, 15, 0);
                            delete.Content = "X";
                            delete.Tag = MainWindow.mainwindow.commands[i].id.ToString();
                            delete.Click += delegate
                            {
                                MainWindow.mainwindow.Connection("UPDATE commands SET type = 'Off' WHERE id = " + delete.Tag.ToString());
                                GenerateCommads();
                            };
                            grid.Children.Add(delete);



                            Label about = new Label();
                            about.Content = "Hotkey (Писать без учета Ctrl, использовать с Ctrl всегда)";
                            about.Margin = new Thickness(160, 130, 0, 0);
                            about.FontSize = 8;
                            about.HorizontalAlignment = HorizontalAlignment.Left;
                            grid.Children.Add(about);


                            commandsparrent.Children.Add(grid);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, лучше перезапустить бота");
            }

        }

        private void Hotkey_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char[] arr = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToArray();
            char[] Arr = Enumerable.Range(0, 32).Select((x, i) => (char)('А' + i)).ToArray();
            bool rus = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToString() == e.Text | Arr[i].ToString() == e.Text)
                {
                    rus = true;
                    break;
                }
            }
            if(rus) e.Handled = true;

        }

        private void Txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!MainWindow.mainwindow.NumberOrNot(e.Text)) e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tagsCom.Add(addtagtxt.Text);
            GenetateAddTags(addtagtxt.Text);
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string keyk = hotkeytxt.Text;
            if (keyk == "") keyk = " ";
            else keyk = keyk.ToUpper();
            int id = MainWindow.mainwindow.commands.Count;
            var end = MainWindow.mainwindow.Connection(SqlHelper.SqlHelp.InsterSql(new string[] {
                MainWindow.tabels.commands.ToString(), id.ToString(),msgtxt.Text, headertxt.Text, "On",
                tagcb.SelectedValue.ToString(), keyk
            }));
            if (end == null) MessageBox.Show("Произошла ошибка, команда не загружена!");
            else GenerateCommads();
        }

        private void textinputaddkey(object sender, TextCompositionEventArgs e)
        {
            char[] arr = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToArray();
            char[] Arr = Enumerable.Range(0, 32).Select((x, i) => (char)('А' + i)).ToArray();
            bool rus = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToString() == e.Text | Arr[i].ToString() == e.Text)
                {
                    rus = true;
                    break;
                }
            }
            if (rus) e.Handled = true;
        }

        private void filerchanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateCommads();
        }

    }
}
