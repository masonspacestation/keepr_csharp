namespace keepr_csharp.Interfaces;

public interface IRepository<T>
{
  public List<T> GetAll();
  public T GetById(int id);
  public T Create(T data);
  public void Destroy(int id);
  public T Udpate(T data);
}