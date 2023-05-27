using ClassesModule;
using HttpModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Timers;

namespace BotModule
{
    public class GameEvent
    {
        public static bool NowPlayed = false;
        private int[] gamesId = new int[4];
        private int countGames = 0;
        private int countTick = 0;
        private Timer timer;
        private PredicionClass actualPrediction;

        public GameEvent()
        {
            
        }
        public void StartTimer(int _countTicks)
        {
            countTick = _countTicks;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(countTick == 0)
            {
                timer.Stop();
                ClosePredicion(actualPrediction);
            }
            countTick--;
        }

        public void SetGameEvent()
        {
            if (countGames != 4)
            {
                switch (gamesId[countGames])
                {
                    case 0:
                        {
                            actualPrediction = new PredicionClass();
                            POST.StartPredicionAsync(actualPrediction);
                            StartTimer(actualPrediction.Duration);
                        }
                        break;
                }
            }
        }
        public void ClosePredicion(PredicionClass predicion)
        {
            PATCH.EndPrediction(predicion);
            countGames++;
            SetGameEvent();
        }
    }
}
