using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShipmentController : Controller
    {
        private readonly ShipmentContext _ctx;
        private readonly IUomHelper _uomHelper;

        public ShipmentController(ShipmentContext ctx, IUomHelper uomHelper)
        {
            _ctx = ctx;
            _uomHelper = uomHelper;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostShipment(ShipmentVm shipment)
        {
            var entity = await _ctx.Shipments.AsNoTracking().FirstOrDefaultAsync(x => x.ReferenceId == shipment.ReferenceId);
            if (entity != null)
            {
                entity.TransportPacks = shipment.TransportPacks;
                entity.EstimatedTimeArrival = shipment.EstimatedTimeArrival;
                entity.Organizations = await _ctx.Organizations.Where(x => shipment.Organizations.Contains(x.Code)).Select(x => x.Id).ToListAsync();
                _ctx.Shipments.Update(entity);
            }
            else
            {
                var orgIds = await _ctx.Organizations.Where(x => shipment.Organizations.Contains(x.Code)).Select(x => x.Id).ToListAsync();
                if (orgIds.Count != shipment.Organizations.Count)
                {
                    return BadRequest("Unknown organization code provided");
                }
                await _ctx.Shipments.AddAsync(new Shipment
                {
                    ReferenceId = shipment.ReferenceId,
                    EstimatedTimeArrival = shipment.EstimatedTimeArrival,
                    Organizations = orgIds,
                    TransportPacks = shipment.TransportPacks
                });
            }

            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ShipmentVm>> GetShipment(string id)
        {
            var shipment = await _ctx.Shipments.FindAsync(id);
            if (shipment == null)
                return NotFound();

            return new ShipmentVm
            {
                ReferenceId = shipment.ReferenceId,
                EstimatedTimeArrival = shipment.EstimatedTimeArrival,
                Organizations = await _ctx.Organizations.Where(x => shipment.Organizations.Contains(x.Id)).Select(x => x.Code).ToListAsync(),
                TransportPacks = shipment.TransportPacks
            };
        }

        [HttpPost]
        [Route("TotalWeight")]
        public ActionResult<TotalWeightResponse> GetTotalWeight(TotalWeightRequest request)
        {
            try
            {
                var transportPacksList = _ctx.Shipments.Select(x => x.TransportPacks).AsEnumerable();
                var total = transportPacksList
                    .SelectMany(transportPacks => transportPacks.Nodes)
                    .Sum(pack => _uomHelper.ConvertTotalWeight(pack.TotalWeight, request.Unit));

                return new TotalWeightResponse
                {
                    Unit = request.Unit,
                    Weight = total
                };
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}