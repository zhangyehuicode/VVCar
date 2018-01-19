using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Domain;
using YEF.Core.Data;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Services.DomainServices
{
    public partial class MakeCodeRuleService : DomainServiceBase<IRepository<MakeCodeRule>, MakeCodeRule, Guid>, IMakeCodeRuleService
    {
        public MakeCodeRuleService()
        {
        }

        #region methods

        public override MakeCodeRule Add(MakeCodeRule entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CurrentValue = 0;
            return base.Add(entity);
        }

        public override bool Update(MakeCodeRule entity)
        {
            if (entity == null)
                return false;
            this.DoValidate(entity);
            var codeRule = this.Repository.GetByKey(entity.ID);
            if (codeRule == null)
                throw new DomainException("修改失败，数据不存在");
            codeRule.Code = entity.Code;
            codeRule.Name = entity.Name;
            codeRule.IsAvailable = entity.IsAvailable;
            codeRule.IsManualMake = entity.IsManualMake;
            codeRule.Length = entity.Length;
            codeRule.Prefix1 = entity.Prefix1;
            codeRule.Prefix1Length = entity.Prefix1Length;
            codeRule.Prefix1Rule = entity.Prefix1Rule;
            codeRule.Prefix2 = entity.Prefix2;
            codeRule.Prefix2Length = entity.Prefix2Length;
            codeRule.Prefix2Rule = entity.Prefix2Rule;
            codeRule.Prefix3 = entity.Prefix3;
            codeRule.Prefix3Length = entity.Prefix3Length;
            codeRule.Prefix3Rule = entity.Prefix3Rule;
            return this.Repository.Update(codeRule) > 0;
        }

        protected override bool DoValidate(MakeCodeRule entity)
        {
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("代码 {0} 已使用，不能重复添加。", entity.Code));
            exists = this.Repository.Exists(t => t.Name == entity.Name && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("名称 {0} 已使用，不能重复添加。", entity.Name));
            return true;
        }

        string GetCodePrefix(ECodePrefixRule prefixRule, int length, string prefix, DateTime dateTime)
        {
            var newCodePrefix = string.Empty;
            if (prefixRule == ECodePrefixRule.None)
                return newCodePrefix;
            switch (prefixRule)
            {
                case ECodePrefixRule.Fixed:
                    newCodePrefix = prefix;
                    break;
                case ECodePrefixRule.Department:
                    newCodePrefix = AppContext.DepartmentCode;
                    break;
                case ECodePrefixRule.Date:
                    newCodePrefix = dateTime.ToString(prefix);
                    break;
            }

            return newCodePrefix;
        }

        string GetDeptCodePrefix(int length)
        {
            if (string.IsNullOrEmpty(AppContext.DepartmentCode))
                return string.Empty;
            var deptCodeLength = AppContext.DepartmentCode.Length;
            if (deptCodeLength <= length)
                return AppContext.DepartmentCode;
            return AppContext.DepartmentCode.Substring(deptCodeLength - length);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.Get(p => p.ID == key);
            entity.IsDeleted = true;
            return base.Update(entity);
        }

        string BuildCode(MakeCodeRule codeRule, DateTime dateTime)
        {
            if (codeRule == null || codeRule.IsManualMake)
                return string.Empty;
            var newCodePrefix1 = GetCodePrefix(codeRule.Prefix1Rule, codeRule.Prefix1Length, codeRule.Prefix1, dateTime);
            var newCodePrefix2 = GetCodePrefix(codeRule.Prefix2Rule, codeRule.Prefix2Length, codeRule.Prefix2, dateTime);
            var newCodePrefix3 = GetCodePrefix(codeRule.Prefix3Rule, codeRule.Prefix3Length, codeRule.Prefix3, dateTime);
            var runningNumberLength = codeRule.Length - newCodePrefix1.Length - newCodePrefix2.Length - newCodePrefix3.Length;
            string runningNumber = codeRule.CurrentValue.PadLeft(runningNumberLength, '0');
            return string.Concat(newCodePrefix1, newCodePrefix2, newCodePrefix3, runningNumber);
        }

        #endregion

        #region IMakeCodeRuleService 成员

        public string GetCode(string codeType)
        {
            var codeRule = this.Repository.GetQueryable(false)
                .FirstOrDefault(t => t.Code == codeType && t.IsAvailable == true);
            if (codeRule == null || codeRule.IsManualMake)
                return string.Empty;
            codeRule.CurrentValue++;
            return BuildCode(codeRule, DateTime.Today);
        }

        public string GenerateCode(string codeType)
        {
            return GenerateCode(codeType, DateTime.Today);
        }

        public string GenerateCode(string codeType, DateTime dateTime)
        {
            var codeRule = this.Repository.Get(t => t.Code == codeType && t.IsAvailable == true);
            if (codeRule == null || codeRule.IsManualMake)
                return string.Empty;
            codeRule.CurrentValue++;
            this.Repository.Update(codeRule);
            return BuildCode(codeRule, dateTime);
        }

        public void ResetCode(string codeType)
        {
            var codeRule = this.Repository.Get(t => t.Code == codeType && t.IsAvailable == true);
            if (codeRule == null || codeRule.IsManualMake)
                return;
            codeRule.CurrentValue = 0;
            this.Repository.Update(codeRule);
        }

        #endregion
    }
}
