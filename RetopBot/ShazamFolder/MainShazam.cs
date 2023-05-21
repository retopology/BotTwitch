using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Variables;

namespace RetopBot.ShazamFolder
{
    
    public class MainShazam
    {
        public const string CONFIG_PATH = "Shozom.json";
        public const int IDENTIFY_TIMEOUT = 30000;

        public async void GenetateMusicAsync(string msgid)
        {
            Config.Load(CONFIG_PATH);
            var cancel = new CancellationTokenSource();
            Task.Delay(IDENTIFY_TIMEOUT).ContinueWith((_) => { cancel.Cancel(); });

            var match = await Task.Run(() => Shazam.IdentifyAsync(Config.Object.Device, cancel.Token));
            if (match != null)
            {
                MainWindow.mainwindow.PushMsg(msgid, $"Трек, который щас играет - {match.Artist} - {match.Title}.");
            }
            else
            {
                
                MainWindow.mainwindow.PushMsg(msgid, $"Трек не найден.");
            }
            ValuesProject.findTrack = true;

        }
    }
}
