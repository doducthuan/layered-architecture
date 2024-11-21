using LayeredArchitecture.Common.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace LayeredArchitecture.API.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        [NonAction]
        public ActionResult BaseResult(ApiResponse? data = null)
        {
            if (data == null)
            {
                return Ok();
            }

            switch (data.status)
            {
                case (int)HttpStatusCode.OK:
                    return Ok(data);
                case (int)HttpStatusCode.BadRequest:
                    return BadRequest(data);
                case (int)HttpStatusCode.UnprocessableEntity:
                    return UnprocessableEntity(data);
                case (int)HttpStatusCode.Unauthorized:
                    return Unauthorized(data);
                case (int)HttpStatusCode.Forbidden:
                    return StatusCode((int)HttpStatusCode.Forbidden, data);
                case (int)HttpStatusCode.NotFound:
                    return NotFound(data);
                default:
                    return StatusCode(data.status, data);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        Field = x.Key,
                        Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    })
                    .Reverse()
                    .ToList();
                var response = ApiResponse.Response(DefineResponse.EnumCodes.R_CMN_422_01, string.Join(',', errors));
                context.Result = BaseResult(response);
            }

            base.OnActionExecuting(context);
        }
    }
}
