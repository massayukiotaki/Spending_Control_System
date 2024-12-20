using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpendingControlSystem.Entities;

namespace SpendingControlSystem.Data.Map
{
    public class InvestmentTransactionMap : IEntityTypeConfiguration<InvestmentTransaction>
    {
        public void Configure(EntityTypeBuilder<InvestmentTransaction> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(true);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName($"{nameof(InvestmentTransaction)}Id");

            builder.Property(x => x.Type).IsRequired().HasMaxLength(100);
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
