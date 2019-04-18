using Newtonsoft.Json;

namespace TD.Api.Dtos
{
    public class UpdateProfileRequest
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        
        [JsonProperty("image_id")]
        public int? ImageId { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get => "https://td-api.julienmialon.com/images/" + (ImageId == null ? 1 : ImageId); set => ImageUrl = value; }

    }
}