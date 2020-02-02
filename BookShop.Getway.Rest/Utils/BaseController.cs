using BookShop.Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Getway.Rest.Utils
{
    public class BaseController : Controller
    {
        protected new IActionResult Ok()
            => base.Ok(Envelope.Ok());

        protected IActionResult Ok<T>(T result)
            => base.Ok(Envelope.Ok(result));

        protected IActionResult Error(Error errorMessage)
            => BadRequest(Envelope.Error(errorMessage));

        protected IActionResult FromResult<T>(Result<T> result)
            => result.IsSuccess ? Ok(result.Value) : Error(result.Error);

        protected IActionResult FromResult(Result result)
            => result.IsSuccess ? Ok() : Error(result.Error);
    }
}
