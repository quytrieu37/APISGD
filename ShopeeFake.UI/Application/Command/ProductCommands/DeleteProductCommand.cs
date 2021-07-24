using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.ProductCommands
{
    public class DeleteProductCommand : IRequest<Response<ResponsDefault>>
    {
        public int ProductId { get; set; }
    }
}
