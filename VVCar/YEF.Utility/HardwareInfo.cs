using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Utility
{
    /// <summary>
    /// 硬件信息
    /// </summary>
    public static class HardwareInfo
    {
        /// <summary>
        /// 获取硬件ID
        /// </summary>
        /// <returns></returns>
        public static string GetHardwareId()
        {
            return GetProcessorId();
        }

        /// <summary>
        /// 获取cpu的Id
        /// </summary>
        /// <returns></returns>
        public static string GetProcessorId()
        {
            try
            {
                var searcher = new ManagementObjectSearcher("select ProcessorId from Win32_Processor");
                foreach (var obj in searcher.Get())
                {
                    return obj["ProcessorId"].ToString().Trim();
                }
                return "null";
            }
            catch { return "error"; }
        }

        /// <summary>
        /// 获取硬盘Id
        /// </summary>
        /// <returns></returns>
        public static string GetHardDiskId()
        {
            try
            {
                var searcher = new ManagementObjectSearcher("select SerialNumber from win32_DiskDrive");
                foreach (var obj in searcher.Get())
                {
                    return obj["SerialNumber"].ToString().Trim();
                }
                return "null";
            }
            catch { return "error"; }
        }

        /// <summary>
        /// 获取网卡MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetNetwordAdapter()
        {
            try
            {
                var searcher = new ManagementObjectSearcher("select MacAddress from Win32_NetworkAdapterConfiguration where IPEnabled = 1");
                foreach (var obj in searcher.Get())
                {
                    return obj["MacAddress"].ToString().Trim();
                }
                return "null";
            }
            catch { return "error"; }
        }

        /// <summary>
        /// 获取主板ID
        /// </summary>
        /// <returns></returns>
        public static string GetBaseboardId()
        {
            try
            {
                var searcher = new ManagementObjectSearcher("select SerialNumber from Win32_baseboard");
                foreach (var obj in searcher.Get())
                {
                    return obj["SerialNumber"].ToString().Trim();
                }
                return "null";
            }
            catch { return "error"; }
        }

        /// <summary>
        /// 获取SMBIOS UUID
        /// </summary>
        /// <returns></returns>
        public static string GetSystemUUID()
        {
            try
            {
                var searcher = new ManagementObjectSearcher("select UUID from Win32_ComputerSystemProduct");
                foreach (var obj in searcher.Get())
                {
                    return obj["UUID"].ToString().Trim();
                }
                return "null";
            }
            catch { return "error"; }
        }
    }
}
