namespace Wallet_Crypto.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string CryptoCode { get; set; }
        public string Action { get; set; }
        public decimal CryptoAmount { get; set; }
        public decimal Money { get; set; }
        public DateTime DateTime { get; set; }
    }
}
