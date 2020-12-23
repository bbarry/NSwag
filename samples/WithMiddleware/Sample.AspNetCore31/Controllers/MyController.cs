using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Sample.AspNetCore31.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))] // <- this causes the api convention to not apply
    public class MyController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [ApiConventionMethod(typeof(MyApiConventions), nameof(MyApiConventions.Post))]
        public async Task<ActionResult<MyDocument>> SendContactMessage([FromBody] SendContactMessageCommand request, CancellationToken token)
        {
            await Task.Yield();
            return Ok(new MyDocument());
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyDocument))]
        public async Task<ActionResult<MyDocument>> SendContactMessage2([FromBody] SendContactMessageCommand request, CancellationToken token)
        {
            await Task.Yield();
            return Ok(new MyDocument());
        }
    }

    public class SendContactMessageCommand
    {
    }
    public class MyDocument
    {
    }

    public static class MyApiConventions
    {
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyDocument))]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        public static void Post(
            [FromBody]
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Exact)]
            object request)
        {
        }
    }
}
