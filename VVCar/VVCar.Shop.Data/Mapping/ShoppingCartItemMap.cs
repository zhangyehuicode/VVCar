﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Data.Mapping
{
    public class ShoppingCartItemMap : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemMap()
        {
            HasKey(t => t.ID);

            Property(t => t.ProductName)
                .HasMaxLength(20);

            Property(t => t.ImgUrl)
                .HasMaxLength(50);
        }
    }
}
