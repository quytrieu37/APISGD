using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.OrderCommands
{
    public class AddOrderCommand : IRequest<Response<ResponsDefault>>
    {
        public int UserId { get; set; }
        public string OrderNote { get; set; }
        public List<OrderDetailModel> orderDetails { get; set; }
    }
}
