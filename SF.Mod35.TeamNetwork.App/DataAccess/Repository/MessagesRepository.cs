using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.DataAccess.Repository;

public class MessagesRepository : Repository<Message>
{
	public MessagesRepository(ApplicationDbContext db) : base(db)
	{
	}
}
