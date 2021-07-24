using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.StoreCommands
{
    public class DeleteStoreCommand : IRequest<Response<ResponsDefault>>
    {
        public int StoreId { get; set; }
    }
}
