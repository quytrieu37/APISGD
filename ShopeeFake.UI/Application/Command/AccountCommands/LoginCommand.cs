using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.AccountCommands
{
    public class LoginCommand : IRequest<Response<ResponseToken>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
