using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RetopBot.Classes
{
    public class TimersClass
    {
        public string text;
        public bool ActiveTimer;
        public string header;
        public int countTimer;
        public Timer timer;
        public TimersClass(string text, int countTimer, string header)
        {
            this.text = text;
            ActiveTimer = true;
            this.header = header;
            this.countTimer = countTimer;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        int countTick = 0;
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            countTick++;
            if (countTick == countTimer)
            {
                countTick = 0;
                MainWindow.mainwindow.SendMsg(text, 1);

            }
        }
        public void StopTimer()
        {
            timer.Stop();
            ActiveTimer = false;
        }
        public void ResumeTimer()
        {
            timer.Start();
            ActiveTimer = true;
        }

    }
}
