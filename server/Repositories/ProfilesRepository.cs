namespace keepr_csharp.Repositories;
using keepr_csharp.Interfaces;


public class ProfilesRepository : IRepository<Profile>
{
  private readonly IDbConnection _db;

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }

  public Profile Create(Profile data)
  {
    throw new NotImplementedException();
  }

  public void Destroy(int id)
  {
    throw new NotImplementedException();
  }

  public List<Profile> GetAll()
  {
    throw new NotImplementedException();
  }

  public Profile GetById(string profileId)
  {
    string sql = @"
    SELECT *
FROM profiles
    WHERE profiles.creatorId = @profileId
    ;";

    Profile profile = _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
    return profile;
    // SELECT
    //    profiles.*,
    // accounts.* 
    // FROM profiles 

    // JOIN accounts ON profile.id = accounts.id
    // WHERE accounts.id = @profileId
    // ;";

    // Profile profile = _db.Query<Profile, Account, Profile>(sql, (profile, account) =>
    // {
    //   return profile;
    // }, new { profileId }).FirstOrDefault();
    // return profile;
  }

  public Profile GetById(int id)
  {
    throw new NotImplementedException();
  }

  public Profile Udpate(Profile data)
  {
    throw new NotImplementedException();
  }
}