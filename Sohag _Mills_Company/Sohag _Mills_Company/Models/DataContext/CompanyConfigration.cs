//using Sohag__Mills_Company.Models.Configration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sohag__Mills_Company.Models.Entity;
using System.Reflection.Emit;

namespace Sohag__Mills_Company.Models.DataContext
{
    public class CompanyConfigration : IEntityTypeConfiguration<CampanyTeam>
    {
        public CompanyConfigration() { }
        public void Configure(EntityTypeBuilder<CampanyTeam> builder)
        {
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}