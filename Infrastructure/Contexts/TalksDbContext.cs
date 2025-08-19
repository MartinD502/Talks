using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class TalksDbContext : DbContext
    {
        public TalksDbContext(DbContextOptions<TalksDbContext> options) : base(options){}

        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Certification> Certifications { get; set; }        
    }
}