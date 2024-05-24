namespace keepr_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{

  private readonly VaultsService _vaultsService;

  public VaultsController(VaultsService vaultsService)
  {
    _vaultsService = vaultsService;
  }




}