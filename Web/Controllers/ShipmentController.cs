using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnitsNet;
using UnitsNet.Units;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShipmentController : Controller
    {
        private readonly ShipmentContext _ctx;
        private readonly List<string> _supportedUnits;

        public ShipmentController(ShipmentContext ctx)
        {
            _ctx = ctx;

            _supportedUnits = new List<string>
            {
                "pounds",
                "kilograms",
                "ounces",
                ""
            };
        }
        
        [HttpPost]
        public async Task<IActionResult> PostShipment(Shipment shipment)
        {
            var entity = await _ctx.Shipments.AsNoTracking().FirstOrDefaultAsync(x => x.ReferenceId == shipment.ReferenceId);
            if (entity != null)
            {
                _ctx.Shipments.Update(shipment);
            }
            else
            {
                await _ctx.Shipments.AddAsync(shipment);
            }

            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Shipment>> GetShipment(string id)
        {
            return await _ctx.Shipments.FindAsync(id);
        }

        [HttpPost]
        [Route("TotalWeight")]
        public async Task<ActionResult<TotalWeight>> GetTotalWeight(string unit)
        {
            var transportPacksList = await _ctx.Shipments.Select(x => x.TransportPacks).ToListAsync();
            var finalUnitAbbrev = GetUomAbbrev(unit);
            if (finalUnitAbbrev == "")
                return BadRequest($"Provided unit '{unit}' is not supported");
            var finalUnit = UnitParser.Default.Parse(finalUnitAbbrev, typeof(MassUnit));
            var total = 0M;
            
            foreach (var transportPacks in transportPacksList)
            {
                foreach (var pack in transportPacks.Nodes)
                {
                    var uom = GetUomAbbrev(pack.TotalWeight.Unit);
                    if (uom == "")
                    {
                        throw new ApplicationException($"{pack.TotalWeight.Unit} is not properly handled");
                    }
                    var unitType = UnitParser.Default.Parse(uom, typeof(MassUnit));
                    total += Convert.ToDecimal(UnitConverter.Convert(pack.TotalWeight.Weight, unitType, finalUnit));
                }
            }

            return new TotalWeight
            {
                Unit = unit,
                Weight = total
            };
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
                    return "";
            }
        }
    }
}