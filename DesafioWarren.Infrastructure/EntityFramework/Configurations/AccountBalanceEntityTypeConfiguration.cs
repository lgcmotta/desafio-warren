using System;
using System.Diagnostics.CodeAnalysis;
using DesafioWarren.Domain.Entities;
using DesafioWarren.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

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
                .HasField("_balance")
                .HasPrecision(19, 4);

            builder.Property(accountBalance => accountBalance.Transactions)
                .HasField("_transactions");

            builder.Property(accountBalance => accountBalance.Currency)
                .HasField("_currency")
                .HasConversion(currency => currency.Value
                    , isoCode => Enumeration.GetItemByValue<Currency>(isoCode));

            builder.Ignore(accountBalance => accountBalance.Transactions);
            
            builder.HasMany(typeof(AccountTransaction), "_transactions")
                .WithOne()
                .HasForeignKey("AccountBalanceId")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property<DateTime>("LastModified")
                .ValueGeneratedOnAddOrUpdate()
                .HasValueGenerator<LastModifiedValueGenerator>();
        }
    }

    public class LastModifiedValueGenerator : ValueGenerator<DateTime>
    {
        public override DateTime Next(EntityEntry entry)
        {
            return DateTime.Now;
        }

        public override bool GeneratesTemporaryValues { get; }
    }
}