using Microsoft.EntityFrameworkCore;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.DataAccess.Repository;

public class DialogRepository : Repository<Dialog>
{
	public DialogRepository(ApplicationDbContext db) : base(db)
	{
	}

	public Dialog GetDialogByUsers(string user1Id, string user2Id)
	{
		return Set.Include(d => d.Messages)
			//.AsEnumerable()
			.Where(d => (d.User1Id == user1Id && d.User2Id == user2Id) || (d.User1Id == user2Id && d.User2Id == user1Id))
			.FirstOrDefault();
	}
}
