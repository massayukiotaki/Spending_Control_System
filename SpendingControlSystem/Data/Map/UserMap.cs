using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendingControlSystem.Entities;

namespace SpendingControlSystem.Data.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id).IsClustered(true);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName($"{nameof(User)}Id");

            builder.HasMany(x => x.Budgets)
                   .WithOne(x => x.User)
                   .HasForeignKey("UserId")
                   .IsRequired();

            builder.HasMany(x => x.Costs)
                   .WithOne(x => x.User)
                   .HasForeignKey("UserId")
                   .IsRequired();

            builder.HasMany(x => x.FinancialGoals)
                   .WithOne(x => x.User)
                   .HasForeignKey("UserId")
                   .IsRequired();

            builder.HasMany(x => x.Incomes)
                   .WithOne(x => x.User)
                   .HasForeignKey("UserId")
                   .IsRequired();
            
            builder.HasMany(x => x.Investments)
                   .WithOne(x => x.User)
                   .HasForeignKey("UserId")
                   .IsRequired();            
            
            builder.HasMany(x => x.MonthlyReports)
                   .WithOne(x => x.User)
                   .HasForeignKey("UserId")
                   .IsRequired();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(80);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Birthdate).IsRequired();

            builder.Property(x => x.DataHoraInclusao).IsRequired();

            builder.Property(x => x.UsuarioInclusao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.DataHoraAlteracao).IsRequired();
                
            builder.Property(x => x.UsuarioAlteracao).HasMaxLength(30).IsRequired();

            builder.Property(x => x.IsActive).IsRequired();
        }
    }
}
