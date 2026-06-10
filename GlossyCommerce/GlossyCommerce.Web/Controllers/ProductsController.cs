using GlossyCommerce.Data;
using GlossyCommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlossyCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products (Used by Customer Page to see items)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.OrderByDescending(p => p.Id).ToListAsync();
        }

        // POST: api/products (Used by Admin Page to add new items)
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        // DELETE: api/products/5 (Used by Admin to remove items)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                // This gracefully catches the SQL Foreign Key constraint error!
                return BadRequest("Cannot delete this product because it is tied to an existing customer order. Data integrity protected.");
            }
        }
    }
}