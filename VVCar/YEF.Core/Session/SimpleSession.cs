using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Session
{
    /// <summary>
    /// SimpleSession
    /// </summary>
    public class SimpleSession : ISession, ISessionProvider
    {
        #region ISession 成员

        /// <summary>
        /// 商户号
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }

        #endregion

        #region ISessionProvider 成员

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return this;
        }

        #endregion
    }
}
