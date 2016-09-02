using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace LMS.Core.Entities
{
    public class Area
    {
        [Key]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }
    }
}
