using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMBTools.Web.DAL.Models;

namespace SMBTools.Web.DAL.Infrastructure.ModelsConfiguration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasIndex(i => i.Email).IsUnique();
    }
}