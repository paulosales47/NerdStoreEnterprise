﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Clientes.API.Models;
using NSE.Core.DomainObjects;

namespace NSE.Clientes.API.Data.Mappins
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Numero)
                .IsRequired()
                .HasMaxLength(Cpf.CpfMaxLenght)
                .HasColumnName("Cpf")
                .HasColumnType($"varchar({Cpf.CpfMaxLenght})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Endereco)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.EnderecoMaxLenght})");
            });

            //1 : 1 => Cliente: Endereço
            builder.HasOne(c => c.Endereco)
                .WithOne(c => c.Cliente);

            builder.ToTable("Clientes");

        }
    }
}
