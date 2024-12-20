using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpendingControlSystem.Entities;

namespace SpendingControlSystem.Data.Map
{
    public class InvestmentMap : IEntityTypeConfiguration<Investment>
    {
        public void Configure(EntityTypeBuilder<Investment> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(true);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName($"{nameof(Investment)}Id");

            builder.HasMany(x => x.InvestmentTransactions)
                   .WithOne(x => x.Investments)
                   .HasForeignKey("InvestmentId")
                   .IsRequired();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Value).IsRequired().HasPrecision(10, 2);
            builder.Property(x => x.Date).IsRequired();

            builder.Property(x => x.DataHoraInclusao).IsRequired();

            builder.Property(x => x.UsuarioInclusao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.DataHoraAlteracao).IsRequired();

            builder.Property(x => x.UsuarioAlteracao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
