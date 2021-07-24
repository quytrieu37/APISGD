using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.AdminCommands
{
    public class StatusUserCommand : IRequest<Response<ResponsDefault>>
    {
        public int UserId { get; set; }
    }
}
