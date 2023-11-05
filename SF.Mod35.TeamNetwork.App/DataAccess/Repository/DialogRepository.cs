using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.DataAccess.Repository;

public class DialogRepository : Repository<Dialog>
{
	public DialogRepository(ApplicationDbContext db) : base(db)
	{
	}
}
