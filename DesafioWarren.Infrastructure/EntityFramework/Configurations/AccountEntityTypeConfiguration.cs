using System;
using System.Diagnostics.CodeAnalysis;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DesafioWarren.Infrastructure.EntityFramework.Configurations
{
    [ExcludeFromCodeCoverage]
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ConfigurePrimaryKey();

            builder.IgnoreDomainEvents();

            builder.Property(account => account.Name)
                .HasField("_name");

            builder.Property(account => account.Email)
                .HasField("_email");
            
            builder.Property(account => account.PhoneNumber)
                .HasField("_phoneNumber");

            builder.Property(account => account.Number)
                .HasField("_accountNumber")
                .HasValueGenerator<AccountNumberValueGenerator>();

            builder.Property<DateTime>("Created");

            builder.Property<DateTime>("LastModified");

            builder.HasOne(typeof(AccountBalance), "_accountBalance")
                .WithOne()
                .HasForeignKey(typeof(AccountBalance), "AccountId")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

    public class AccountNumberValueGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues { get; }

        public override string Next(EntityEntry entry)
        {
            return new Random().Next(10000, 9999999).ToString();
        }
        
    }
}