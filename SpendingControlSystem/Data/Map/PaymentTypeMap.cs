using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SpendingControlSystem.Entities;

namespace SpendingControlSystem.Data.Map
{
    public class PaymentTypeMap : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(true);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName($"{nameof(PaymentType)}Id");

            builder.HasMany(x => x.Costs)
                   .WithOne(x => x.PaymentType)
                   .HasForeignKey("PaymentTypeId")
                   .IsRequired();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);

            builder.Property(x => x.DataHoraInclusao).IsRequired();

            builder.Property(x => x.UsuarioInclusao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.DataHoraAlteracao).IsRequired();

            builder.Property(x => x.UsuarioAlteracao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
