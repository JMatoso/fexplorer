using System;
using fexplorer.Services;

namespace fexplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryService.Initialize();

            string cmd = string.Empty;
            
            while(true)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write($"~{DirectoryService.CurrentDirectory}: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(cmd);

                if(Console.ReadKey(true).Key == ConsoleKey.UpArrow)
                {
                    cmd = Explorer.SetCommand();
                    continue;
                }

                cmd = Console.ReadLine();

                if(cmd.Trim().Equals("exit"))
                {
                    Config.LatestCommands.Add(cmd);
                    Config.SaveUsedCommands();
                    break;
                }

                if(!string.IsNullOrWhiteSpace(cmd))
                    Explorer.Navigate(cmd);

                cmd = string.Empty;
            }
        }
    }
}
