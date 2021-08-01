using System;
using System.Diagnostics;
using System.Linq;

namespace Application_process
{
    public class Application
    {
        public Process process { get; private set; }
        public Application() { }

        public void FindProcess(string appName)
        {
            process = Process.GetProcessesByName(appName)
                ?.FirstOrDefault(p  => p.ProcessName == appName) ?? throw new Exception("Процесс не найден или не запущен");
        }

        public TimeSpan GetStartApp()
        {
            TimeSpan startTime;
            startTime = DateTime.Now - process.StartTime;
            return startTime;
        }
        
        public void CloseApp()
        {
            process.Kill();
        }
    }
}