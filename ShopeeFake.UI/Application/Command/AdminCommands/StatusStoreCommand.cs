using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.AdminCommands
{
    public class StatusStoreCommand : IRequest<Response<ResponsDefault>>
    {
        public int StoreId { get; set; }
    }
}
