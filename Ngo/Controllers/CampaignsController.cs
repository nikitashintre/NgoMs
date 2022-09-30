using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ngo.Data;
using Ngo.Models;

namespace Ngo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CampaignsController> _logger;
        public CampaignsController(ApplicationDbContext context, ILogger<CampaignsController> logger)
        {
            _context = context;
            _logger = logger;   
        }

        // GET: api/Campaigns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campaign>>> GetCampaigns()
        {
            //return await _context.Campaigns.ToListAsync();
            try
            {
                var campaigns = await _context.Campaigns
                                     .ToListAsync();

                // Check if data exists in the Database
                if (campaigns == null)
                {
                    return NotFound();          // RETURN: No data was found            HTTP 404
                }
                return Ok(campaigns);          // RETURN: OkObjectResult - good result HTTP 200
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message); // RETURN: BadResult                    HTTP 400
            }
        }

        // GET: api/Campaigns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetCampaign(int id)
        {
            try
            {
                var campaign = await _context.Campaigns.FindAsync(id);

                if (campaign == null)
                {
                    return NotFound();
                }

                return Ok(campaign);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message); // RETURN: BadResult                    HTTP 400
            }
        }

        // PUT: api/Campaigns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaign(int id, Campaign campaign)
        {
            if (id != campaign.CamaignId)
            {
                return BadRequest();
            }

            _context.Entry(campaign).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Campaigns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Campaign>> PostCampaign(Campaign campaign)
        {
            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampaign", new { id = campaign.CamaignId }, campaign);
        }

        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Campaign>> DeleteCampaign(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var campaign = await _context.Campaigns.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();

            return Ok(campaign);
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);
            }
        }

        private bool CampaignExists(int id)
        {
            return _context.Campaigns.Any(e => e.CamaignId == id);
        }
    }
}
