using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeFake.UI.Application.Command.AccountCommands;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }
        [HttpPost]
        [Route("register-user")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(
            [FromBody] RegisterUserCommand command)
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
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Response<ResponseToken>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command)
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
        [Route("change-password")]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordCommand command)
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
        [Route("update-info")]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ResponsDefault>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateInfo(
            [FromBody] UpdateInfoCommand command)
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
