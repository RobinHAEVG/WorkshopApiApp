using System.Text.Json.Serialization;

namespace WorkshopApi
{
    public class Review
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }
        [JsonPropertyName("rating")]
        public int Rating { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
