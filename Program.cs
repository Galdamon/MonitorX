using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace MonitorX
{
    class Program 
    {
        //creates new log file if it doesn't exist
        //writes given message into log file with timestamp
        static void Logger(string logMessage) 
        {
            string logFile = Directory.GetCurrentDirectory() + "\\log.txt";
            string timeStamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            
            if(!File.Exists(logFile)) 
            {
                using (FileStream fs = File.Create(logFile)) ;
            }
            using (StreamWriter file = new StreamWriter(logFile, true)) 
            {
                file.WriteLine($"{timeStamp} : {logMessage}");
            }
        }

        //checks each process with given name and calculates its runtime
        //closes processes that are running longer than deadline 
        static void Interval(string name, int deadline)
        {
            Process[] runningProc = Process.GetProcessesByName(name);
            TimeSpan timeToKill = new TimeSpan(0, 0, deadline, 0);

            foreach (Process y in runningProc)
            {
                TimeSpan runtime = DateTime.Now - y.StartTime;
                if(runtime > timeToKill)
                {
                    Logger($"It is time to kill {y.Id.ToString()}. It was running for {runtime.ToString(@"hh\:mm\:ss")}.");
                    y.Kill();
                }
            }
        }
        static void Main(string[] args){
            bool canRun = true;
            int deadline = 0;
            int repeat = 0;
            if(args.Length < 3) { canRun = false; }
            else
            {
                try
                {
                    deadline = Int32.Parse(args[1]);
                    repeat = Int32.Parse(args[2]);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse arguments.");
                    canRun = false;
                }
            }
            
            if (canRun)
            {
                Logger($"Monitor started. Tracking '{args[0]}', Time to kill {args[1]} minutes, repeat each {args[2]} minutes.");

                while(true)
                {
                    Interval(args[0], deadline);
                    Thread.Sleep(repeat * 60000);
                }
            }
            else { Console.WriteLine($"Monitor can't run with these arguments!"); }
        }
    }
}
