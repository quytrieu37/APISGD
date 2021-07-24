using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFake.Domain.Entities;
using ShopeeFake.UI.Application.Command.OrderCommands;
using ShopeeFake.UI.Infrastructure;
using ShopeeFake.UI.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStoreQueries _storeQueries;
        public OrderController(
            IMediator mediator,
            IStoreQueries storeQueries)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _storeQueries = storeQueries ?? throw new ArgumentException(nameof(storeQueries));
        }
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("list-order")]
        public async Task<IActionResult> ListOrder([FromQuery]DateTime? start =null, [FromQuery]DateTime? end=null)
        {
            List<Order> Orders= await _storeQueries.GetAllOrder(start,end);
            if (Orders != null)
            {
                return Ok(Orders);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("get-list-order-byId")]
        public async Task<IActionResult> GetOrderById([FromQuery] int UserId)
        {
            List<Order> Orders = await _storeQueries.GetOrderByUserId(UserId);
            if (Orders != null)
            {
                return Ok(Orders);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(List<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("list-order-detail-base-order-id")]
        public async Task<IActionResult> ListOrderBaseUserId([FromQuery]int OrderId)
        {
            List<OrderDetail> OrderDetail = await _storeQueries.GetOrderDetailByOrderId(OrderId);
            if (OrderDetail != null)
            {
                return Ok(OrderDetail);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(List<dynamic>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("list-order-by-store-id")]
        public async Task<IActionResult> GetListOrderByStoreId([FromQuery] int StoreId)
        {
            List<dynamic> productOrders = await _storeQueries.GetOrderByStoreId(StoreId);
            if (productOrders != null)
            {
                return Ok(productOrders);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("add-order")]
        public async Task<IActionResult> AddOrder(AddOrderCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }
            Response<ResponsDefault> result = await _mediator.Send(command);
            if (result.State == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("update-order")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }
            Response<ResponsDefault> result = await _mediator.Send(command);
            if (result.State == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
