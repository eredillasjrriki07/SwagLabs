
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwagLabs.Dal.EfStructures;
using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;

namespace SwagLabs.Dal.Repos;

public class UserRepo : TemporalTableBaseRepo<User>, IUserRepo
{
    public UserRepo(AppDbContext context)
        : base(context)
    {
    }

    internal UserRepo(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public async Task<User> FindByUsernameAsync(string username)
        => await BuildQuery().FirstOrDefaultAsync(u => u.UserName == username);

    public override async Task<IEnumerable<User>> GetAllAsync()
        => await BuildQuery().ToListAsync();

    public override async Task<IEnumerable<User>> GetAllIgnoreQueryFiltersAsync()
        => await BuildQuery().ToListAsync();

    internal IOrderedQueryable<User> BuildQuery()
        => Table.OrderBy(u => u.UserName);
}
