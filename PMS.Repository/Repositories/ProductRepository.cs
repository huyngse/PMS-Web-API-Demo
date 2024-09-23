using Microsoft.EntityFrameworkCore;
using PMS.Repository.Base;
using PMS.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(SWP391ProductManagementSystemContext context) => _context = context;

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.ProductCategory).ToListAsync();            
        }

        public async Task<Product> GetByIdAsync(int id)
        {    
            var result = await _context.Products.Include(p => p.ProductCategory).FirstAsync(p => p.ProductId == id);
            
            return result;            
        }
    }
}
