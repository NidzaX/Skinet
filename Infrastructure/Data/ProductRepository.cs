using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyCollection<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(x => x.Brand == brand);

        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(x => x.Type == type);
 
        query = sort switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "pirceDesc" => query.OrderByDescending(x => x.Price),
            _ => query.OrderBy(x => x.Name)
        };
       
        return await query.ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async  Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}

