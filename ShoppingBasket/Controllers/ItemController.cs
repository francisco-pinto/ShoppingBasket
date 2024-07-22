using Application.Shared;
using ShoppingBasket.Utils;

namespace ShoppingBasket.Controllers;

using System.ComponentModel.DataAnnotations;
using System.Net;
using Application.Commands.ItemCommands;
using MediatR;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoint to manage items
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("api/v1/shopping-basket/item")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class ItemController(IMediator mediator) : Controller
{
    /// <summary>
    /// Create a new item
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateItemAsync([FromBody][Required] CreateItem.CreateItemCommand command)
    {
        var response = await mediator.Send(command);
        return response.Result is ErrorResponse ? ErrorHandler.Handle(response.Result) : this.Ok(response.Result);
    }
    
    /// <summary>
    /// Add discount to specific item
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="addDiscountCommand"></param>
    /// <returns></returns>
    [HttpPost("{itemId:int}/add-discount")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddDiscountAsync(
        [FromRoute][Required] int itemId,
        [FromBody][Required] AddDiscount.AddDiscountCommand addDiscountCommand)
    {
        addDiscountCommand.ItemId = itemId;
        
        var response = await mediator.Send(addDiscountCommand);
        return response.Result is ErrorResponse ? ErrorHandler.Handle(response.Result) : this.Ok(response.Result);
    }
}