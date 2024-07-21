using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Application;
using ShoppingBasket.Domain.Common;

namespace ShoppingBasket.Controllers;

public abstract class ApiController : ControllerBase
    {
        protected new IActionResult Ok<T>(Result<T> result)
        {
            return result.Match(
                success => StatusCode(200, new
                {
                    success = true,
                    data = success
                }),
                errors =>
                {
                    var firstError = errors.Errors.First();
                    return firstError.TypeError switch
                    {
                        ErrorTypes.NotFound => NotFound(new
                        {
                            success = false, errors = errors.Errors.Select(n => new {code=n.Key, message=n.Value})
                        }),
                        ErrorTypes.Forbidden => StatusCode(403,
                            new { success = false, errors = errors.Errors.Select(n =>  new {code=n.Key, message=n.Value}) }),
                        _ => BadRequest(new
                        {
                            success = false, errors = errors.Errors.Select(n =>  new {code=n.Key, message=n.Value})
                        })
                    };
                });

        }
        
        protected new IActionResult Created<T>(Result<T> result)
        {
            return result.Match(
                success => StatusCode(201, new
                {
                    success = true,
                    data = success
                }),
                errors =>
                {
                    var firstError = errors.Errors.First();
                    return firstError.TypeError switch
                    {
                        ErrorTypes.NotFound => NotFound(new
                        {
                            success = false, errors = errors.Errors.Select(n => new {code=n.Key, message=n.Value})
                        }),
                        ErrorTypes.Forbidden => StatusCode(403,
                            new { success = false, errors = errors.Errors.Select(n =>new {code=n.Key, message=n.Value}) }),
                        _ => BadRequest(new
                        {
                            success = false, errors = errors.Errors.Select(n => new {code=n.Key, message=n.Value})
                        })
                    };
                });
        }

        protected new IActionResult NoContent<T>(Result<T> result)
        {
            return result.Match(
                success => StatusCode(204, new
                {
                    success = true,
                    data = success
                }),
                errors =>
                {
                    var firstError = errors.Errors.First();
                    return firstError.TypeError switch
                    {
                        ErrorTypes.NotFound => NotFound(new
                        {
                            success = false,
                            errors = errors.Errors.Select(n => new { code = n.Key, message = n.Value })
                        }),
                        ErrorTypes.Forbidden => StatusCode(403,
                            new { success = false, errors = errors.Errors.Select(n => new { code = n.Key, message = n.Value }) }),
                        _ => BadRequest(new
                        {
                            success = false,
                            errors = errors.Errors.Select(n => new { code = n.Key, message = n.Value })
                        })
                    };
                });

        }
}