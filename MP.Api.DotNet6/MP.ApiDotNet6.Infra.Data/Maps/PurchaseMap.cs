﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Infra.Data.Maps
{
    public class PurchaseMap : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("compra");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("idcompra")
                .UseIdentityColumn();

            builder.Property(x => x.PersonId)
                .HasColumnName("idpessoa");

            builder.Property(x => x.ProductId)
                .HasColumnName("idproduto");

            builder.Property(x => x.Date)
                .HasColumnType("date")
                .HasColumnName("datacompra");

            //uma pessoa para "N" compras
            builder.HasOne(x => x.Person)
                .WithMany(x => x.Purchase);

            //um produto para varias compras
            builder.HasOne(x => x.Product)
                .WithMany(x => x.Purchase);


        }
    }
}
