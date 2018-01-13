using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YEF.Core.Export
{
    public class ExportInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="propertyName"></param>
        /// <param name="customizeDisplay"></param>
        public ExportInfo(string propertyName, string customizeDisplay = null)
        {
            PropertyName = propertyName;
            _customizeDisplay = customizeDisplay;
        }


        #region fields

        /// <summary>
        /// 自定义显示名称
        /// </summary>
        private readonly string _customizeDisplay;

        private Dictionary<object, object> _valueMaps;
        #endregion fields
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 格式化
        /// </summary>
        public Func<string, string> Formater { get; set; }

        /// <summary>
        /// 列显示名称
        /// </summary>
        public string Display
        {
            get
            {
                if (!string.IsNullOrEmpty(_customizeDisplay))
                    return _customizeDisplay;
                return PropertyInfo.GetCustomAttributes(true).GetDescription();
            }
        }


        #region Public Method
        /// <summary>
        /// 设置属性信息
        /// </summary>
        /// <param name="type"></param>
        public void SetPropertyInfo(Type type)
        {
            PropertyInfo = type.GetProperty(PropertyName.Split('.')[0]);
        }

        public string GetDisplayValue<T>(T entity)
        {
            var v = GetValue(entity);
            if (Formater != null)
                return Formater(v);
            return v;
        }

        /// <summary>
        /// 获取实体的属性值
        /// </summary>
        /// <param name="entity"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetValue<T>(T entity)
        {
            var v = GetRawValue(entity);

            if (_valueMaps != null)
            {
                if (_valueMaps.Keys.Contains(v))
                    v = _valueMaps[v];
            }else if (v is bool)
            {
                return (bool)v ? "是" : "否";
            }
            else if (v is decimal)
            {
                return ((decimal)v).ToString("f");
            }
            else if (v is DateTime)
            {
                return ((DateTime)v).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (v is Enum)
            {
                return EnumExtensions.GetDescription(v as Enum);
            }
            else
            {
                return v.ToString();
            }
            return v.ToString();
        }

        public object GetRawValue<T>(T entity)
        {
            var v = GetPValue(entity, PropertyInfo, PropertyName.Split('.'), 0) ?? "";
            return v;
        }


        /// <summary>
        /// 设置值映射
        /// 给定值存在于字典中则返回字典中的值代替
        /// </summary>
        /// <param name="map"></param>
        public void SetValueMap(Dictionary<object, object> map)
        {
            _valueMaps = map;
        }
        #endregion Public Method

        #region private Method
        private object GetPValue<T>(T entity, PropertyInfo pi, string[] ps, int index)
        {
            var v = pi.GetValue(entity,null);
            if (v == null)
                return "";
            if (ps.Length == index + 1)
                return v;

            return GetPValue(v, v.GetType().GetProperty(ps[++index]), ps, index);
        }

        /// <summary>
        /// 属性信息
        /// </summary>
        private PropertyInfo PropertyInfo { get; set; }
        #endregion private Method

    }
}
