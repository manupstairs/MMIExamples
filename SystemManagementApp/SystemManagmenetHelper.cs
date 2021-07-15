using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemManagementApp
{
    class SystemManagmenetHelper
    {
        public void GetAssetTagAndExpressServiceCode()
        {
            string assetTag = string.Empty;
            string expressCode = string.Empty;


            using (var manageClassInstance = new ManagementClass("Win32_SystemEnclosure"))
            {
                ManagementObjectCollection collection = manageClassInstance.GetInstances();
                foreach (ManagementObject obj in collection)
                {
                    assetTag = obj["SerialNumber"].ToString();
                    Console.WriteLine(assetTag);
                    break;
                }
            }

        }

        public void GetBIOSVersion()
        {
            string biosVersion = string.Empty;


            using (var manageClassInstance = new ManagementClass("Win32_BIOS"))
            {
                ManagementObjectCollection collection = manageClassInstance.GetInstances();
                foreach (ManagementObject obj in collection)
                {
                    biosVersion = obj["SMBIOSBIOSVersion"].ToString();
                    Console.WriteLine(biosVersion);
                    break;
                }
            }
        }

        public string GetSystemModel()
        {
            string model = string.Empty;
            SelectQuery query = new SelectQuery(@"Select * from Win32_ComputerSystem");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                //execute the query
                foreach (ManagementObject process in searcher.Get())
                {
                    //print system info
                    process.Get();
                    model = process["Model"].ToString();
                }
            }
            Console.WriteLine(model);
            return model;
        }

        public void GetPortNameAndFriednlyName()
        {
            using (ManagementObjectSearcher mos =
                    new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE ClassGuid = '{4d36e978-e325-11ce-bfc1-08002be10318}'"))
            {
                if (mos.Get().Count > 0)
                {
                    foreach (var com in mos.Get())
                    {
                        string friendlyName = (string)com["Name"];
                        int idx1 = friendlyName.IndexOf("(COM");
                        if (idx1 >= 0)
                        {
                            int idx2 = friendlyName.IndexOf(')', idx1++);
                            string portName = friendlyName.Substring(idx1, idx2 - idx1);
                            Console.WriteLine($"portName: {portName}, friendlyName: {friendlyName}");
                        }
                    }
                }
            }
        }

        public string GetOsVersion()
        {

            var query = "SELECT * FROM Win32_OperatingSystem";
            using (var searcher = new ManagementObjectSearcher(query))
            {
                var info = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                var version = info.Properties["Version"].Value.ToString();
                var OsVersion = version.Split('.');
                var _version = string.Format("{0}.{1}", OsVersion[0], OsVersion[1]);
                Console.WriteLine(_version);
                return _version;
            }

        }

        private ManagementEventWatcher brightnessEventWatcher;
        private const string QUERYSCOPE = "root\\WMI";

        public void InitializeBrightnessWatcher()
        {
            if (brightnessEventWatcher == null)
            {
                //string query = "SELECT Brightness from WmiMonitorBrightnessEvent";
                string query = "SELECT * from BIOSEvent";
                brightnessEventWatcher = GetWmiEventWatcher(QUERYSCOPE, query);
                brightnessEventWatcher.EventArrived += OnBrightnessChange;
                brightnessEventWatcher.Start();
            }
        }

        private ManagementEventWatcher GetWmiEventWatcher(string scopeStr, string queryStr)
        {
            ManagementScope scope = new ManagementScope(scopeStr);
            EventQuery query = new EventQuery(queryStr);
            return new ManagementEventWatcher(scope, query);
        }

        private void OnBrightnessChange(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine($"OnBrightnessChange new status is {e.Context}");
        }
    }
}
