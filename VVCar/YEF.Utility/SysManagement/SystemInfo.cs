using System;
using System.Collections.Generic;

namespace YEF.Utility.SysManagement
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 系统信息
        /// </summary>
        public SystemInfo()
        {
            LogicalDrives = new List<DriveInfo>();
            ProcessList = new List<ProcessInfo>();
        }

        /// <summary>
        /// CPU 个数
        /// </summary>
        public int ProcessorCount { get; set; }

        /// <summary>
        /// CPU 使用率
        /// </summary>
        public string CpuUsageAmount { get; set; }

        /// <summary>
        /// 总内存大小
        /// </summary>
        public string TotalMemory { get; set; }

        /// <summary>
        ///剩余内存
        /// </summary>
        public string AvailableMemory { get; set; }

        /// <summary>
        /// 逻辑磁盘信息
        /// </summary>
        public IList<DriveInfo> LogicalDrives { get; set; }

        /// <summary>
        /// 进程列表
        /// </summary>
        public IList<ProcessInfo> ProcessList { get; set; }

        /// <summary>
        /// IP 信息
        /// </summary>
        public IpCityInfo IPInfo { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string DeptID { get; set; }

        /// <summary>
        /// 门店编号 
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    /// <summary>
    /// 进程信息
    /// </summary>
    public class ProcessInfo
    {
        /// <summary>
        /// 进程信息
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="name"></param>
        /// <param name="timespan"></param>
        /// <param name="usageMemory"></param>
        /// <param name="filename"></param>
        public ProcessInfo(int processId, string name, double timespan, double usageMemory, string filename)
        {
            this.ProcessId = processId;
            this.Name = name;
            this.TimeSpan = timespan;
            this.UsageMemory = usageMemory;
            this.ModuleName = filename;
        }

        /// <summary>
        /// 进程ID
        /// </summary>
        public int ProcessId { get; set; }

        /// <summary>
        /// 进程名称  
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public double TimeSpan { get; set; }

        /// <summary>
        /// 进程关联内存用量 
        /// </summary>
        public double UsageMemory { get; set; }

        /// <summary>
        /// 模块名称 
        /// </summary>
        public string ModuleName { get; set; }
    }

    /// <summary>
    /// 驱动器信息 
    /// </summary>
    public class DriveInfo
    {
        /// <summary>
        /// 驱动器信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="totalSize"></param>
        /// <param name="freespace"></param>
        public DriveInfo(string name, string totalSize, string freespace)
        {

            this.Name = name;
            this.TotalSize = totalSize;
            this.AvailableSize = freespace;
        }

        /// <summary>
        /// 磁盘盘符
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 磁盘总大小
        /// </summary>
        public string TotalSize { get; set; }

        /// <summary>
        /// 磁盘可用空间
        /// </summary>
        public string AvailableSize { get; set; }
    }

    /// <summary>
    /// IP城市信息
    /// </summary>
    public class IpCityInfo
    {
        /// <summary>
        /// IP 
        /// </summary>
        public string cip { get; set; }

        /// <summary>
        /// CID 
        /// </summary>
        public string cid { get; set; }

        /// <summary>
        /// city name
        /// </summary>
        public string cname { get; set; }
    }
}
