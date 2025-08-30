using SwagLabs.Dal.EfStructures;
using SwagLabs.Models.Entities;

AppDbContext context = new AppDbContextFactory().CreateDbContext(null);

Product product = new Product()
{
    Name = "HotSiLog",
    Description = "Hot Sinaing Itlog, (kala mo sinangag no?)",
    Price = 99.45
};

context.Products.Add(product);

context.SaveChanges();