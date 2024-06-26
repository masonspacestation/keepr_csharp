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

  private Vault PopulateCreator(Vault vault, Profile creator)
  {
    vault.Creator = creator;
    return vault;
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


  public void Destroy(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE vaults.id = @vaultId LIMIT 1;";
    _db.Execute(sql, new { vaultId });
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

  public Vault Udpate(Vault vaultData)
  {
    string sql = @"
    UPDATE vaults
    SET
    name = @Name,
    isPrivate = @IsPrivate
    WHERE id = @Id
    LIMIT 1;

    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.id = @Id;";

    Vault vault = _db.Query<Vault, Profile, Vault>(sql, PopulateCreator, vaultData).FirstOrDefault();
    return vault;
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults

    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.creatorId = @userId
    ;";

    List<Vault> myVaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      vault.CreatorId = profile.Id;
      return vault;
    }, new { userId }).ToList();
    return myVaults;
  }

  internal List<Vault> GetProfileVaults(string profileId)
  {
    string sql = @"
        SELECT
        vaults.*,
        accounts.*

        FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id

        WHERE vaults.creatorId = @profileId
        ;";

    List<Vault> profileVaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
  {
    vault.Creator = profile;
    return vault;
  }, new { profileId }).ToList();
    return profileVaults;

  }
}