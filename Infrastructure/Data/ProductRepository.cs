using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                        .Include(p=>p.ProductBrand)
                        .Include(p=>p.ProductType)
                        .FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products
                    .Include(p=>p.ProductBrand)
                    .Include(p=>p.ProductType)
                    .ToListAsync();
        }

        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}