using Application.Shared;
using ShoppingBasket.Utils;

namespace ShoppingBasket.Controllers;

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using Application.Commands.DiscountCommands;
using Application.Queries.DiscountQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints to manage discounts
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("api/v1/shopping-basket/discount")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class DiscountController(IMediator mediator) : Controller
{
    /// <summary>
    /// Endpoint to create a percentage discount
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("percentage-discount")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddPercentageDiscountAsync(
        [FromBody][Required] AddPercentageDiscount.AddPercentageDiscountCommand command)
    {
        var discount = await mediator.Send(command);
        return this.Ok(discount);
    }
    
    /// <summary>
    /// Endpoint to create a quantity discount
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("quantity-discount")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddQuantityDiscountAsync(
        [FromBody][Required] AddQuantityDiscount.AddQuantityDiscountCommand command)
    {
        var discount = await mediator.Send(command);
        return this.Ok(discount);
    }
    
    /// <summary>
    /// Get all the discounts
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetDiscountsAsync()
    {
        var discounts = await mediator.Send(new GetDiscounts.GetDiscountsQuery());
        return this.Ok(discounts);
    }
    
    /// <summary>
    /// Get discount by id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{discountId:int}")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetDiscountByIdAsync(int discountId)
    {
        var response = await mediator.Send(new GetDiscountById.GetDiscountByIdQuery(discountId));
        return response.Result is ErrorResponse ? ErrorHandler.Handle(response.Result) : this.Ok(response.Result);
    }
}