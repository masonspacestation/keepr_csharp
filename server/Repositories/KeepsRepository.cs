using keepr_csharp.Interfaces;

namespace keepr_csharp.Repositories;

public class KeepsRepository : IRepository<Keep>
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  private Keep PopulateCreator(Keep keep, Profile profile)
  {
    keep.Creator = profile;
    return keep;
  }

  public Keep Create(Keep keepData)
  {
    string sql = @"
    INSERT INTO keeps(name, description, img, creatorId)
    VALUES(@Name, @Description, @Img, @CreatorId);

    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.id = LAST_INSERT_ID()
    ;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, keepData).FirstOrDefault();
    return keep;
  }

  public void Destroy(int id)
  {
    throw new NotImplementedException();
  }

  public List<Keep> GetAll()
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
     FROM keeps
     
     JOIN accounts ON accounts.id = keeps.creatorId
     ;";

    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator).ToList();
    return keeps;
  }

  public Keep GetById(int keepId)
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps

    JOIN accounts ON keeps.creatorId = accounts.id
    WHERE keeps.id = @keepId
    ;";

    Keep keep = _db.Query<Keep, Profile, Keep>(sql, PopulateCreator, new { keepId }).FirstOrDefault();
    return keep;
  }

  public Keep Udpate(Keep data)
  {
    throw new NotImplementedException();
  }

  internal List<Keep> GetKeepsByVaultId(int vaultId, string userId)
  {
    //     string sql = @"
    //        SELECT keeps.*, vaults.*, vaultKeeps.*, accounts.*
    // FROM
    //     keeps
    //     JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    //     JOIN vaults ON vaultKeeps.vaultId = vaults.id
    //     JOIN accounts ON vaultKeeps.creatorId = accounts.id
    // WHERE
    //     vaults.id = @vaultId;";

    string sql = @"
       SELECT keeps.*, vaults.*, vaultKeeps.*, accounts.*
FROM
    keeps
    JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    JOIN vaults ON vaultKeeps.vaultId = vaults.id
    JOIN accounts ON vaultKeeps.creatorId = accounts.id
WHERE
    vaults.id = @vaultId;";

    List<Keep> keeps = _db.Query<Keep, Vault, VaultKeep, Profile, Keep>(sql, (keep, vault, vk, profile) =>
    {
      keep.VaultKeepId = vk.Id;
      keep.Creator = profile;
      return keep;
    }, new { vaultId }).ToList();
    return keeps;
  }
}