

namespace keepr_csharp.Services;

public class VaultsService
{
  private readonly VaultsRepository _repository;
  private readonly ProfilesService _profilesService;

  public VaultsService(VaultsRepository repository, ProfilesService profilesService)
  {
    _repository = repository;
    _profilesService = profilesService;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault newVault = _repository.Create(vaultData);
    return newVault;
  }

  private List<Vault> GetAllVaults(string userId)
  {
    List<Vault> vaults = _repository.GetAll();
    return vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId);
  }

  private Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetById(vaultId);
    if (vault == null)
    {
      throw new Exception($"Invalid id: {vaultId}");
    }
    return vault;
  }


  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId);
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception($"Unauthorized to access: {vaultId} 🦹");
    }
    return vault;
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    List<Vault> myVaults = _repository.GetMyVaults(userId);
    return myVaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId);
  }

  internal Vault UpdateVault(int vaultId, Vault vaultData, string userId)
  {
    Vault vaultToUpdate = GetVaultById(vaultId);
    if (vaultToUpdate.CreatorId != userId)
    {
      throw new Exception("Unauthorized to update");
    }
    vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
    vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

    Vault updatedVault = _repository.Udpate(vaultToUpdate);
    return updatedVault;
  }

  internal void DestroyVault(int vaultId, string userId)
  {
    Vault vaultToDestroy = GetVaultById(vaultId);
    if (vaultToDestroy.CreatorId != userId)
    {
      throw new Exception("Unauthorized to destroy this vault.");
    }
    _repository.Destroy(vaultId);
  }

  internal List<Vault> GetProfileVaults(string profileId, string userId)
  {
    _profilesService.GetProfileById(profileId);

    List<Vault> vaults = _repository.GetProfileVaults(profileId);
    return vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId);
  }
}