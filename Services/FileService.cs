using System;
using System.IO;
using System.Text;

namespace fexplorer.Services
{
    public static class FileService
    {
        private static FileInfo fileInfo { get; set; }
 
        public static void Copy(string file, string destFile)
        {
            if(Exists(file))
            {
                fileInfo = new(file);
                fileInfo.CopyTo(destFile, true);
                Explorer.SuccessOnConsole($"File '{file}' copied.");
            }
        }

        public static void Move(string file, string destFile)
        {
            if(Exists(Path.Combine(DirectoryService.CurrentDirectory, file)))
            {
                fileInfo = new(Path.Combine(DirectoryService.CurrentDirectory, file));
                fileInfo.MoveTo(destFile, true);
                Explorer.SuccessOnConsole($"File '{file}' moved.");
            }
        }

        public static void Delete(string file)
        {            
            if(Exists(Path.Combine(DirectoryService.CurrentDirectory, file)))
            {
                fileInfo = new(Path.Combine(DirectoryService.CurrentDirectory, file));
                fileInfo.Delete();
                Explorer.SuccessOnConsole($"File '{file}' deleted.");
            }
        }

        public static void Read(string file)
        {            
            if(!string.IsNullOrEmpty(file))
            {   
                if(Exists(Path.Combine(DirectoryService.CurrentDirectory, file)))
                {
                    fileInfo = new(Path.Combine(DirectoryService.CurrentDirectory, file));
                    Console.WriteLine($"\nOpening '{file}'... \n");
                    Console.WriteLine(new StringBuilder().Append('-', 100) + "\n");
                    
                    foreach (var item in File.ReadAllLines(Path.Combine(DirectoryService.CurrentDirectory, file), System.Text.Encoding.UTF8))
                        Console.WriteLine(item);

                    Console.WriteLine("\n" + new StringBuilder().Append('-', 100));
                }
            }
            else
            {
                Explorer.ErrorOnConsole("Missing operand.");
            }
        }

        public static void Create(string path)
        {
            fileInfo = new(Path.Combine(DirectoryService.CurrentDirectory, path));
            fileInfo.Create();
            Explorer.SuccessOnConsole($"File ${path} created.");
        }

        public static void OpenOn(string process, string file)
        {         
            if(!string.IsNullOrEmpty(process) && !string.IsNullOrEmpty(file))
            {   
                if(Exists(Path.Combine(DirectoryService.CurrentDirectory, file)))
                {
                    fileInfo = new(Path.Combine(DirectoryService.CurrentDirectory, file));
                    
                    try
                    {
                        System.Diagnostics.Process.Start(process, file);
                        Console.WriteLine($"\nOpening '{file}' on '{process}'... \n");
                    }
                    catch(Exception e)
                    {
                        Explorer.ErrorOnConsole(e.Message);
                    }
                }
            }
            else
            {
                Explorer.ErrorOnConsole("Missing one or more operands.");
            }
        }

        private static bool Exists(string file)
        {
            fileInfo = new(file);

            if(!fileInfo.Exists)
            {
                Explorer.ErrorOnConsole($"File '{file}' not found.");
                return false;
            }

            return true;
        }
    }
}