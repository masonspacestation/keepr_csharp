namespace keepr_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0Provider;
  private readonly KeepsService _keepsService;


  public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider, VaultsService vaultsService, KeepsService keepsService)
  {
    _profilesService = profilesService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
  }

  [HttpGet("{profileId}")]
  public async Task<ActionResult<Profile>> GetProfileById(string profileId)
  {
    try
    {
      // Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Profile profile = _profilesService.GetProfileById(profileId);
      return Ok(profile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


  // NOTE this will check the user's identity so that it doesn't send back private vaults in the network request
  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetProfileVaults(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultsService.GetProfileVaults(profileId, userInfo?.Id);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public async Task<ActionResult<List<Keep>>> GetProfileKeeps(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _keepsService.GetProfileKeeps(profileId, userInfo?.Id);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


}