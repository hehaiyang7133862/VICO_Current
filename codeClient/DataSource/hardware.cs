using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;
using System.Threading;

namespace nsVicoClient
{
    public class hardware
    {
        public static string getProcessorId()
        {
            string cpuInfo = "";//cpu序列号  
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            return cpuInfo;
        }
        //获取硬盘ID 
        public static string getHDid()
        {
            string HDid = "";
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid = (string)mo.Properties["Model"].Value;
            }
            return HDid;
        }


        //获取网卡硬件地址  
        //     public static string getMacAddress()
        //     {
        // ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");  
        // ManagementObjectCollection moc2 = mc.GetInstances();  
        // foreach(ManagementObject mo in moc2)  
        // {  
        //  if((bool)mo["IPEnabled"] == true)  
        //   Response.Write("MAC address\t{0}"+mo["MacAddress"].ToString());  
        //  mo.Dispose();  
        // }  
        //}

        
        public static List<string> GetUsbSerial()
        {
            Thread.Sleep(500);
            List<string> _serialNumber = new List<string>();
            string[] diskArray;
            string driveNumber;
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition");
            foreach (ManagementObject dm in searcher.Get())
            {
                getValueInQuotes(dm["Dependent"].ToString());
                diskArray = getValueInQuotes(dm["Antecedent"].ToString()).Split(',');
                driveNumber = diskArray[0].Remove(0, 6).Trim();
                var disks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject disk in disks.Get())
                {
                    if (disk["Name"].ToString() == ("\\\\.\\PHYSICALDRIVE" + driveNumber) & disk["InterfaceType"].ToString() == "USB")
                    {
                        _serialNumber.Add(parseSerialFromDeviceID(disk["PNPDeviceID"].ToString()));
                    }
                }
            }
            return _serialNumber;
        }
        private static string parseSerialFromDeviceID(string deviceId)
        {
            var splitDeviceId = deviceId.Split('\\');
            var arrayLen = splitDeviceId.Length - 1;
            var serialArray = splitDeviceId[arrayLen].Split('&');
            var serial = serialArray[0];
            return serial;
        }

        private static string getValueInQuotes(string inValue)
        {
            var posFoundStart = inValue.IndexOf("\"");
            var posFoundEnd = inValue.IndexOf("\"", posFoundStart + 1);
            var parsedValue = inValue.Substring(posFoundStart + 1, (posFoundEnd - posFoundStart) - 1);
            return parsedValue;
        }
    }


}
