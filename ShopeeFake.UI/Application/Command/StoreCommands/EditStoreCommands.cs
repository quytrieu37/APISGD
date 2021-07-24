using MediatR;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.StoreCommands
{
    public class EditStoreCommands : IRequest<Response<ResponsDefault>>
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public int UserId { get; set; }
        public string StoreAvatar { get; set; }
        public int StoreState { get; set; }
    }
}
