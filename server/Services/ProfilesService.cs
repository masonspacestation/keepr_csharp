

namespace keepr_csharp.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository profilesRepository)
  {
    _repo = profilesRepository;
  }


  internal Profile GetProfileById(string profileId)
  {
    Profile profile = _repo.GetById(profileId);
    if (profile == null)
    {
      throw new Exception($"Invalid id: {profileId}");
    }
    return profile;
  }

}