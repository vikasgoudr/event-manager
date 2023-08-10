using EventManager.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EventManager.DAL.Context;

public class EventManagerDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options):base(options)
    {
        
    }
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<Question> Questions { get; set; }
    public virtual DbSet<Answer> Answers { get; set; }
    public virtual DbSet<PaymentTier> PaymentTiers { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<AppRole> Roles { get; set; }

    public virtual DbSet<Ticket> Ticket { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Event>()
            .HasOne(x => x.Organizer)
            .WithMany(x => x.Events)
            .HasForeignKey(x => x.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Event>()
            .HasOne(x => x.Address);
        builder.Entity<Question>()
            .HasOne(x => x.Event)
            .WithMany(x => x.Questions)
            .HasForeignKey(x => x.EventId);
        builder.Entity<Answer>()
            .HasOne(x => x.Question)
            .WithMany(x => x.Answers)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Answer>()
            .HasOne(x => x.Answerer)
            .WithMany(x => x.Answers)
            .HasForeignKey(x => x.AnswererId);
        builder.Entity<PaymentTier>()
            .HasOne(x => x.Event)
            .WithMany(x => x.PaymentTiers)
            .HasForeignKey(x => x.EventId);
        builder.Entity<AppUser>()
            .HasOne(x => x.Address);
    }
}
