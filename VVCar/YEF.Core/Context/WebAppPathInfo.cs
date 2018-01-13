using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace YEF.Core.Context
{
    /// <summary>
    /// App路径信息
    /// </summary>
    public class WebAppPathInfo : IAppPathInfo
    {
        /// <summary>
        /// 应用程序根路径
        /// </summary>
        public string RootPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        /// <summary>
        /// bin文件夹路径
        /// </summary>
        public string BinPath
        {
            get
            {
                return Path.Combine(RootPath, "bin");
            }
        }

        /// <summary>
        /// Config文件夹路径
        /// </summary>
        public string ConfigPath
        {
            get
            {
                return Path.Combine(RootPath, "Config");
            }
        }

        /// <summary>
        /// AppData文件夹路径
        /// </summary>
        public string AppDataPath
        {
            get
            {
                return Path.Combine(RootPath, "App_Data");
            }
        }

        /// <summary>
        /// Temp文件夹路径
        /// </summary>
        public string TempPath
        {
            get
            {
                return Path.Combine(RootPath, "App_Data/Temp");
            }
        }
    }
}
