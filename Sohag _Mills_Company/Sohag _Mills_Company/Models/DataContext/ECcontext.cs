//using Sohag__Mills_Company.Models.Configration;
using Sohag__Mills_Company.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace Sohag__Mills_Company.Models.DataContext
{
    //public class ECcontext : IdentityDbContext<ApplicationUser>
    public class ECcontext : IdentityDbContext
    {
        //public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CampanyTeam> CampanyTeams { get; set; }

        public DbSet<ImagesCarousel> ImagesCarousels { get; set; }

        
        public DbSet<IsoCertification> IsoCertifications { get; set; }

        public DbSet<clients> Clients { get; set; }



        public DbSet<FuturePlan> FuturePlans { get; set; }

        public DbSet<FuturePlan_Details> FuturePlan_details { get; set; }

        public DbSet<Indicators> Indicators { get; set; }
        public DbSet<FilePdf> FilePdfs { get; set; }

        public DbSet<Indicators_Details> Indicators_details { get; set; }

        public DbSet<Shareholder_Structure> Shareholder_structures { get; set; }
        public DbSet<About_Company> About_Companies { get; set; }
        public DbSet<Statement> Statements { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Region_Sector_Details> Region_Sector_Details { get; set; }
        public DbSet<PhonesSectors> PhonesSectors { get; set; }


        public ECcontext(DbContextOptions<ECcontext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-P3J7SRH\\SQLEXPRESS;Initial Catalog=GraduationProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Sector>()
                .HasMany(rsd => rsd.Region_Sector_Details)
                .WithOne(ps => ps.Sector)
                  .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Region_Sector_Details>()
                    .HasMany(rsd => rsd.PhonesSectors)
                    .WithOne(ps => ps.Phones_Address_Details)
                      .OnDelete(DeleteBehavior.Cascade);



            builder.Entity<CampanyTeam>().HasQueryFilter(c => !c.IsDeleted);


            builder.Entity<ImagesCarousel>().HasQueryFilter(c => !c.IsDeleted);
            
            builder.Entity<IsoCertification>().HasQueryFilter(c => !c.IsDeleted);

            //For Delete the child with master
            builder.Entity<FuturePlan>()
                    .HasMany(rsd => rsd.PlanDetails)
                    .WithOne(ps => ps.FuturePlan)
                    .OnDelete(DeleteBehavior.Cascade);

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID_Admin = "ad376a8f-9eab-4bb9-9fca-30b01540f445";
            const string ROLE_ID_USER = "ad376a8f-9eab-4bb9-9fca-30b01540f777";
            //Add Only Tow Role 
            //Id = "ROLE_ID_Admin",
            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole { Id = ROLE_ID_Admin,Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Name = "User", NormalizedName = "USER" }
                    );
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID_Admin,
                UserId = ADMIN_ID
            });

            builder.Entity<IdentityUser>().HasData(
             new IdentityUser
             {
                 Id = ADMIN_ID,
                 Email = "Admin123@gmail.com",
                 UserName = "Admin123",
                 NormalizedEmail = "ADMIN123@GMAIL.COM",
                 NormalizedUserName = "ADMIN123",
                 PasswordHash = "AQAAAAIAAYagAAAAEH2XzyoLz+knSF5ueC5ART89pb8N4CR8g25c+L8w7w2ZQzQTlw+X551f87NqrjHqNg==", // Admin@123
             });








            base.OnModelCreating(builder);

            //const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            //const string ROLE_ID_Admin = "ad376a8f-9eab-4bb9-9fca-30b01540f445";
            //const string ROLE_ID_USER = "ad376a8f-9eab-4bb9-9fca-30b01540f777";
            //var role = builder.Entity<IdentityRole>().HasData(
            //    new IdentityRole
            //    {
            //        Id = ROLE_ID_Admin,
            //        Name = "Admin",
            //        NormalizedName = "ADMIN"
            //    },
            //    new IdentityRole
            //    {
            //        Id = ROLE_ID_USER,
            //        Name = "User",
            //        NormalizedName = "User"
            //    });
            //builder.Entity<ApplicationUser>().HasData(
            //     new ApplicationUser
            //     {
            //         Id = ADMIN_ID,
            //         Email = "Ayman123@gmail.com",
            //         UserName = "Ayman123@gmail.com",
            //         NormalizedEmail = "AYMAN123@GMAIL.COM",
            //         NormalizedUserName = "AYMAN123@GMAIL.COM",
            //         PasswordHash = "AQAAAAIAAYagAAAAEH2XzyoLz+knSF5ueC5ART89pb8N4CR8g25c+L8w7w2ZQzQTlw+X551f87NqrjHqNg==", // Admin@123
            //         FirstName = " Ayman",
            //         LastName = "Sayed",
            //         Adress = "Cairo , Egypt",
            //         Phone = "01142202287"
            //     });








            //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    RoleId = ROLE_ID_Admin,
            //    UserId = ADMIN_ID
            //});
            builder.ApplyConfiguration(new CompanyConfigration());
            builder.ApplyConfiguration(new FilePdfConfigration());
                //builder.ApplyConfiguration(new CategoryConfigration());
            //    //builder.ApplyConfiguration(new BrandConfigration());


            //}
        }
    }
}
