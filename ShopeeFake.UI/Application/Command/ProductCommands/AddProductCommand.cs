using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.ProductCommands
{
    public class AddProductCommand : IRequest<Response<ResponsDefault>>
    {
        public string ProductName { get; set; }
        public int StoreId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsHidden { get; set; }
    }
}
