


namespace keepr_csharp.Services;



public class KeepsService
{

  private readonly KeepsRepository _repository;
  private readonly VaultsService _vaultsService;
  private readonly ProfilesService _profilesService;

  public KeepsService(KeepsRepository repository, VaultsService vaultsService, ProfilesService profilesService)
  {
    _repository = repository;
    _vaultsService = vaultsService;
    _profilesService = profilesService;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repository.Create(keepData);
    return keep;
  }

  internal string DestroyKeep(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);
    if (keep.CreatorId != userId)
    {
      throw new Exception($"Not authorized to delete this keep");
    }
    _repository.Destroy(keepId);
    return $"Removed keep {keep.Name}";
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAll();
    return keeps;
  }

  // internal List<Keep> GetAllKeeps(string userId)
  // {
  //   List<Keep> keeps = _repository.GetAll();
  //   return keeps;
  // }

  internal Keep IncrementVisits(int keepId, string userId)
  {
    _repository.IncrementViews(keepId);
    Keep keep = GetKeepById(keepId, userId);
    return keep;
  }


  internal void IncrementKeeps(int keepId)
  {
    _repository.IncrementKeeps(keepId);
    // Keep keep = GetKeepById(vaultKeepId);
    // return keep;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetById(keepId);
    if (keep == null)
    {
      throw new Exception($"Invalid ID: {keepId}");
    }
    return keep;
  }
  internal Keep GetKeepById(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);

    return keep;
  }

  internal List<Keep> GetKeepsByVaultId(int vaultId, string userId)
  {
    _vaultsService.GetVaultById(vaultId, userId);
    List<Keep> keeps = _repository.GetKeepsByVaultId(vaultId, userId);
    return keeps;
  }

  internal List<Keep> GetProfileKeeps(string profileId, string userId)
  {
    _profilesService.GetProfileById(profileId, userId);
    List<Keep> profileKeeps = _repository.GetProfileKeeps(profileId);
    return profileKeeps;
  }

}

