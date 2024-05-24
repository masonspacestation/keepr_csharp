

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

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetById(vaultId);
    return vault;
  }
}