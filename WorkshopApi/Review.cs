using System.Text.Json.Serialization;

namespace WorkshopApi
{
    public class Review
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("review_id")]
        public int ReviewId { get; set; }
        [JsonPropertyName("rating")]
        public int Rating { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
