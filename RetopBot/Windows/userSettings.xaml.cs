using ClassesModule;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using RetopBot.Classes;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace RetopBot.Windows
{
    /// <summary>
    /// Логика взаимодействия для userSettings.xaml
    /// </summary>
    public partial class userSettings : Window
    {
        MySqlConnection MYconnect;
        string ConnectString = "";
        public bool canExist = true;
        public userSettings()
        {
            InitializeComponent();
            try
            {
                SetBase();
                MySqlDataReader db_documents = Connection($"SELECT * FROM token");
                if (db_documents != null)
                {
                    tokenUser usertok = new tokenUser();
                    while (db_documents.Read())
                    {
                        usertok.username = db_documents.GetValue(0).ToString();
                        usertok.token = db_documents.GetValue(1).ToString();
                        usertok.clientId = db_documents.GetValue(2).ToString();
                        usertok.userId = db_documents.GetValue(3).ToString();

                    }
                    usernameTxt.Text = usertok.username;
                    tokenTxt.Password = usertok.token;
                    clientIdtxt.Password = usertok.clientId;
                    twitchIdTxt.Text = usertok.userId;
                }
                else
                {
                    MYconnect.Close();
                    canExist = false;
                }
            }
            catch
            {
                MYconnect.Close();
                canExist = false;
            }

        }
        public void SetBase()
        {
            ConnectString = $"server={Properties.Settings.Default.server};" +
                    $"user={Properties.Settings.Default.user};" +
                    $"password={Properties.Settings.Default.password};" +
                    $"port={Properties.Settings.Default.port};" +
                    $"database={Properties.Settings.Default.database};";

            MYconnect = new MySqlConnection(ConnectString);
        }
        public MySqlDataReader Connection(string query)
        {
            try
            {
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
        private void dragmee(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void close(object sender, RoutedEventArgs e)
        {
            MYconnect.Close();
            this.Close();
        }

        private void save(object sender, MouseButtonEventArgs e)
        {
            var end = Connection($"UPDATE token SET username = '{usernameTxt.Text}', token = '{tokenTxt.Password}'," +
                $" clientId = '{clientIdtxt.Password}', userId = '{twitchIdTxt.Text}'");
            if(end != null)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Отустствует подключение! Проверьте настройки сети");
            }
        }
    }
}
