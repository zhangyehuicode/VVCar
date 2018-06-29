﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;

namespace VVCar.VIP.Data.Mapping
{
    public class CouponPushMap : EntityTypeConfiguration<CouponPush>
    {
        public CouponPushMap()
        {
            HasKey(t => t.ID);

            Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(30);

            Property(t => t.CreatedUser)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.LastUpdateUser)
                .HasMaxLength(20);
        }
    }
}
