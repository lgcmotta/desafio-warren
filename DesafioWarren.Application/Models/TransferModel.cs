namespace DesafioWarren.Application.Models
{
    public class TransferModel : TransactionModel
    {
        public string DestinationAccount { get; set; }
    }

    public class PaymentModel : TransactionModel
    {
        public string InvoiceNumber { get; set; }
    }

    public class TransactionModel
    {
        public decimal Value { get; set; }
    }
}   