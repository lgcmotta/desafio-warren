using System;

namespace DesafioWarren.Application.Models
{
    public class AccountTransactionModel : IEntityModel
    {
        public Guid Id { get; set; }

        public string TransactionType { get; set; }

        public DateTime Occurrence { get; set; }

        public decimal TransactionValue { get; set; }

        
    }
}