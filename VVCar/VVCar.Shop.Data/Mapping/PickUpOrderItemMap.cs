﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class PickUpOrderItemMap : EntityTypeConfiguration<PickUpOrderItem>
    {
        public PickUpOrderItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.ProductName)
                .HasMaxLength(20);
        }
    }
}
