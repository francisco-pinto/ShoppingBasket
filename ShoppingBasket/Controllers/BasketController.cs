using Application.Shared;
using ShoppingBasket.Utils;

namespace ShoppingBasket.Controllers;

using Application.Queries.BasketQueries;
using Application.Commands.BasketCommands;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Endpoints to manage basket
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("api/v1/shopping-basket/basket")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class BasketController(IMediator mediator) : Controller
{
    /// <summary>
    /// Endpoint to buy a basket
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("buy")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> BuyAsync([FromBody][Required] Buy.BuyCommand command)
    {
        var response = await mediator.Send(command);

        return response.Result is ErrorResponse ? ErrorHandler.Handle(response.Result) : this.Ok(response.Result);
    }
    
    /// <summary>
    /// Add item to basket
    /// </summary>
    /// <param name="basketId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("{basketId:int}/add-item")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddItemToBasketAsync(
        [FromRoute][Required] int basketId,
        [FromBody][Required] AddItemToBasket.AddItemToBasketCommand command)
    {
        command.BasketId = basketId;
        
        var response = await mediator.Send(command);

        return response.Result is ErrorResponse ? ErrorHandler.Handle(response.Result) : this.Ok(response.Result);
    }
    
    /// <summary>
    /// Create a new basket
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateBasketAsync()
    {
        var basket = await mediator.Send(new CreateBasket.CreateBasketCommand());

        //return this.Created();
        return this.Ok(basket);
    }
    
    /// <summary>
    /// Get all baskets 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBasketsAsync()
    {
        var basket = await mediator.Send(new GetBaskets.GetBasketsQuery());

        return this.Ok(basket);
    }
    
    /// <summary>
    /// Get basket by id
    /// </summary>
    /// <param name="basketId"></param>
    /// <returns></returns>
    [HttpGet("{basketId:int}")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBasketByIdAsync([FromRoute][Required] int basketId)
    {
        var response = await mediator.Send(new GetBasketById.GetBasketByIdQuery(basketId));

        return response.Result is ErrorResponse ? ErrorHandler.Handle(response.Result) : this.Ok(response.Result);
    }
    
    /*TODO:
     * Remove Item Basket
     * Delete Basket
     */
}