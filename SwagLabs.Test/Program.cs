using SwagLabs.Dal.EfStructures;
using SwagLabs.Models.Entities;

AppDbContext context = new AppDbContextFactory().CreateDbContext(null);

//User user = new()
//{
//    UserName = "test",
//    Password = "pass",
//};

//context.Users.Add(user);

User user = context.Users.First();
user.Password = "newpass";

context.SaveChanges();