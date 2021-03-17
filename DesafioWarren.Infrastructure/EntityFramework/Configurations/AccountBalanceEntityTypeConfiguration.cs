﻿using System;
using System.Diagnostics.CodeAnalysis;
using DesafioWarren.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioWarren.Infrastructure.EntityFramework.Configurations
{
    [ExcludeFromCodeCoverage]
    public class AccountBalanceEntityTypeConfiguration : IEntityTypeConfiguration<AccountBalance>
    {
        public void Configure(EntityTypeBuilder<AccountBalance> builder)
        {
            builder.ConfigurePrimaryKey();

            builder.IgnoreDomainEvents();

            builder.Property(accountBalance => accountBalance.Balance)
                .HasField("_balance");

            builder.Property(accountBalance => accountBalance.Transactions)
                .HasField("_transactions");

            builder.Ignore(accountBalance => accountBalance.Transactions);

            builder.HasMany(typeof(AccountTransaction), "_transactions")
                .WithOne()
                .HasForeignKey("AccountBalanceId")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property<DateTime>("LastModified");
        }
    }
}