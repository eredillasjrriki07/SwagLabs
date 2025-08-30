using Microsoft.EntityFrameworkCore;
using SwagLabs.Dal.EfStructures;
using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;

namespace SwagLabs.Dal.Repos;

public class ProductRepo : TemporalTableBaseRepo<Product>, IProductRepo
{
    public ProductRepo(AppDbContext context)
        : base(context)
    {
    }

    internal ProductRepo(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public override async Task<IEnumerable<Product>> GetAllAsync()
        => await BuildQuery().ToListAsync();

    internal IOrderedQueryable<Product> BuildQuery()
        => Table.OrderBy(p => p.Name);
}
