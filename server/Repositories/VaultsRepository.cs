using System.Reflection.Metadata.Ecma335;
using keepr_csharp.Interfaces;

namespace keepr_csharp.Repositories;

public class VaultsRepository : IRepository<Vault>
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Vault Create(Vault data)
  {
    string sql = @"
    INSERT INTO vaults(name, description, isPrivate, img, creatorId)
    VALUES(@Name, @Description, @IsPrivate, @Img, @CreatorId);

    SELECT
    vaults.*,
    accounts.*
    FROM vaults

  JOIN accounts ON accounts.id = vaults.creatorId
  WHERE vaults.id = LAST_INSERT_ID()
    ;";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.CreatorId = profile.Id;
      vault.Creator = profile;
      return vault;
    }, data).FirstOrDefault();
    return vault;
  }


  public void Destroy(int id)
  {
    throw new NotImplementedException();
  }

  public List<Vault> GetAll()
  {
    throw new NotImplementedException();
  }

  public Vault GetById(int vaultId)
  {
    string sql = @"
    SELECT 
    vaults.*,
    accounts.*
    FROM vaults
    
    JOIN accounts ON accounts.id = vaults.CreatorId
    WHERE vaults.id = @vaultId
    ;";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.CreatorId = profile.Id;
      vault.Creator = profile;
      return vault;
    }, new { vaultId }).FirstOrDefault();
    return vault;
  }

  public Vault Udpate(Vault data)
  {
    throw new NotImplementedException();
  }
}