using System;

namespace DesafioWarren.Application.Models
{
    public class AccountTransactionModel : EntityModel
    {
        public string TransactionType { get; set; }

        public DateTime Occurrence { get; set; }

        public decimal TransactionValue { get; set; }

    }
}