using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.DataAccess.Config;

public class DialogConfiguration : IEntityTypeConfiguration<Dialog>
{
	public void Configure(EntityTypeBuilder<Dialog> builder)
	{
		builder.ToTable("Dialogs").HasKey(p => p.Id);
		builder.Property(x => x.Id).UseIdentityColumn();

        builder
            .HasOne<User>(x => x.User1)
            .WithMany()
            .HasForeignKey(x => x.User1Id)
            .OnDelete(DeleteBehavior.ClientNoAction);
        // Our network doesnt support deleting user profile anyway...

        builder
            .HasOne<User>(x => x.User2)
            .WithMany()
            .HasForeignKey(x => x.User2Id)
            .OnDelete(DeleteBehavior.ClientNoAction);
	}
}
