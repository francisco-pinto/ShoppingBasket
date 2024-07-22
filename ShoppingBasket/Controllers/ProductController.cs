namespace ShoppingBasket.Controllers;

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using Application.Commands.ProductCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint to manage products
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("api/v1/shopping-basket/product")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class ProductController(IMediator mediator) : Controller
{
    /// <summary>
    /// Endpoint to create products
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateProductAsync([FromBody][Required] CreateProduct.CreateProductCommand command)
    {
        var item = await mediator.Send(command);
        return this.Ok(item);
    }
    
    /*TODO:
     * GetProducts
     * GetProduct
     */
    
}