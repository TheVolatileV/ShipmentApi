using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

#nullable disable

namespace Infrastructure.Models
{
    public partial class Organization
    {
        [JsonIgnore, XmlIgnore]
        public string Type { get; set; }
        public Guid Id { get; set; }
        public string Code { get; set; }
    }
}
