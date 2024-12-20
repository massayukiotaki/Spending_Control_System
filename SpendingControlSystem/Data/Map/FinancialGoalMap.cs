using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpendingControlSystem.Entities;

namespace SpendingControlSystem.Data.Map
{
    public class FinancialGoalMap : IEntityTypeConfiguration<FinancialGoal>
    {
        public void Configure(EntityTypeBuilder<FinancialGoal> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(true);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName($"{nameof(FinancialGoal)}Id");

            builder.Property(x => x.Description).IsRequired().HasMaxLength(250);
            builder.Property(x => x.ValueTarget).IsRequired().HasPrecision(10, 2);
            builder.Property(x => x.DateTarget).IsRequired();

            builder.Property(x => x.DataHoraInclusao).IsRequired();

            builder.Property(x => x.UsuarioInclusao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.DataHoraAlteracao).IsRequired();

            builder.Property(x => x.UsuarioAlteracao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
