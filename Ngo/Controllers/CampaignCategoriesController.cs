using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ngo.Data;
using Ngo.Models;

namespace Ngo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CampaignCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CampaignCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignCategory>>> GetCampaignCategories()
        {
            return await _context.CampaignCategories.Include(c=>c.Campaigns).ToListAsync();
        }

        // GET: api/CampaignCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignCategory>> GetCampaignCategory(int id)
        {
            var campaignCategory = await _context.CampaignCategories.FindAsync(id);

            if (campaignCategory == null)
            {
                return NotFound();
            }

            return campaignCategory;
        }

        // PUT: api/CampaignCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampaignCategory(int id, CampaignCategory campaignCategory)
        {
            if (id != campaignCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(campaignCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampaignCategoryExists(id))
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

        // POST: api/CampaignCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CampaignCategory>> PostCampaignCategory(CampaignCategory campaignCategory)
        {
            _context.CampaignCategories.Add(campaignCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampaignCategory", new { id = campaignCategory.CategoryId }, campaignCategory);
        }

        // DELETE: api/CampaignCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CampaignCategory>> DeleteCampaignCategory(int id)
        {
            var campaignCategory = await _context.CampaignCategories.FindAsync(id);
            if (campaignCategory == null)
            {
                return NotFound();
            }

            _context.CampaignCategories.Remove(campaignCategory);
            await _context.SaveChangesAsync();

            return campaignCategory;
        }

        private bool CampaignCategoryExists(int id)
        {
            return _context.CampaignCategories.Any(e => e.CategoryId == id);
        }
    }
}
