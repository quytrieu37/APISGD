using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFake.Domain.Entities;
using ShopeeFake.UI.Application.Command.ProductCommands;
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
    public class ProductController : ControllerBase
    {
        private readonly IStoreQueries _storeQueries;
        private readonly IMediator _mediator;
        public ProductController(IStoreQueries storeQueries,
            IMediator mediator)
        {
            _storeQueries = storeQueries;
            _mediator = mediator;
        }
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("list-product")]
        public async Task<IActionResult> ListProduct()
        {
            List<Product> products = await _storeQueries.GetAllProducts();
            return Ok(products);
        }
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("list-product-summary")]
        public async Task<IActionResult> ListProductSummary()
        {
            var products = await _storeQueries.GetAllProductSummary();
            return Ok(products);
        }
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("get-product-by-product-id")]
        public async Task<IActionResult> GetProductById([FromQuery] int ProductId)
        {
            Product Product = await _storeQueries.GetProductById(ProductId);
            if (Product != null)
            {
                return Ok(Product);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("get-product-by-store-id")]
        public async Task<IActionResult> GetProductByStoreId([FromQuery] int StoreId)
        {
            List<Product> Product = await _storeQueries.GetProductByStoreId(StoreId);
            if (Product != null)
            {
                return Ok(Product);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("get-all-product-purchased")]
        public async Task<IActionResult> GetProductPurchased([FromQuery] int UserId)
        {
            List<dynamic> Product = await _storeQueries.GetProductPurchased(UserId);
            if (Product != null)
            {
                return Ok(Product);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status409Conflict)]
        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddProduct(AddProductCommand command)
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
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status409Conflict)]
        [HttpPut]
        [Route("edit-Product")]
        public async Task<IActionResult> EditProduct(EditProductCommand command)
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
        [HttpDelete]
        [Route("delete-product")]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductCommand command)
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
        [HttpPost]
        [Route("change-status-product")]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeStatusProduct(ChangeStatusProductCommand command)
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
