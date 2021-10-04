using System;
using Infrastructure.Models;
using UnitsNet;
using UnitsNet.Units;

namespace Infrastructure.Services
{
    public interface IUomHelper
    {
        public double ConvertTotalWeight(TotalWeight tw, string uom);
    }
    
    public class UomHelper : IUomHelper
    {
        public double ConvertTotalWeight(TotalWeight tw, string endUom)
        {
            var currentUomAbbrev = GetUomAbbrev(tw.Unit);
            var endUomAbbrev = GetUomAbbrev(endUom);

            var currentUnitType = UnitParser.Default.Parse(currentUomAbbrev, typeof(MassUnit));
            var endUnitType = UnitParser.Default.Parse(endUomAbbrev, typeof(MassUnit));
            return UnitConverter.Convert(tw.Weight, currentUnitType, endUnitType);
        }
        
        private static string GetUomAbbrev(string unit)
        {
            switch (unit.ToLower())
            {
                case "ounce":
                case "ounces":
                case "oz":
                    return "oz";
                
                case "kilogram":
                case "kilograms":
                case "kg":
                    return "kg";
                
                case "pound":
                case "pounds":
                case "lbs":
                    return "lbs";

                default:
                    throw new InvalidOperationException($"{unit} is not supported as a unit for weight conversion");
            }
        }
    }
}