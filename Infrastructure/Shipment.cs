using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

#nullable disable

namespace Infrastructure
{
    public partial class Shipment
    {
        [JsonIgnore, XmlIgnore]
        public string Type { get; set; }
        public string ReferenceId { get; set; }
        public DateTime? EstimatedTimeArrival { get; set; }
        public List<string> Organizations { get; set; }
        public TransportPacks TransportPacks { get; set; }
    }
    
    public class TransportPacks
    {
        public List<TransportPack> Nodes { get; set; }
    }

    public class TransportPack
    {
        public TotalWeight TotalWeight { get; set; }
    }

    public class TotalWeight
    {
        public decimal Weight { get; set; }
        public string Unit { get; set; }
    }
}
