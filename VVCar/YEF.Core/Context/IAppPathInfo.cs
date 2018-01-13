using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Context
{
    /// <summary>
    /// 应用程序路径信息
    /// </summary>
    public interface IAppPathInfo
    {
        /// <summary>
        /// 应用程序根路径
        /// </summary>
        string RootPath { get; }

        /// <summary>
        /// bin文件夹路径
        /// </summary>
        string BinPath { get; }

        /// <summary>
        /// Config文件夹路径
        /// </summary>
        string ConfigPath { get; }

        /// <summary>
        /// AppData文件夹路径
        /// </summary>
        string AppDataPath { get; }

        /// <summary>
        /// Temp文件夹路径
        /// </summary>
        string TempPath { get; }
    }
}
