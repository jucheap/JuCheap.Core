using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JuCheap.Core.Data
{
    [DbContext(typeof(JuCheapContext))]
    internal class JuCheapContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JuCheap.Users", b =>
            {
                b.Property<int>("Id").ValueGeneratedOnAdd();
                b.Property<string>("LoginName").IsRequired().HasMaxLength(20);
                b.Property<string>("Email").IsRequired().HasMaxLength(36);
                b.Property<string>("Password").IsRequired().HasMaxLength(36);
                b.Property<string>("RealName").IsRequired().HasMaxLength(20);
                b.Property<bool>("IsSuperMan").IsRequired();
                b.ToTable("Users");
            });
        }
    }
}
