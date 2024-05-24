using Microsoft.AspNetCore.Identity;

namespace keepr_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{

  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0Provider;

  public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider)
  {
    _vaultsService = vaultsService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault newVault = _vaultsService.CreateVault(vaultData);
      return Ok(newVault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public ActionResult<Vault> GetVaultById(int vaultId)
  {
    try
    {
      Vault vault = _vaultsService.GetVaultById(vaultId);
      return Ok(vault);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }


}