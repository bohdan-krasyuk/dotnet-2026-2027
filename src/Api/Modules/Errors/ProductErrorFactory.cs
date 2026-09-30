using Application.Products.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Modules.Errors;

public static class ProductErrorFactory
{
    public static ObjectResult ToObjectResult(this ProductException error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = error switch
            {
                ProductAlreadyExistsException => StatusCodes.Status409Conflict,
                ProductNotFoundException => StatusCodes.Status404NotFound,
                ProductUnhandledException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Product error handler is not implemented.")
            }
        };
    }
}