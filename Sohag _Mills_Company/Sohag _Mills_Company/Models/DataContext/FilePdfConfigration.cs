//using Sohag__Mills_Company.Models.Configration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sohag__Mills_Company.Models.Entity;
using System.Reflection.Emit;

namespace Sohag__Mills_Company.Models.DataContext
{
    public class FilePdfConfigration : IEntityTypeConfiguration<FilePdf>
    {
        public FilePdfConfigration() { }
        public void Configure(EntityTypeBuilder<FilePdf> builder)
        {
            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}