using PMS.Repository.Models;
using PMS.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Repository
{
    public class UnitOfWork
    {
        private SWP391ProductManagementSystemContext _context;
        private ProductRepository _productRepository;

        public UnitOfWork() => _context ??= new SWP391ProductManagementSystemContext();

        public ProductRepository ProductRepository
        {
            get { return _productRepository ??= new ProductRepository(_context); }
        }
    }
}
