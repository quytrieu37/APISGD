using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFake.Domain.Entities;
using ShopeeFake.UI.Application.Command.StoreCommands;
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
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStoreQueries _storeQueries;
        public StoreController(
            IMediator mediator,
            IStoreQueries storeQueries)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _storeQueries = storeQueries ?? throw new ArgumentException(nameof(storeQueries));
        }
        [ProducesResponseType(typeof(List<Store>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("list-store")]
        public async Task<IActionResult> ListStore()
        {
            List<Store> stores = await _storeQueries.GetAllStore();
            return Ok(stores);
        }
        [ProducesResponseType(typeof(Store), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("get-store-byId")]
        public async Task<IActionResult> GetStoreById([FromQuery] int StoreId)
        {
            Store stores = await _storeQueries.GetStoreById(StoreId);
            if (stores != null)
            {
                return Ok(stores);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(List<Store>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("list-store-base-user-id")]
        public async Task<IActionResult> ListStoreBaseUserId(int UserId)
        {
            List<Store> stores = await _storeQueries.GetStoreByUserId(UserId);
            if (stores != null)
            {
                return Ok(stores);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        [HttpPost]
        [Route("create-new-store")]
        public async Task<IActionResult> CreateStore(AddStoreCommand command)
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
        [HttpPut]
        [Route("edit-store")]
        public async Task<IActionResult> EditStore(EditStoreCommands command)
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
        [HttpDelete]
        [Route("delete-store")]
        public async Task<IActionResult> DeleteStore(DeleteStoreCommand command)
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
