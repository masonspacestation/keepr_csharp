using keepr_csharp.Interfaces;

namespace keepr_csharp.Repositories;

public class VaultKeepsRepository : IRepository<VaultKeep>
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }


  private VaultKeep PopulateCreator(VaultKeep vk, Profile profile)
  {
    vk.CreatorId = profile.Id;
    return vk;
  }
  public VaultKeep Create(VaultKeep vkData)
  {
    string sql = @"
    INSERT INTO vaultKeeps(vaultId, keepId, creatorId)
    VALUES(@VaultId, @KeepId, @CreatorId);

 SELECT 
 vaultKeeps.*,
 vaults.*,
 keeps.*,
 accounts.* 
FROM vaultKeeps

    JOIN vaults ON vaultKeeps.vaultId = vaults.id
    JOIN keeps ON vaultKeeps.keepId = keeps.id
    JOIN accounts ON accounts.id = vaultKeeps.creatorId
    WHERE vaultKeeps.id = LAST_INSERT_ID()
    ;";


    VaultKeep newVk = _db.Query<VaultKeep, Vault, Keep, Profile, VaultKeep>(sql, (newVk, vault, keep, profile) =>
    {
      newVk.VaultId = vault.Id;
      newVk.KeepId = keep.Id;
      newVk.CreatorId = profile.Id;
      return newVk;
    }, vkData).FirstOrDefault();
    return newVk;
  }

  public void Destroy(int id)
  {
    throw new NotImplementedException();
  }

  public List<VaultKeep> GetAll()
  {
    throw new NotImplementedException();
  }

  public VaultKeep GetById(int vaultKeepId)
  {
    throw new NotImplementedException();
    //     string sql = @"
    //     SELECT vaultKeeps.*, vaults.*, keeps.*, accounts.*
    // FROM
    //     vaultKeeps
    //     JOIN vaults ON vaultKeeps.vaultId = vaults.id
    //     JOIN keeps ON vaultKeeps.keepId = keeps.id
    //     JOIN accounts ON vaultKeeps.creatorId = accounts.id
    // WHERE
    //     vaultKeeps.id = @vaultKeepId
    //     ;";

    //     VaultKeep vk = _db.Query<VaultKeep, Keep, Vault, Profile, VaultKeep>(sql, (vaultKeep, vault, keep, profile) =>
    //     {
    //       vaultKeep.VaultId = vault.Id;
    //       vaultKeep.KeepId = keep.Id;
    //       vaultKeep.CreatorId = profile.Id;
    //       return vaultKeep;
    //     }, vaultKeepId).FirstOrDefault();
    //     return vk;
  }

  public VaultKeep Udpate(VaultKeep data)
  {
    throw new NotImplementedException();
  }
}