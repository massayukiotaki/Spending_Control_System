using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpendingControlSystem.Entities;

namespace SpendingControlSystem.Data.Map
{
    public class MonthlyReportMap : IEntityTypeConfiguration<MonthlyReport>
    {
        public void Configure(EntityTypeBuilder<MonthlyReport> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(true);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName($"{nameof(MonthlyReport)}Id");

            builder.Property(x => x.YearMonth).IsRequired();
            builder.Property(x => x.TotalRevenues).IsRequired().HasPrecision(10, 2);
            builder.Property(x => x.TotalCosts).IsRequired().HasPrecision(10, 2);
            builder.Property(x => x.TotalInvestments).IsRequired().HasPrecision(10, 2);

            builder.Property(x => x.DataHoraInclusao).IsRequired();

            builder.Property(x => x.UsuarioInclusao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.DataHoraAlteracao).IsRequired();

            builder.Property(x => x.UsuarioAlteracao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
