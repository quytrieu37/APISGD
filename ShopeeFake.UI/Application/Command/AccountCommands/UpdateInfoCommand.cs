using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.AccountCommands
{
    public class UpdateInfoCommand : IRequest<Response<ResponsDefault>>
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
    }
}
