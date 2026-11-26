using Microsoft.EntityFrameworkCore;
using SMartSHOP.Api.Data.Seed;
using SMartSHOP.Api.Models;

namespace SMartSHOP.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        await using var transaction = await db.Database.BeginTransactionAsync();

        var existingCategories = await db.Categories.ToListAsync();
        var byName = existingCategories.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);

        var newCategories = CatalogSeedData.Categories
            .Where(name => !byName.ContainsKey(name))
            .Select(name => new Category { Name = name })
            .ToList();

        if (newCategories.Count > 0)
        {
            db.Categories.AddRange(newCategories);
            await db.SaveChangesAsync();

            foreach (var category in newCategories)
            {
                byName[category.Name] = category;
            }
        }

        var existingProductNamesList = await db.Products
            .Select(p => p.Name)
            .ToListAsync();

        var existingProductNames = new HashSet<string>(
            existingProductNamesList,
            StringComparer.OrdinalIgnoreCase);

        var newProducts = CatalogSeedData.Products
            .Where(p => !existingProductNames.Contains(p.Name) && byName.ContainsKey(p.Category))
            .Select(p => new Product
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = byName[p.Category].Id
            })
            .ToList();

        if (newProducts.Count > 0)
        {
            db.Products.AddRange(newProducts);
            await db.SaveChangesAsync();
        }

        await transaction.CommitAsync();
    }
}