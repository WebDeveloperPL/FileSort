namespace FileSort.Core
{
  
    public static class LoggerWrapper
    {

        public static void Debug(string text)
        {
            System.Console.ForegroundColor = ConsoleColor.DarkGray;
            System.Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {text}");
     
        }

        public static void Log(string text)
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {text}");
         
        }

        public static void LogWarning(string text)
        {
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {text}");
     
        }

        public static void Success(string text)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {text}");
        
        }

        public static void LogException(string text, Exception ex)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {text}. Exception: {ex.Message}");
   
        }
        public static void Error(string text)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"{DateTime.Now:HH:mm:ss}: {text}.");
       
        }
    }
}


