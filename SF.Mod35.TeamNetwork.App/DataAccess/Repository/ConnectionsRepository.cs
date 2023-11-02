using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SF.Mod35.TeamNetwork.ClassLibrary.Models;

namespace SF.Mod35.TeamNetwork.App.DataAccess.Repository;

public class ConnectionsRepository : Repository<Connection>
{
	public ConnectionsRepository(ApplicationDbContext db) : base(db)
	{
	}

	public bool CreateConnection(User user, User target, ConnectionStatus status)
	{
		try
		{
			var connection = new Connection()
			{
				UserId = user.Id,
				User = user,
				ConnectedUserId = target.Id,
				ConnectedUser = target,
				Status = status
			};
			Create(connection);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			return false;
		}
		return true;
	}

	public Connection GetConnection(User user, User target)
	{
		return Set.AsEnumerable()
			.LastOrDefault(c => (c.UserId == user.Id) && (c.ConnectedUserId == target.Id));
	}
	public ConnectionStatus GetConnectionStatus(User user, User target)
	{
		var connection = GetConnection(user, target);
		return connection != null ? connection.Status : ConnectionStatus.Absent;
	}

	public List<User> GetConnectedWithUser(User user, ConnectionStatus status)
	{
		var queryForConnected = Set
			.Include(c => c.ConnectedUser)
			.AsEnumerable()
			.Where(c => c.UserId == user.Id)
			.Where(c => c.Status == status)
			.Select(c => c.ConnectedUser);
		return queryForConnected.ToList();
	}

	public bool SetConnectionStatus(User user, User target, ConnectionStatus status)
	{
		var connection = GetConnection(user, target);
		if (connection != null)
		{
			connection.Status = status;
			Update(connection);
			return true;
		}
		return false;
	}
}
