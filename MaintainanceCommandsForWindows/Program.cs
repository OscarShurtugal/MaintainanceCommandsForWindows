using System;
using System.Diagnostics;


namespace MaintainanceCommandsForWindows
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Clear Java Cache: "+cleanJavaCache());

            Console.WriteLine("Clear Internet Explorer: "+clearInternetExplorerCache());

            Console.ReadLine();

        }


        /// <summary>
        /// This method woks with temp files of IE, deleting them as requested
        /// 
        /// </summary>
        /* This is the List of all possible combinations for the command on Clear My Tracks By Process #. its basically the bit code.
            1    = Browsing History
            2    = Cookies
            4    = Temporary Internet Files
            8    = Offline favorites and download history
            16   = Form Data
            32   = Passwords
            64   = Phishing Filter Data
            128  = Web page Recovery Data
            256  = Do not Show GUI when running the cache clear
            512  = Do not use Multi-threading for deletion
            1024 = Valid only when browser is in private browsing mode
            2048 = Tracking Data
            4096 = Data stored by add-ons
            8192 = Preserves Cached data for Favorite websites
            */
        private static string clearInternetExplorerCache()
        {
            cmdEmulatorClass emulatorClass = new cmdEmulatorClass();
            return emulatorClass.cmdEmulator("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255");          
        }

        /// <summary>
        /// This method cleans the Java cache entirely
        /// </summary>
        /// <returns></returns>
        /*
         * -clearcache  Delete all non installed application from cache
         * -uninstall   Delete all applications from cache
         */
        private static string cleanJavaCache()
        {
            cmdEmulatorClass emulatorClass = new cmdEmulatorClass();
            return emulatorClass.cmdEmulator("javaws -clearcache");
        }



    }


    public class cmdEmulatorClass
    {

        /// <summary>
        /// This method receives the cmd command to be executed as a string and then returns an interpreted message of the execution
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string cmdEmulator(string command)
        {
            int result = ExecuteCommand(command);
            switch (result)
            {

                case 0:
                    return "Comando Exitoso";
                    break;
                case 1:
                    return "Función Incorrecta";
                    break;
                case 2:
                    return "Hay un dato erróneo";
                    break;
                default:
                    return "No Ejecutado";
                    break;
            }
        }

        /// <summary>
        /// This method executes the CMD command invoking the cmd process with c#. This allows us to get the exit code to test for success
        /// it returns the code as an integer, which will be interpreted by the caller method
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private int ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C "+command)
            {
                CreateNoWindow=true,
                UseShellExecute=false,
                WorkingDirectory="C:\\",
                Verb="runas",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit();
            var exitCode = process.ExitCode;
            process.Close();
            return exitCode;
        }



    }
}
