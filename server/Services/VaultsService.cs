

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
}