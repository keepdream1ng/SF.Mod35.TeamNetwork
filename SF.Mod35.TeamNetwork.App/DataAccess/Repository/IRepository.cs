namespace SF.Mod35.TeamNetwork.App.DataAccess.Repository;

public interface IRepository<T> where T : class
{
	IEnumerable<T> GetAll();
	T Get(int id);
	void Create(T item);
	void Update(T item);
	void Delete(T item);
}
