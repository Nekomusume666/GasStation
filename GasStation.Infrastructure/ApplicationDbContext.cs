using GasStation.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Domain.Models.GasStation> GasStations { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Pump> Pumps { get; set; }
        public DbSet<News> News { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка сущности Client
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client"); // Имя таблицы в БД

                entity.HasKey(e => e.ID_Client); // Первичный ключ

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(30);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BonusPoints)
                    .HasDefaultValue(0);
            });

            // Настройка сущности Administrator
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("Administrator");

                entity.HasKey(e => e.ID_Administrator);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Настройка сущности GasStation
            modelBuilder.Entity<Domain.Models.GasStation>(entity =>
            {
                entity.ToTable("GasStation");

                entity.HasKey(e => e.ID_GasStation);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Coordinates)
                    .HasMaxLength(50);

                entity.HasOne(e => e.Administrator)
                    .WithMany()
                    .HasForeignKey(e => e.ID_Administrator)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка сущности Fuel
            modelBuilder.Entity<Fuel>(entity =>
            {
                entity.ToTable("Fuel");

                entity.HasKey(e => e.ID_Fuel);

                entity.Property(e => e.PricePerLiter)
                    .IsRequired()
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.HasOne(e => e.GasStation)
                    .WithMany(g => g.Fuels)
                    .HasForeignKey(e => e.ID_GasStation)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.FuelType)
                    .WithMany(g => g.Fuels)
                    .HasForeignKey(e => e.ID_FuelType)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка сущности Transactions
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.ToTable("Transactions");

                entity.HasKey(e => e.ID_Transactions);

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.Cost)
                    .IsRequired()
                    .HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Date)
                    .IsRequired();

                entity.Property(e => e.BonusPoints);

                entity.HasOne(e => e.Client)
                    .WithMany(c => c.Transactions)
                    .HasForeignKey(e => e.ID_Client)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Fuel)
                    .WithMany()
                    .HasForeignKey(e => e.ID_Fuel)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Pump)
                    .WithMany()
                    .HasForeignKey(e => e.ID_Pump)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка сущности FuelType
            modelBuilder.Entity<FuelType>(entity =>
            {
                entity.ToTable("FuelType");

                entity.HasKey(e => e.ID_FuelType);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Настройка сущности Pump
            modelBuilder.Entity<Pump>(entity =>
            {
                entity.ToTable("Pump");

                entity.HasKey(e => e.ID_Pump);

                entity.HasOne(e => e.GasStation)
                    .WithMany()
                    .HasForeignKey(e => e.ID_GasStation)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.FuelType)
                    .WithMany()
                    .HasForeignKey(e => e.ID_FuelType)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка сущности Supply
            modelBuilder.Entity<Supply>(entity =>
            {
                entity.ToTable("Supply");

                entity.HasKey(e => e.ID_Supply);

                entity.Property(e => e.SupplyDate)
                    .IsRequired();

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.Cost)
                    .IsRequired()
                    .HasColumnType("decimal(10, 2)");

                entity.HasOne(e => e.GasStation)
                    .WithMany()
                    .HasForeignKey(e => e.ID_GasStation)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Employee)
                    .WithMany()
                    .HasForeignKey(e => e.ID_Employee)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Fuel)
                    .WithMany()
                    .HasForeignKey(e => e.ID_Fuel)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка сущности News
            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("News");

                entity.HasKey(e => e.ID_News);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Description)
                    .IsRequired();

                entity.Property(e => e.StartDate)
                    .IsRequired();

                entity.Property(e => e.EndDate)
                    .IsRequired();

                entity.HasOne(e => e.Administrator)
                    .WithMany()
                    .HasForeignKey(e => e.ID_Administrator)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Настройка сущности Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasKey(e => e.ID_Employee);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(e => e.GasStation)
                    .WithMany()
                    .HasForeignKey(e => e.ID_GasStation)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
