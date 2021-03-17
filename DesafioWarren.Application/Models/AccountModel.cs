namespace DesafioWarren.Application.Models
{
    public class AccountModel : EntityModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }
        
        public string PhoneNumber { get; set; }

        public decimal Balance { get; set; }    
    }
}   