using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationController : Controller
    {
        private readonly ShipmentContext _ctx;
        
        public OrganizationController(ShipmentContext ctx)
        {
            _ctx = ctx;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostOrg(Organization org)
        {
            var entity = await _ctx.Organizations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == org.Id);
            if (entity != null)
            {
                _ctx.Update(org);
            }
            else
            {
                await _ctx.AddAsync(org);
            }
            
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<Organization>> GetOrg(Guid id)
        {
            return await _ctx.Organizations.FindAsync(id);
        }
    }
}