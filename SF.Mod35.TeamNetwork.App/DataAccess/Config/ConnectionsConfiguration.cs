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
	}
}
