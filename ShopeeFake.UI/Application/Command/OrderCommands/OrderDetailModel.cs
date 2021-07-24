using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.OrderCommands
{
    public class OrderDetailModel
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set;}
        public int StoreId { get; set; }
        public string OrderDetailNote { get; set; }
    }
}
