using MessageDrop.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace MessageDrop.EF
{
    public class MessageDropDataContext : DbContext
    {
        public MessageDropDataContext()
        {

        }

        public MessageDropDataContext(DbContextOptions<MessageDropDataContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        // Define DbSets 
        public DbSet<Message> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("" +
                "Data Source=(localdb)\\MSSQLLocalDB;" +
                "Initial Catalog=MessageDropAppData;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=False;" +
                "TrustServerCertificate=False;" +
                "ApplicationIntent=ReadWrite;" +
                "MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().ToTable("Message");
        }

    }
}
