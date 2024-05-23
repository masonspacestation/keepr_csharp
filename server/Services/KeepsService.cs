

namespace keepr_csharp.Services;



public class KeepsService
{

  private readonly KeepsRepository _repository;

  public KeepsService(KeepsRepository repository)
  {
    _repository = repository;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repository.Create(keepData);
    return keep;
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAll();
    return keeps;
  }
}

