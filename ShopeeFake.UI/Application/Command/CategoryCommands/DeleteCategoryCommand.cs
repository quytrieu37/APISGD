using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.CategoryCommands
{
    public class DeleteCategoryCommand : IRequest<Response<ResponsDefault>>
    {
        public int CategoryId { get; set; }
    }
}
