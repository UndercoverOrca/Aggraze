using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aggraze.Infrastructure;

public class AggrazeDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public AggrazeDbContext(DbContextOptions<AggrazeDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}