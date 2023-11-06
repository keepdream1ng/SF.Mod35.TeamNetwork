using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.DataAccess.Config;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
	public void Configure(EntityTypeBuilder<Message> builder)
	{
		builder.ToTable("Messages").HasKey(p => p.Id);
		builder.Property(x => x.Id).UseIdentityColumn();

		builder
			.HasOne<Dialog>(x => x.Dialog)
			.WithMany(d => d.Messages)
			.HasForeignKey(x => x.DialogId)
			.OnDelete(DeleteBehavior.ClientNoAction);
	}
}
