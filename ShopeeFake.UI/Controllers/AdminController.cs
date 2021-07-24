using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFake.Domain.Entities;
using ShopeeFake.UI.Application.Command.AdminCommands;
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
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStoreQueries _storeQueries;
        public AdminController(
            IMediator mediator,
            IStoreQueries storeQueries)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
            _storeQueries = storeQueries ?? throw new ArgumentException(nameof(storeQueries));
        }
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("get-list-user")]
        public async Task<IActionResult> ListUser()
        {
            List<User> users = await _storeQueries.GetAllUser();
            if (users != null)
            {
                return Ok(users);
            }
            return NotFound(new Response<ResponsDefault>() { State = false, Message = ErrorCode.NotFound });
        }
        [HttpPost]
        [Route("chang-state-user")]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeStateUser(
            [FromBody] StatusUserCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            if (result.State)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost]
        [Route("chang-state-store")]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeStateStore(
            [FromBody] StatusStoreCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            if (result.State)
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
