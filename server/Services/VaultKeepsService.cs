using keepr_csharp.Interfaces;

namespace keepr_csharp.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;
  private readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
  {
    _repository = repository;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep vkData)
  {
    VaultKeep newVk = _repository.Create(vkData);
    return newVk;
  }

  private VaultKeep GetVaultKeepById(int vkId)
  {
    VaultKeep vk = _repository.GetById(vkId);
    if (vk == null)
    {
      throw new Exception($"Invalid Id: {vkId}");
    }
    return vk;
  }

  // internal VaultKeep GetVaultKeepById(int vkId, string userId)
  // {
  //   VaultKeep vk = GetVaultKeepById(vkId);
  //   if (vault.CreatorId != userId)
  //   {
  //     throw new Exception($"Unauthorized to ");
  //     }
  // }

  internal string DestroyVault(int vkId, string userId)
  {
    VaultKeep vk = GetVaultKeepById(vkId);
    Vault vault = _vaultsService.GetVaultById(vk.VaultId, userId);
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception($"Unauthorized to delete this vault");
    }
    _repository.Destroy(vkId);
    return $"Removed vault {vault.Name}";
  }
}