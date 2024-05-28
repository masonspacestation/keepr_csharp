namespace keepr_csharp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth0Provider;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider, KeepsService keepsService)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth0Provider = auth0Provider;
    _keepsService = keepsService;
  }


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vkData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vkData.CreatorId = userInfo.Id;
      VaultKeep newVk = _vaultKeepsService.CreateVaultKeep(vkData);
      _keepsService.IncrementKeeps(vkData.KeepId);
      return Ok(newVk);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpDelete("{vaultKeepId}")]
  public async Task<ActionResult<string>> DestroyVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      _keepsService.IncrementKeeps(vaultKeepId);
      return Ok("VaultKeep was deleted. Huzzah!");
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}