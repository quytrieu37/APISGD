using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ShopeeFake.UI.Infrastructure;

namespace ShopeeFake.UI.Application.Command.CategoryCommands
{
    public class AddCategoryCommand : IRequest<Response<ResponsDefault>>
    {
        public string CategoryName { get; set; }
    }
}
