using System.Collections.Generic;
using System.IO;

namespace fexplorer.Services
{
    public static class Config
    {
        private static string _usedCommands = "UsedCommands.txt";
        private static FileInfo _fileInfo = new(_usedCommands);
        public static readonly List<string> LatestCommands = new();

        public static void GetUsedCommands()
        {
            if(_fileInfo.Exists)
            {                
                foreach (var item in File.ReadAllLines(_fileInfo.FullName, System.Text.Encoding.UTF8))
                    LatestCommands.Add(item);
            }
        }

        public static void SaveUsedCommands()
        {
            if(_fileInfo.Exists)
            {
                var file = File.ReadAllLines(_usedCommands, System.Text.Encoding.UTF8);
                LatestCommands.RemoveRange(0, (int)file.LongLength);
            }

            File.AppendAllLines(_usedCommands, LatestCommands, System.Text.Encoding.UTF8);
        }
    }
}