using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Infrastructure.Models;
using Newtonsoft.Json;

namespace Web.Models
{
    public class ShipmentVm
    {
        [JsonIgnore, XmlIgnore]
        public string Type { get; set; }
        public string ReferenceId { get; set; }
        public DateTime? EstimatedTimeArrival { get; set; }
        public List<string> Organizations { get; set; }
        public TransportPacks TransportPacks { get; set; }
    }
}
