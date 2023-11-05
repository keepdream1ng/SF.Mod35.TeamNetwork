using SF.Mod35.TeamNetwork.App.DataAccess.Repository;

namespace SF.Mod35.TeamNetwork.App.DataAccess.UoW;

public interface IUnitOfWork : IDisposable
{
	ConnectionsRepository ConnectionsRepo { get; }
	DialogRepository DialogRepo { get; }
	MessagesRepository MessagesRepo { get; }
	void Dispose();
	Task<int> SaveChanges(bool ensureAutoHistory = false);
}