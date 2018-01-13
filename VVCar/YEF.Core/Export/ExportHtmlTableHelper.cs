using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core.Export
{
    /// <summary>
    /// 导出html辅助类
    /// </summary>
    public class ExportHtmlTableHelper<T> where T:IExportStyle
    {
        public Action<string> BeforeCreateHeadRow;

        public ExportHtmlTableHelper(IEnumerable<ExportInfo> exportInfos)
        {
            _exportInfos = exportInfos;
        }
        protected IEnumerable<ExportInfo> _exportInfos;

        private Type _currentType;

        protected string FillHead()
        {
            var entity = _currentType.Assembly.CreateInstance(_currentType.FullName) as IExportStyle;
            var head = "<thead>";
            if (BeforeCreateHeadRow != null)
                BeforeCreateHeadRow(head);
            head += "<tr>";
            var cl = 0;
            _exportInfos.ForEach((info) =>
            {
                head += string.Format("<th class='{0}'>{1}</th>", entity.GetHeadCellCssClass(info.PropertyName), info.Display);
            });
            head += "</tr></thead>";
            return head;
        }

        public string Export(IList<T> entities)
        {
            SetPropertyInfo(typeof(T));

            var table = "<table  cellpadding='0' cellspacing='0'>";
            var head = FillHead();
            table += head;
            table += FillBody(entities);
            table += "</table>";
            return table;
        }

        private string FillBody(IList<T> entities)
        {
            var body = "<tbody>";
            for (var i = 0; i < entities.Count(); i++)
            {
                body += FillRow(entities[i]);
            }
            body += "</tbody>";
            return body;
        }

        private void SetPropertyInfo(Type type)
        {
            _currentType = type;
            _exportInfos.ForEach((info) => { info.SetPropertyInfo(type); });
        }

        private string FillRow(T entity)
        {
            var body = string.Format("<tr class='{0}'>", entity.GetRowCssClass());
            _exportInfos.ForEach((info) =>
            {
                body += string.Format("<td class='{0}'>{1}</td>", entity.GetCellCssClass(info.PropertyName, info.GetValue(entity)), info.GetDisplayValue(entity));
            });
            body += "</tr>";
            return body;
        }
    }
}
