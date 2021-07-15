using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopWatcher = new Stopwatch();
            stopWatcher.Start();

            var helper = new SystemManagmenetHelper();
            helper.GetAssetTagAndExpressServiceCode();
            helper.GetBIOSVersion();
            helper.GetSystemModel();
            helper.GetPortNameAndFriednlyName();
            helper.GetOsVersion();

            helper.InitializeBrightnessWatcher();

            stopWatcher.Stop();
            Console.WriteLine(stopWatcher.Elapsed);
            while (true) { Thread.Sleep(1000); };
           // Console.ReadKey();
        }
    }
}
