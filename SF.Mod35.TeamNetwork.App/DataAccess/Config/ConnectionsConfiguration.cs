using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.DataAccess.Config;

public class ConnectionsConfiguration : IEntityTypeConfiguration<Connection>
{
	public void Configure(EntityTypeBuilder<Connection> builder)
	{
		builder.ToTable("UsersConnections").HasKey(p => p.Id);
		builder.Property(x => x.Id).UseIdentityColumn();
		builder.Property(x => x.Status).IsRequired().HasConversion<byte>();

        // SQL keept yelling on me about cascade cicles till i configured this.
        builder
            .HasOne<User>(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.ClientNoAction);
        // Our network doesnt support deleting user profile anyway...

        builder
            .HasOne<User>(x => x.ConnectedUser)
            .WithMany()
            .HasForeignKey(x => x.ConnectedUserId)
            .OnDelete(DeleteBehavior.ClientNoAction);
	}
}
