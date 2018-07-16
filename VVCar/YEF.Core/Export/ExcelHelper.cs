using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YEF.Core.Enums;

namespace YEF.Core.Export
{
    /// <summary>
    /// Excel 帮助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 从Excel导入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="fieldInfos"></param>
        /// <returns></returns>
        public static IList<T> ImportFromExcel<T>(string fileName, IEnumerable<ExcelFieldInfo> fieldInfos)
            where T : new()
        {
            //IWorkbook workBook = null;
            //switch (extendsion)
            //{
            //    case "xls":
            //        workBook = new HSSFWorkbook(stream);
            //        break;
            //    case "xlsx":
            //        workBook = new XSSFWorkbook(stream);
            //        break;
            //}
            IWorkbook workBook = null;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var extendsion = Path.GetExtension(fileName);
                switch (extendsion)
                {
                    case ".xls":
                        workBook = new HSSFWorkbook(stream);
                        break;

                    case ".xlsx":
                        workBook = new XSSFWorkbook(stream);
                        break;
                }
            }
            if (workBook == null) { throw new Exception("Excel表格工作簿为空"); }
            ISheet sheet = workBook.GetSheetAt(0);

            IList<T> list = new List<T>();
            if (sheet.PhysicalNumberOfRows < 1)
                return list;

            IRow row = null;
            T entity;
            var properties = typeof(T).GetProperties();
            ExcelFieldInfo fieldInfo;
            for (int j = 1; j < sheet.PhysicalNumberOfRows; j++)
            {
                row = sheet.GetRow(j);
                entity = new T();
                foreach (var prop in properties)
                {
                    fieldInfo = fieldInfos.FirstOrDefault(info => info.PropertyName == prop.Name);
                    if (fieldInfo == null)
                        continue;

                    var cellValue = row.GetCell(fieldInfo.Index);
                    if (!fieldInfo.AllowNullValue && (cellValue == null || string.IsNullOrEmpty(cellValue.ToString())))
                        throw new Exception(string.Format("第{0}行“{1}”不能为空", j + 1, fieldInfo.DisplayName));

                    string cellValueStr = cellValue.ToString();
                    if (prop.PropertyType == typeof(int))
                    {
                        int temp;
                        if (!int.TryParse(cellValueStr, out temp))
                            throw new Exception(string.Format("第{0}行“{1}”应为{2}类型", j + 1, fieldInfo.DisplayName, "整数"));
                        prop.SetValue(entity, temp, null);
                    }
                    else if (prop.PropertyType == typeof(decimal))
                    {
                        decimal temp;
                        if (!decimal.TryParse(cellValueStr, out temp))
                            throw new Exception(string.Format("第{0}行“{1}”应为{2}类型", j + 1, fieldInfo.DisplayName, "整数"));
                        prop.SetValue(entity, temp, null);
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        DateTime temp;
                        cellValueStr = cellValue.DateCellValue.ToString();
                        if (!DateTime.TryParse(cellValueStr, out temp))
                            throw new Exception(string.Format("第{0}行“{1}”应为{2}类型", j + 1, fieldInfo.DisplayName, "时间"));
                        prop.SetValue(entity, temp, null);
                    }
                    else if (prop.PropertyType == typeof(DateTime?))
                    {
                        DateTime temp;
                        cellValueStr = cellValue.DateCellValue.ToString();
                        if (!DateTime.TryParse(cellValueStr, out temp))
                            throw new Exception(string.Format("第{0}行“{1}”应为{2}类型", j + 1, fieldInfo.DisplayName, "时间"));
                        prop.SetValue(entity, temp, null);
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        bool temp;
                        if (!bool.TryParse(cellValueStr, out temp))
                            throw new Exception(string.Format("第{0}行“{1}”应为{2}类型", j + 1, fieldInfo.DisplayName, "布尔"));
                        prop.SetValue(entity, cellValueStr, null);
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(entity, cellValueStr, null);
                    }
                    else if (prop.PropertyType == typeof(ESex))
                    {
                        if (!(cellValueStr.Equals("男") || cellValueStr.Equals("女")))
                            throw new Exception(string.Format("第{0}行“{1}”应为{2}类型", j + 1, fieldInfo.DisplayName, "性别"));
                        prop.SetValue(entity, cellValueStr.Equals("男") ? ESex.Male : ESex.Female);
                    }
                    else
                    {
                        throw new Exception(string.Format("第{0}行“{1}”类型未知", j + 1, fieldInfo.DisplayName));
                    }
                }
                list.Add(entity);
            }
            return list;
        }
    }
}