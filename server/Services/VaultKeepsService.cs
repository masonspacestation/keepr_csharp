using keepr_csharp.Interfaces;

namespace keepr_csharp.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;

  public VaultKeepsService(VaultKeepsRepository repository)
  {
    _repository = repository;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vkData)
  {
    VaultKeep newVk = _repository.Create(vkData);
    return newVk;
  }
}