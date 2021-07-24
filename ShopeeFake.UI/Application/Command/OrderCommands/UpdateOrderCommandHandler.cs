using MediatR;
using ShopeeFake.Domain.Entities;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.OrderCommands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<ResponsDefault>>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = _orderRepository.Orders.FirstOrDefault(x => x.Id == request.OrderId);
            if(order == null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                };
            }
            order.OrderState = request.OrderState;
            _orderRepository.EditOrder(order);
            int i = await _orderRepository.unitOfWork.SaveAsync();
            if (i > 0)
            {
                return new Response<ResponsDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    @object = new ResponsDefault()
                    {
                        Data = "update order success"
                    }
                };
            }
            return new Response<ResponsDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                @object = new ResponsDefault()
                {
                    Data = "Error Excute DB"
                }
            };
        }
    }
}
