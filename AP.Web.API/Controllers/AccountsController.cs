using AP.Web.API.ViewModels;
using AP.Web.Common.Helpers;
using AP.Web.Persistence.Data.Entities;
using AP.Web.Persistence.UnitOfWork;
using AP.Web.Services.Data.Customers;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AP.Web.API.Controllers
{
  [Route("api/[controller]")]
  public class AccountsController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public AccountsController(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, ILogger<AccountsController> logger)
    {
      _userManager = userManager;
      _mapper = mapper;

    }

    // POST api/accounts
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]RegistrationViewModel model, [FromServices]ICustomerBusinessLogic _service)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userIdentity = _mapper.Map<AppUser>(model);

      var result = await _userManager.CreateAsync(userIdentity, model.Password);

      if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

      await Task.FromResult(_service.InsertCustomer(new Customer { IdentityId = userIdentity.Id, Location = model.Location }));

      return new OkObjectResult("Account created");
    }
  }
}
