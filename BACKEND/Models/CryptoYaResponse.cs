using System.Text.Json.Serialization;

namespace Wallet_Crypto.Models
{
    public class CryptoYaResponse
    {
        [JsonPropertyName("ask")]
        public decimal Ask { get; set; }

        [JsonPropertyName("totalAsk")] //Calcular cuanto se paga al comprar
        public decimal TotalAsk { get; set; }

        [JsonPropertyName("bid")]
        public decimal Bid { get; set; }

        [JsonPropertyName("totalBid")] //Venta
        public decimal TotalBid { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }
    }
}
