

namespace keepr_csharp.Services;

public class VaultsService
{
  private readonly VaultsRepository _repository;

  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault newVault = _repository.Create(vaultData);
    return newVault;
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
      throw new Exception($"Invalid id: {vaultId} 🦹");
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
}