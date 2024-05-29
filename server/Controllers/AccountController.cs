namespace keepr_csharp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultsService vaultsService)
  {
    _accountService = accountService;
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("vaults")]
  public async Task<ActionResult<List<Vault>>> GetMyVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> myVaults = _vaultsService.GetMyVaults(userInfo.Id);
      return Ok(myVaults);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }
  [Authorize]
  [HttpPut("{account}")]
  public async Task<ActionResult<Account>> UpdateAccount(string accountId, [FromBody] Account updateData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Account account = _accountService.Edit(updateData, userInfo.Email);
      return Ok(account);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}


