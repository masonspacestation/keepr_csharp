namespace keepr_csharp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth0Provider;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth0Provider = auth0Provider;
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
      _vaultKeepsService.DestroyVault(vaultKeepId, userInfo.Id);
      return Ok("VaultKeep was deleted. Huzzah!");
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}