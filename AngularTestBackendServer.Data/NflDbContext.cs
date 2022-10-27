using AngularTestBackendServer.Data.Entities;
using AngularTestBackendServer.Data.Entities.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AngularTestBackendServer.Data;

public class NflDbContext : DbContext
{
    public virtual DbSet<GameEntity> Games { get; set; }
    public virtual DbSet<ScheduleEntity> Schedules { get; set; }
    public virtual DbSet<SeasonEntity> Seasons { get; set; }
    public virtual DbSet<DivisionEntity> Divisions { get; set; }
    public virtual DbSet<StadiumEntity> Stadiums { get; set; }
    public virtual DbSet<TeamEntity> Teams { get; set; }

    public NflDbContext()
    {
        // no implementation
    }
    
    public NflDbContext(DbContextOptions<NflDbContext> options) : base(options)
    {
        // no implementation
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConferenceEntityMap.Configure(modelBuilder.Entity<ConferenceEntity>());
        DivisionEntityMap.Configure(modelBuilder.Entity<DivisionEntity>());
        GameEntityMap.Configure(modelBuilder.Entity<GameEntity>());
        ScheduleEntityMap.Configure(modelBuilder.Entity<ScheduleEntity>());
        SeasonEntityMap.Configure(modelBuilder.Entity<SeasonEntity>());
        StadiumEntityMap.Configure(modelBuilder.Entity<StadiumEntity>());
        StadiumTypeEntityMap.Configure(modelBuilder.Entity<StadiumTypeEntity>());
        TeamEntityMap.Configure(modelBuilder.Entity<TeamEntity>());
        
        base.OnModelCreating(modelBuilder);
    }
}