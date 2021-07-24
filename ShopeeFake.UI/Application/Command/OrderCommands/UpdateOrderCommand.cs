using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.OrderCommands
{
    public class UpdateOrderCommand : IRequest<Response<ResponsDefault>>
    {
        public int OrderId { get; set; }
        public int OrderState { get; set; }
    }
}
