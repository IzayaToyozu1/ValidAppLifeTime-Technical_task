using System;
using System.Timers;

namespace Application_process
{
    public class ApplicationLifeTime
    {
        private Application application;
        private string _nameAppProc;
        private int _timeLifeApp;
        private int _checkTime;
        private Timer timer;

        public event Action<string> MessageProcess;

        public ApplicationLifeTime(){}

        public ApplicationLifeTime(string nameAppProc, int timeLifeApp, int checkTime)
        {
            Options(nameAppProc, timeLifeApp, checkTime);
        }

        public void Options(string nameAppProc, int timeLifeApp, int checkTime)
        {
            _nameAppProc = nameAppProc;
            _timeLifeApp = timeLifeApp;
            _checkTime = checkTime;
        }

        public void Start()
        {
            try
            {
                application = new Application();
                application.FindProcess(_nameAppProc);
                MessageProcess?.Invoke($"Приложение {_nameAppProc} найдено");
                TimeCheck(_checkTime);
                InvokeCheckTime(new object(), null);
            }
            catch (Exception e)
            {
                MessageProcess?.Invoke(e.Message);
            }
        }

        private void TimeCheck(int time)
        {
            timer = new Timer(time*60000);
            timer.Elapsed += InvokeCheckTime;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void InvokeCheckTime(object s, ElapsedEventArgs e)
        {
            MessageProcess?.Invoke($"Приложение \"{_nameAppProc}\" работает {(int)GetTimeStartMinuts()} мин. и {application.GetStartApp().Seconds} сек.");
            if (GetTimeStartMinuts() > _timeLifeApp)
            {
                application.CloseApp();
                MessageProcess?.Invoke($"Приложение \"{_nameAppProc}\" закрыто, так как проработало больше {_timeLifeApp} мин");
                timer.Stop();
            }
        }

        private double GetTimeStartMinuts()
        {
            TimeSpan time = application.GetStartApp();
            return time.Minutes + time.Hours * 60 + time.Seconds / 60; //после запятой указываются сек
        }
    }
}