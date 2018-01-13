using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Threading;

namespace YEF.Utility.SysManagement
{
    /// <summary>
    /// 系统信息 Helper
    /// </summary>
    public class SystemInfoHelper
    {
        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        public static SystemInfo GetSystemInfo()
        {
            return new SystemInfo
            {
                ProcessorCount = Environment.ProcessorCount,
                CpuUsageAmount = GetCurrentCpuUsage(),
                TotalMemory = GetPhisicalMemory(),
                AvailableMemory = GetAvailableMemory(),
                LogicalDrives = GetLogicalDrives(),
                ProcessList = GetTop5ProcessInfo(),
                IPInfo = GetIpCityInfo(),
                UpdateTime = DateTime.Now
            };
        }

        /// <summary>
        /// 磁盘分区信息
        /// </summary>
        /// <returns></returns>
        static List<DriveInfo> GetLogicalDrives()
        {
            var drives = new List<DriveInfo>();
            var diskClass = new ManagementClass("Win32_LogicalDisk");
            var disks = diskClass.GetInstances();
            foreach (var disk in disks)
            {
                // DriveType.Fixed 为固定磁盘(硬盘) 
                if (int.Parse(disk["DriveType"].ToString()) == (int)DriveType.Fixed)
                {
                    drives.Add(new DriveInfo(disk["Name"].ToString(), Math.Round(long.Parse(disk["Size"].ToString()) / 1024.00 / 1024.00 / 1024.00, 2) + "GB", Math.Round(long.Parse(disk["FreeSpace"].ToString()) / 1024.00 / 1024.00 / 1024.00, 2) + "GB"));
                }
            }
            return drives;
        }


        /// <summary>
        /// 获取特定磁盘信息 
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        private List<DriveInfo> GetLogicalDrives(char driverId)
        {
            var drives = new List<DriveInfo>();
            var wmiquery = new WqlObjectQuery("SELECT * FROM Win32_LogicalDisk WHERE DeviceID = ’" + driverId + ":’");
            var wmifind = new ManagementObjectSearcher(wmiquery);
            foreach (var disk in wmifind.Get())
            {
                if (int.Parse(disk["DriveType"].ToString()) == (int)DriveType.Fixed)
                {
                    drives.Add(new DriveInfo(disk["Name"].ToString(), Math.Round(long.Parse(disk["Size"].ToString()) / 1024.00 / 1024.00 / 1024.00, 2) + "GB", Math.Round(long.Parse(disk["FreeSpace"].ToString()) / 1024.00 / 1024.00 / 1024.00, 2) + "GB"));
                }
            }
            return drives;
        }

        /// <summary>
        /// 获得进程列表
        /// </summary>
        /// <returns></returns>
        static List<ProcessInfo> GetTop5ProcessInfo()
        {
            var processInfo = new List<ProcessInfo>();
            var processes = Process.GetProcesses();
            foreach (var instance in processes)
            {
                try
                {
                    processInfo.Add(new ProcessInfo(instance.Id,
                        instance.ProcessName,
                        instance.TotalProcessorTime.TotalMilliseconds,
                        Math.Round(instance.WorkingSet64 / 1024.00 / 1024.00, 2),
                        instance.MainModule.ModuleName));
                }
                catch { }
            }
            processInfo.Sort((left, right) =>
            {
                if (left.UsageMemory < right.UsageMemory)
                    return 1;
                else if (left.UsageMemory == right.UsageMemory)
                    return 0;
                else
                    return -1;
            });
            return processInfo.Take(5).ToList();
        }

        /// <summary>
        /// 获得进程列表 特定的
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        private List<ProcessInfo> GetProcessInfo(string processName)
        {
            var processInfo = new List<ProcessInfo>();
            var processes = Process.GetProcessesByName(processName);
            foreach (var instance in processes)
            {
                try
                {
                    processInfo.Add(new ProcessInfo(instance.Id,
                        instance.ProcessName,
                        instance.TotalProcessorTime.TotalMilliseconds,
                        instance.WorkingSet64,
                        instance.MainModule.FileName));
                }
                catch { }
            }
            return processInfo;
        }

        /// <summary>
        /// 结束指定进程
        /// </summary>
        /// <param name="pid">进程ID</param>
        private void EndProcess(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                process.Kill();
            }
            catch { }
        }

        /// <summary>
        /// 获取系统内存大小
        /// </summary>
        /// <returns>内存大小（单位M）</returns>
        static string GetPhisicalMemory()
        {
            var searcher = new ManagementObjectSearcher();   //用于查询一些如系统信息的管理对象 
            searcher.Query = new SelectQuery("Win32_PhysicalMemory ", "", new string[] { "Capacity" });//设置查询条件 
            var collection = searcher.Get();   //获取内存容量 
            var em = collection.GetEnumerator();
            long capacity = 0;
            ManagementBaseObject baseObj;
            while (em.MoveNext())
            {
                baseObj = em.Current;
                if (baseObj.Properties["Capacity"].Value != null)
                {
                    try
                    {
                        capacity += long.Parse(baseObj.Properties["Capacity"].Value.ToString());
                    }
                    catch
                    {
                        return "0";
                    }
                }
            }
            return (int)(capacity / 1024 / 1024) + "MB";
        }

        /// <summary>
        /// 获取当前内存的值 
        /// </summary>
        /// <returns></returns>
        static string GetAvailableMemory()
        {
            return GetPerformanceCounterValue("Memory", "Available MBytes") + "MB"; //_ramCounter.NextValue() + "MB";
        }

        /// <summary>
        /// 获取当前CPU的值 
        /// </summary>
        /// <returns></returns>
        static string GetCurrentCpuUsage()
        {
            return GetPerformanceCounterValue("Processor", "% Processor Time", "_Total") + "%";
        }

        /// <summary>
        /// 获取性能资源
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <returns></returns>
        static string GetPerformanceCounterValue(string categoryName, string counterName)
        {
            var pc = new PerformanceCounter(categoryName, counterName);
            float cpuLoadfirs = 0;
            cpuLoadfirs = pc.NextValue();
            return cpuLoadfirs.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 获取性能资源
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        static string GetPerformanceCounterValue(string categoryName, string counterName, string instanceName)
        {
            var pc = new PerformanceCounter(categoryName, counterName, instanceName);
            float cpuLoadfirs = 0;
            while (cpuLoadfirs <= 0)
            {
                Thread.Sleep(1000);
                cpuLoadfirs = pc.NextValue();
            }
            return cpuLoadfirs.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 获取IP位置信息
        /// </summary>
        /// <returns></returns>
        static IpCityInfo GetIpCityInfo()
        {
            //var request = WebRequest.Create("http://pv.sohu.com/cityjson");
            //Stream s = request.GetResponse().GetResponseStream();
            //StreamReader sr = new StreamReader(s, Encoding.GetEncoding("GB2312"));
            //string all = sr.ReadToEnd();
            //var start = all.IndexOf("{", StringComparison.Ordinal);
            //var resStr = all.Substring(start);
            //var ipinfo = resStr.Substring(0, resStr.Length - 1);
            //var cs = JsonHelper.FromJson<IpCityInfo>(ipinfo);
            //sr.Close();
            //s?.Close();

            using (var httpClient = new HttpClient())
            {
                var result = httpClient.GetStringAsync("http://pv.sohu.com/cityjson").Result;
                var start = result.IndexOf("{", StringComparison.Ordinal);
                var end = result.IndexOf("}", StringComparison.Ordinal);
                if (start < 0 || end < 0)
                {
                    return new IpCityInfo
                    {
                        cid = "未知",
                        cip = "未知",
                        cname = "未知",
                    };
                }
                var jsonStr = result.Substring(start, end - start + 1);
                return JsonHelper.FromJson<IpCityInfo>(jsonStr);
            }
        }
    }
}
