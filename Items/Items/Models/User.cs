using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Items.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "firstName")]
        public string? FirstName { get; set; }

        [Required]
        [JsonProperty(PropertyName = "lastName")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [JsonProperty(PropertyName = "email")]
        public string? Email { get; set; }

  
        [JsonProperty(PropertyName = "type")]
        public string? Type  { get; set; }

        [JsonProperty(PropertyName = "items")] 
        public List<Item>? Items { get; set; }


        public string FullName => $"{FirstName} {LastName}";

    }
}
