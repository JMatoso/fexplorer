using System;
using System.IO;

namespace fexplorer.Services
{
    public static class DirectoryService
    {
        public static string CurrentDirectory { get; set; }
        private static DirectoryInfo dirInfo { get; set; }

        public static void Initialize() => CurrentDirectory = Directory.GetCurrentDirectory();

        public static void ChangeDirectory(string path)
        {
            if(path.Equals(".."))
            {
                var splittedDir = CurrentDirectory.Split("/");
                string newDir = string.Empty;
                
                for (int i = 0; i != (splittedDir.Length - 1); i++)
                    newDir +=  "/" + splittedDir[i];
                
                CurrentDirectory = newDir.Replace("//", "/");
            }
            else
            {
                string newDir = Path.Combine(CurrentDirectory, path);

                if(Exists(newDir))
                {
                    CurrentDirectory = newDir;
                }
                else if(Exists(path))
                {
                    CurrentDirectory = path;
                }
                else
                {
                    Explorer.ErrorOnConsole($"Dir '{path}' not found.");
                }
            }
        }

        public static void ChangeDirectory() => CurrentDirectory = Directory.GetDirectoryRoot(CurrentDirectory);

        public static void Create(string path)
        {
            if(!string.IsNullOrEmpty(path))
            {
                dirInfo = new(Path.Combine(CurrentDirectory, path));

                if(!Exists(Path.Combine(CurrentDirectory, path)))
                {
                    dirInfo.Create();
                    Explorer.SuccessOnConsole($"Dir '{path}' created.");
                }
                else
                {
                    Console.WriteLine($"Dir '{path}' already exists.");
                }
            }
            else
            {
                Explorer.ErrorOnConsole("Missing operand.");
            }                
        }

        public static void Delete(string path)
        {
            if(!string.IsNullOrEmpty(path))
            {
                if(Exists(Path.Combine(CurrentDirectory, path), true))
                {
                    dirInfo = new(Path.Combine(CurrentDirectory, path));
                    dirInfo.Delete(true);
                    Explorer.SuccessOnConsole($"Dir '{path}' deleted.");
                }
            }
            else
            {
                Explorer.ErrorOnConsole("Missing operand.");
            }
        }
        
        public static void List(string extension = "*.*")
        {
            dirInfo = new(CurrentDirectory);

            foreach (var file in dirInfo.GetFileSystemInfos(extension, SearchOption.TopDirectoryOnly))
                Console.WriteLine(file);
        }

        private static bool Exists(string path, bool showErrorMessage = false)
        {
            dirInfo = new(path);

            if(!dirInfo.Exists)
            {
                if(showErrorMessage)
                    Explorer.ErrorOnConsole($"Dir '{path}' not found.");

                return false;
            }

            return true;
        }
    }
}