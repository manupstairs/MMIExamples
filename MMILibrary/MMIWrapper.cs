﻿using Microsoft.Management.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MMILibrary
{
    public class MMIWrapper
    {
        public void GetAssetTagAndExpressServiceCode()
        {
            CimSession cimSession = CimSession.Create("localhost");
            IEnumerable<CimInstance> enumeratedInstances =
                cimSession.EnumerateInstances(@"root\cimv2", "Win32_SystemEnclosure");

            var cimInstance = enumeratedInstances.FirstOrDefault();
            var assetTag = cimInstance?.CimInstanceProperties["SerialNumber"].Value.ToString();
            Console.WriteLine("{0}", assetTag);
            var expressCode = BN2N(assetTag, 36);
            Console.WriteLine("{0}", expressCode);
        }

        private static long BN2N(string InN, int Base)
        {
            char[] toKs = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            if (Base < 2)
            {
                Base = 2;
            }
            char[] digs = InN.ToUpper().ToCharArray();
            Array.Reverse(digs);
            long numb = 0, m, vDec;
            for (int i = 0; i < digs.Length; i++)
            {
                if (i == 0)
                {
                    m = 1;
                }
                else
                {
                    m = (long)Math.Pow(Base, i);
                }
                vDec = Array.IndexOf(toKs, digs[i]);
                numb += (m * vDec);
            }
            return (numb);
        }

        public void GetBIOSVersion()
        {
            CimSession cimSession = CimSession.Create("localhost");
            IEnumerable<CimInstance> enumeratedInstances =
                cimSession.EnumerateInstances(@"root\cimv2", "Win32_BIOS");

            var cimInstance = enumeratedInstances.FirstOrDefault();
            var biosVersion = cimInstance?.CimInstanceProperties["SMBIOSBIOSVersion"].Value.ToString();
            Console.WriteLine("{0}", biosVersion);
        }

        public string GetSystemModel()
        {
            CimSession cimSession = CimSession.Create("localhost");

            IEnumerable<CimInstance> queryInstances =
              cimSession.QueryInstances(@"root\cimv2", "WQL", @"Select * from Win32_ComputerSystem");
            var cimInstance = queryInstances.FirstOrDefault();
            var systemModel = cimInstance?.CimInstanceProperties["model"].Value.ToString();

            Console.WriteLine("{0}", systemModel);

            return systemModel;
        }

        public void GetPortNameAndFriednlyName()
        {
            CimSession cimSession = CimSession.Create("localhost");
            IEnumerable<CimInstance> queryInstances =
             cimSession.QueryInstances(@"root\cimv2", "WQL", @"SELECT * FROM Win32_PnPEntity WHERE ClassGuid = '{4d36e978-e325-11ce-bfc1-08002be10318}'");

            var cimInstance = queryInstances.FirstOrDefault();

            var friendlyName = cimInstance?.CimInstanceProperties["Name"].Value.ToString();

            Console.WriteLine("{0}", friendlyName);
        }
    }
}
