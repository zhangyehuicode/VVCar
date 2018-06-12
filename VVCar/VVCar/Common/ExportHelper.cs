using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using YEF.Core.Export;
using YEF.Core;

namespace VVCar.Common
{
    /// <summary>
    /// 导出会员卡信息操作类
    /// </summary>
    public class ExportHelper
    {
        /// <summary>
        /// WorkBook创建完成后事件
        /// </summary>
        public event Action<ISheet> OnSheetCreated;

        /// <summary>
        /// 行数据填充完成后事件
        /// </summary>
        public event Action<HSSFWorkbook, IRow, object> OnRowFilled;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportHelper"/> class.
        /// </summary>
        /// <param name="exportInfos">The export infos.</param>
        public ExportHelper(IEnumerable<ExportInfo> exportInfos)
        {
            _exportInfos = exportInfos;
        }

        /// <summary>
        /// 导出字段数量
        /// </summary>
        public int ExportInfoCount { get { return _exportInfos.Count(); } }
        private IEnumerable<ExportInfo> _exportInfos;
        private HSSFWorkbook _book;

        /// <summary>
        /// 生成导会员卡信息的excel文件，并返回导出的url地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public string Export<T>(IList<T> entities, string sheetName = "导出数据")
        {
            SetPropertyInfo(typeof(T));
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            _book = book;
            ISheet sheet = book.CreateSheet(sheetName);
            if (OnSheetCreated != null)
                OnSheetCreated(sheet);

            FillHead(sheet);
            for (var i = 0; i < entities.Count(); i++)
            {
                FillRow(entities[i], sheet, i + 1);
            }
            using (var ms = new MemoryStream())
            {
                book.Write(ms);
                var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                using (var fs = new FileStream(ExportTempPath(fileName), FileMode.Create))
                {
                    book.Write(fs);
                }
                return ExportTempUrl(fileName);
            }
        }

        private void SetPropertyInfo(Type type)
        {
            _exportInfos.ForEach((info) => { info.SetPropertyInfo(type); });
        }

        private string ExportTempPath(string fileName)
        {
            var tempDirectory = string.Format("{0}export\\", HttpContext.Current.Request.MapPath("/"));
            if (!Directory.Exists(tempDirectory))
                Directory.CreateDirectory(tempDirectory);
            else
            {
                ClearExpiredFiles(tempDirectory);
            }
            return tempDirectory + fileName;
        }

        private void ClearExpiredFiles(string directory)
        {
            try
            {
                var files = Directory.GetFiles(directory, "*.xls");
                var now = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    long fileTime;
                    if (long.TryParse(fileName, out fileTime)
                        && (now - fileTime) > 100000000)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private string ExportTempUrl(string fileName)
        {
            var exportUrl = string.Format("http://{0}:{1}/export/{2}", HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.Url.Port, fileName);
            return exportUrl;
        }

        private void FillHead(ISheet sheet)
        {
            var row = sheet.CreateRow(0);
            var cl = 0;
            _exportInfos.ForEach((info) =>
            {
                row.CreateCell(cl++).SetCellValue(info.Display);
            });
        }

        private void FillRow<T>(T entity, ISheet sheet, int rowIndex)
        {
            var row = sheet.CreateRow(rowIndex);
            var cl = 0;
            _exportInfos.ForEach((info) =>
            {
                row.CreateCell(cl++).SetCellValue(info.GetValue(entity));
            });

            if (OnRowFilled != null)
                OnRowFilled(_book, row, entity);
        }
    }
}