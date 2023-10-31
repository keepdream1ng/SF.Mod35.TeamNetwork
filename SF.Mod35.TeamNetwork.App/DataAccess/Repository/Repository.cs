using Microsoft.EntityFrameworkCore;

namespace SF.Mod35.TeamNetwork.App.DataAccess.Repository;

/// <summary>
/// Repository stripped from save changes methods, since we using UnitOfWork pattern.
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
	protected DbContext _db;

	public DbSet<T> Set { get; private set; }

	public Repository(ApplicationDbContext db)
	{
		_db = db;
		var set = _db.Set<T>();
		set.Load();

		Set = set;
	}
	// Below methods are syncronos since we are working 
	// with a objects in the app memory and saving with UoW asyncronosly.
	public virtual IEnumerable<T> GetAll()
	{
		return Set;
	}

	public virtual T Get(int id)
	{
		return Set.Find(id);
	}

	public virtual void Create(T item)
	{
		Set.Add(item);
	}

	public virtual void Delete(T item)
	{
		Set.Remove(item);
	}

	public virtual void Update(T item)
	{
		Set.Update(item);
	}
}
