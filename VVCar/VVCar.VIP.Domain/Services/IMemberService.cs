using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Domain;
using YEF.Core.Data;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Dtos;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 会员领域服务接口
    /// </summary>
    public interface IMemberService : IDomainService<IRepository<Member>, Member, Guid>
    {
        /// <summary>
        /// 会员注册，微信渠道
        /// </summary>
        /// <param name="registerDto">The register dto.</param>
        /// <returns></returns>
        string Register(MemberRegisterDto registerDto);
    }
}
