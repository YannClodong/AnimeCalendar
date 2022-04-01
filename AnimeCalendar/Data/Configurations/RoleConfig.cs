using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimeCalendar.Data.Configurations;

public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
{
    private const string AdminRoleId = "68663419-0275-4322-9255-26cd1c116ab4";
    
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole()
            {
                Id = AdminRoleId,
                Name = ConstRoles.AdminRole,
                NormalizedName = ConstRoles.AdminRole
            },
            new IdentityRole()
            {
                Name = ConstRoles.AnimeEditor,
                NormalizedName = ConstRoles.AnimeEditor
            }
        );
    }
}