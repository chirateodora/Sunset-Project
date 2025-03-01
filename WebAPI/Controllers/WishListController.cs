﻿using Application.Commands.WishList;
using Application.Queries.Wishlist;
using Application.Queries.WishList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishListController : Controller
    {
        private readonly IMediator _mediator;

        public WishListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWishListController([FromBody] AddWishListCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateWishListController([FromBody] UpdateWishListCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpDelete("delete/{wishlistId:guid}")]
        public async Task<IActionResult> DeleteWishListController(Guid wishlistId)
        {
            var command = new DeleteWishListCommand(wishlistId);
            var result = await _mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllWishListsController()
        {
            var command = new GetAllWishListsQuery();
            var result = await _mediator.Send(command);
            return result.Count > 0 ? Ok(result) : BadRequest(result);
        }
        [HttpGet("get-wishlist-user/{userId:guid}")]
        public async Task<IActionResult> GetUserWishlist(Guid userId)
        {
            var command = new GetWishlistByUserIdQuery(userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
