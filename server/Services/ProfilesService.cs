

namespace keepr_csharp.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository profilesRepository)
  {
    _repo = profilesRepository;
  }


  // FIXME this won't work because I don't yet have a profiles table. I feel like the way to make that would be to do a join with the accounts table. Then like the rest of the stuff, I would just compare with profiles.creatorId.
  internal Profile GetProfileById(string profileId, string userId)
  {
    Profile profile = _repo.GetById(profileId);
    if (profile == null)
    {
      throw new Exception($"Invalid id: {profileId}");
    }
    return profile;
  }

}