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
    Vault vault = _vaultsService.GetVaultById(vkData.VaultId, vkData.CreatorId);
    if (vkData.CreatorId != vault.CreatorId)
    {
      throw new Exception($"Unauthorized to add to this vault.");
    }
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

  internal string DestroyVaultKeep(int vkId, string userId)
  {
    VaultKeep vk = GetVaultKeepById(vkId);
    Vault vault = _vaultsService.GetVaultById(vk.VaultId, userId);
    // FIXME the check below should be to get by Id
    // NOTE the check below should only really check if the creator is the one doing it. with an and, that means that someone else would be able to delete a public vaultKeep
    // if (vault.IsPrivate == true && vault.CreatorId != userId)
    if (vault.CreatorId != userId)
    {
      throw new Exception($"Unauthorized to delete this vault");
    }
    _repository.Destroy(vkId);
    return $"Removed vault {vault.Name}";
  }


}