using System;
using DesafioWarren.Domain.Aggregates;
using DesafioWarren.Domain.Entities;
using DesafioWarren.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioWarren.Infrastructure.EntityFramework.Configurations
{
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

            builder.Property<DateTime>("Created");

            builder.Property<DateTime>("LastModified");

            builder.HasOne(typeof(AccountBalance), "_accountBalance")
                .WithOne()
                .HasForeignKey(typeof(AccountBalance), "AccountId")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }

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

    public class AccountTransactionEntityTypeConfiguration : IEntityTypeConfiguration<AccountTransaction>
    {
        public void Configure(EntityTypeBuilder<AccountTransaction> builder)
        {
            builder.ConfigurePrimaryKey();

            builder.IgnoreDomainEvents();

            builder.Property(accountTransaction => accountTransaction.TransactionType)
                .HasConversion(transactionType => transactionType.Id
                    , id => Enumeration.GetItemById<TransactionType>(id));


        }
    }

    public static class EntityTypeConfigurationExtensions
    {
        public static EntityTypeBuilder<TEntity> IgnoreDomainEvents<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : Entity
        {
            builder.Ignore(entity => entity.DomainEvents);

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigurePrimaryKey<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : Entity
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .ValueGeneratedOnAdd();

            return builder;
        }
    }
}