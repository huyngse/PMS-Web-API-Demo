using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PMS.Repository;
using PMS.Repository.Models;

namespace PMS.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {        
        private readonly UnitOfWork _unitOfWork;

        public ProductsController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();            
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {            
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }        

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {           
            try
            {
                await _unitOfWork.ProductRepository.CreateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }            

            try
            {                
                _unitOfWork.ProductRepository.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {            
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _unitOfWork.ProductRepository.RemoveAsync(product);            

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _unitOfWork.ProductRepository.GetByIdAsync(id) != null;            
        }
    }
}
