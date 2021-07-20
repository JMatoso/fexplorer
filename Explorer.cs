using System;
using fexplorer.Services;

namespace fexplorer
{
    public class Explorer
    {
        private static int _latestCommand = 0;
        private static int _count = 0;
        public static void Navigate(string cmd)
        {
            var command = cmd.Split(' ');

            Config.LatestCommands.Add(cmd);

            string mainCommand = command[0].ToString().Trim();
            string param = command.Length > 1 ? command[1].Trim() : string.Empty;
            string param2 = command.Length > 2 ? command[2].Trim() : string.Empty;

            switch(mainCommand)
            {
                case "mkdir":
                    DirectoryService.Create(param);                    
                    break;
                case "rmdir":
                    DirectoryService.Delete(param);
                    break;
                case "cd":
                    if(string.IsNullOrEmpty(param))
                        DirectoryService.ChangeDirectory();  
                    else
                        DirectoryService.ChangeDirectory(param);
                    break;
                case "clear":
                    Console.Clear();
                    break;
                case "ls":
                case "dir":
                    DirectoryService.List();
                    break;
                case "rdfile":
                    FileService.Read(param);
                    break;
                case "open":
                    FileService.OpenOn(param, param2);
                    break;
                case "start":
                    AppService.Start(param, param2);
                    break;
                case "wrfile":
                    FileService.Create(param);
                    break;
                case "rmfile":
                    FileService.Delete(param);
                    break;
                default:
                    ErrorOnConsole($"Command '{mainCommand}' not found.");
                    break;
            }
        }

        public static string SetCommand()
        {
            _count++;
            _latestCommand = Config.LatestCommands.Count;
            return Config.LatestCommands[_latestCommand - _count];
        }

        public static void ErrorOnConsole(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + message + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SuccessOnConsole(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + message + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}