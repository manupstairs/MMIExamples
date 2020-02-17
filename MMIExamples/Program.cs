using MMILibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopWatcher = new Stopwatch();
            stopWatcher.Start();

            var mmiWarpper = new MMIWrapper();
            mmiWarpper.GetAssetTagAndExpressServiceCode();
            mmiWarpper.GetBIOSVersion();
            mmiWarpper.GetSystemModel();
            mmiWarpper.GetPortNameAndFriednlyName();
            mmiWarpper.GetOsVersion();

            stopWatcher.Stop();
            Console.WriteLine(stopWatcher.Elapsed);
            Console.ReadKey();
        }
    }
}
