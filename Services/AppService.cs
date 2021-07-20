using System;

namespace fexplorer.Services
{
    public class AppService
    {
        public static void Start(string process, string arg)
        {
            if(!string.IsNullOrEmpty(process))
            {
                try
                {
                    if(string.IsNullOrEmpty(arg))
                        System.Diagnostics.Process.Start(process);
                    else
                        System.Diagnostics.Process.Start(process, arg);
                    
                    Console.WriteLine($"\nOpening '{process}'... \n");
                }
                catch(Exception e)
                {
                    Explorer.ErrorOnConsole(e.Message);
                }
            }
            else
            {
                Explorer.ErrorOnConsole("Missing operand.");
            }
        }
    }
}