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
    public class CampaignCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CampaignCategoriesController> _logger;

        public CampaignCategoriesController(
            ApplicationDbContext context,
            ILogger<CampaignCategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }
      
        // GET: api/CampaignCategories
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<CampaignCategory>>> GetCampaignCategories()
        //{
        //    return await _context.CampaignCategories.Include(c => c.Campaigns).ToListAsync();
        //}

        public async Task<IActionResult> GetCampaignCategories()
        {
            try
            {
                var categories = await _context.CampaignCategories
                                     .Include(c => c.Campaigns)
                                     .ToListAsync();

                // Check if data exists in the Database
                if (categories == null)
                {
                    return NotFound();          // RETURN: No data was found            HTTP 404
                }
                return Ok(categories);          // RETURN: OkObjectResult - good result HTTP 200
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message); // RETURN: BadResult                    HTTP 400
            }
        }

        // GET: api/CampaignCategories/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<CampaignCategory>> GetCampaignCategory(int id)
        //{
        //    var campaignCategory = await _context.CampaignCategories.FindAsync(id);

        //    if (campaignCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    return campaignCategory;
        //}
        public async Task<IActionResult> GetCampaignCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var category = await _context.CampaignCategories.FindAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
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
        public async Task<IActionResult> PostCampaignCategory(CampaignCategory category)
        {
            // Sanitize the Data
            category.CategoryName = category.CategoryName.Trim();

            // Server Side Validation
            bool isDuplicateFound = _context.CampaignCategories.Any(c => c.CategoryName == category.CategoryName);
            if (isDuplicateFound)
            {
                ModelState.AddModelError("POST", "Duplicate Category Found!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.CampaignCategories.Add(category);
                    await _context.SaveChangesAsync();

                    // return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);

                    // To enforce that the HTTP RESPONSE CODE 201 "CREATED", package the response.
                    var result = CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
                    return Ok(result);
                }
                catch (System.Exception exp)
                {
                    ModelState.AddModelError("POST", exp.Message);
                }
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaignCategory(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            try
            {
                var category = await _context.CampaignCategories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                _context.CampaignCategories.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(category);
            }
            catch (System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);
            }
        }

        //public async Task<ActionResult<CampaignCategory>> PostCampaignCategory(CampaignCategory campaignCategory)
        //{
        //    _context.CampaignCategories.Add(campaignCategory);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCampaignCategory", new { id = campaignCategory.CategoryId }, campaignCategory);
        //}

        //// DELETE: api/CampaignCategories/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<CampaignCategory>> DeleteCampaignCategory(int id)
        //{
        //    var campaignCategory = await _context.CampaignCategories.FindAsync(id);
        //    if (campaignCategory == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CampaignCategories.Remove(campaignCategory);
        //    await _context.SaveChangesAsync();

        //    return campaignCategory;
        //}

        private bool CampaignCategoryExists(int id)
        {
            return _context.CampaignCategories.Any(e => e.CategoryId == id);
        }
    }
}
