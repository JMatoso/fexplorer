using System;
using fexplorer.Services;

namespace fexplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryService.Initialize();
            Config.GetUsedCommands();

            string cmd = string.Empty;
            
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"~{DirectoryService.CurrentDirectory}: ");
                Console.ForegroundColor = ConsoleColor.White;

                cmd = Console.ReadLine();

                var keyInfo = new ConsoleKeyInfo();
                if(keyInfo.Equals(ConsoleKey.UpArrow))
                {
                    Console.WriteLine("up");
                }

                if(cmd.Trim().Equals("exit"))
                {
                    Config.SaveUsedCommands();
                    break;
                }

                if(!string.IsNullOrWhiteSpace(cmd))
                    Explorer.Navigate(cmd);
            }
        }
    }
}
