using Microsoft.EntityFrameworkCore.Infrastructure;
using SF.Mod35.TeamNetwork.App.DataAccess.Repository;

namespace SF.Mod35.TeamNetwork.App.DataAccess.UoW;

/// <summary>
/// Unit of work responsible for saving changes to the database, so repositories dont do that.
/// Also it serves repositories as we need them.
/// </summary>
public class UnitOfWork : IDisposable, IUnitOfWork
{
	private ApplicationDbContext _appContext;
	private ConnectionsRepository _connectionsRepo;
	public ConnectionsRepository ConnectionsRepo
	{
		get
		{
			if (_connectionsRepo == null)
			{
				_connectionsRepo = new ConnectionsRepository(_appContext);
			}
			return _connectionsRepo;
		}
	}

	public UnitOfWork(ApplicationDbContext app)
	{
		this._appContext = app;
	}

	public Task<int> SaveChanges(bool ensureAutoHistory = false)
	{
		return _appContext.SaveChangesAsync(ensureAutoHistory);
	}

	private bool disposed = false;
	protected virtual void Dispose(bool disposing)
	{
		if (!this.disposed)
		{
			if (disposing)
			{
				_appContext.Dispose();
			}
		}
		this.disposed = true;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
