using System;
using System.Text.RegularExpressions;
using Application_process;

namespace ValidAppLifeTime__Technical_task_
{
    class Program
    {
        private static ApplicationLifeTime _application;
        public static void Main(string[] args)
        {
            string textCommand = GetCommandStart(args);
            _application = new ApplicationLifeTime();
            _application.MessageProcess += MessageResult;
            Console.Write("Введите команду по следующему шаблону: *Наименование приложения* *время разрешенной работы(в минутах)* *через какое время проверять приложение(в минутах)*\n" +
                              "Пример: *chrome 5 1*");
            while (true)
            {
                if (textCommand == "")
                    textCommand = Console.ReadLine();
                StartApp(textCommand);
                textCommand = "";
            }
        }

        static void StartApp(string textCommand)
        {
            int timeLifeAPp;
            int checkTime;
            string nameApp = ConvertCommand(ref textCommand, @"\w+\W");

            if (int.TryParse(ConvertCommand(ref textCommand, @"\d+\s"), out timeLifeAPp) == false ||
                int.TryParse(textCommand, out checkTime) == false)
            {
                Console.Write("Команда введена неверно!\nПопобуйте снова:");
                return;
            }

            _application.Options(nameApp, timeLifeAPp, checkTime);
            _application.Start();
        }

        static string ConvertCommand(ref string text, string pattern)
        {
            Regex r = new Regex(pattern);
            string result = r.Match(text).Value;
            text = Regex.Replace(text, result, "");
            return result = Regex.Replace(result, " ", ""); ;
        }

        static void MessageResult(string message)
        {
            Console.WriteLine(message);
        }

        static string GetCommandStart(string[] text)
        {
            string result = "";
            foreach (var item in text)
            {
                result += item + " ";
            }

            return result;
        }
    }
}
