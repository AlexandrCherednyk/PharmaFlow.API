namespace PharmaFlow.AdministrationService.Data.DbContexts;

internal class AdministrationServiceDbContext : DbContext
{
    public AdministrationServiceDbContext(DbContextOptions<AdministrationServiceDbContext> options) : base(options)
    {
        //try
        //{
        //    var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

        //    if (dbCreator != null)
        //    {
        //        if (!dbCreator.CanConnect()) dbCreator.Create();
        //        if (!dbCreator.HasTables()) dbCreator.CreateTables();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}
    }

    public DbSet<PharmacyPersistence> Pharmacies { get; set; }

    public DbSet<PharmacyMemberPersistence> PharmacyMembers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PharmacyPersistence>()
            .HasKey(p => p.ID);

        modelBuilder.Entity<PharmacyPersistence>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<PharmacyPersistence>()
            .Property(pm => pm.State)
            .IsRequired()
            .HasConversion<int>();

        modelBuilder.Entity<PharmacyPersistence>()
            .HasQueryFilter(p => p.State != PharmacyStatePersistence.Removed);

        modelBuilder.Entity<PharmacyMemberPersistence>()
            .HasKey(pm => pm.ID);

        modelBuilder.Entity<PharmacyMemberPersistence>()
            .Property(pm => pm.FirstName)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<PharmacyMemberPersistence>()
            .Property(pm => pm.LastName)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<PharmacyMemberPersistence>()
            .Property(pm => pm.Email)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<PharmacyMemberPersistence>()
            .Property(pm => pm.Phone)
            .IsRequired()
            .HasMaxLength(15);

        modelBuilder.Entity<PharmacyMemberPersistence>()
            .Property(pm => pm.State)
            .IsRequired()
            .HasConversion<int>();

        modelBuilder.Entity<PharmacyMemberPersistence>()
            .HasOne(pm => pm.Pharmacy)
            .WithMany(p => p.Members)
            .HasForeignKey(pm => pm.PharmacyID)
            .HasConstraintName("fk_member_pharmacy");

        modelBuilder.Entity<PharmacyMemberPersistence>()
            .HasQueryFilter(p => p.State != PharmacyMemberStatePersistence.Removed);
    }
}
