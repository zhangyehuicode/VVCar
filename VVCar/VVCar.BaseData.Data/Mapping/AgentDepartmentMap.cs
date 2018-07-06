using System.Data.Entity.ModelConfiguration;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Data.Mapping
{
    /// <summary>
    /// 代理商门店Map
    /// </summary>
    public class AgentDepartmentMap : EntityTypeConfiguration<AgentDepartment>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AgentDepartmentMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.LegalPerson)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.IDNumber)
                .HasMaxLength(18);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(25);

            Property(t => t.WeChatOAPassword)
                .HasMaxLength(20);

            Property(t => t.MobilePhoneNo)
                .HasMaxLength(11);

            Property(t => t.BusinessLicenseImgUrl)
                .HasMaxLength(50);

            Property(t => t.LegalPersonIDCardFrontImgUrl)
                .HasMaxLength(50);

            Property(t => t.LegalPersonIDCardBehindImgUrl)
                .HasMaxLength(50);

            Property(t => t.CompanyAddress)
                .HasMaxLength(50);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdatedUser)
                .HasMaxLength(20);

            Property(t => t.WeChatAppID)
                .HasMaxLength(50);

            Property(t => t.WeChatAppSecret)
                .HasMaxLength(50);

            Property(t => t.WeChatMchID)
                .HasMaxLength(20);

            Property(t => t.WeChatMchKey)
                .HasMaxLength(50);

            Property(t => t.Bank)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.BankCard)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}
