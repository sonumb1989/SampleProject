using AP.Web.API.Action;
using AP.Web.API.Common;
using AP.Web.Common.Messages;
using AP.Web.Persistence.Data;
using AP.Web.Persistence.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AP.Web.API.Controllers
{
  //[Authorize(Policy = "ApiUser")]
  [Route("api/[controller]/[action]")]
  public class DashboardController : BaseApiController
  {
    private readonly ClaimsPrincipal _caller;
    private readonly ApplicationDbContext _appDbContext;

    public DashboardController(UserManager<AppUser> userManager, ApplicationDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
      _caller = httpContextAccessor.HttpContext.User;
      _appDbContext = appDbContext;
    }

    // GET api/dashboard/home
    //[HttpPost]
    //public async Task<IActionResult> Home([FromBody]GetListProductAction action)
    //{
    //  var result = await action.ExecuteAction();
    //  return result.ReturnData;
    //}

    [HttpPost]
    public HttpActionResult Product([FromBody]GetListProductAction action)
    {
      var result = action.ExecuteAction(DataContext);
      return new HttpActionResult(result.Result);
    }

    [HttpPost]
    public HttpActionResult InsertProduct([FromBody] InsertProductAction action)
    {
      var result = action.ExecuteAction(DataContext);
      return new HttpActionResult(result.Result);
    }

    //// POST api/accounts
    //[HttpPost]
    ////RegistrationViewModel model, [FromServices]ICustomerService _service
    //public async Task<IActionResult> Post([FromBody]CustomerAction action)
    //{
    //  var result = await action.ExecuteAction(DataContext);
    //  return result;

    //  //if (!ModelState.IsValid)
    //  //{
    //  //  return BadRequest(ModelState);
    //  //}

    //  //var userIdentity = _mapper.Map<AppUser>(model);

    //  //var result = await _userManager.CreateAsync(userIdentity, model.Password);

    //  //if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

    //  //await Task.FromResult(_service.InsertCustomer(new Customer { IdentityId = userIdentity.Id, Location = model.Location }));

    //  //return new OkObjectResult("Account created");
    //}
  }
}
