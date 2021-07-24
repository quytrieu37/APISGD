using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.CategoryCommands
{
    public class EditCategoryCommand : IRequest<Response<ResponsDefault>>
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
