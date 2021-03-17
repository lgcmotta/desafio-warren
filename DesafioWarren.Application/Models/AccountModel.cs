using System;

namespace DesafioWarren.Application.Models
{
    public class AccountModel : AccountModelBase, IEntityModel
    {
        public Guid Id { get; set; }
        
        public decimal Balance { get; set; }
    }
}   