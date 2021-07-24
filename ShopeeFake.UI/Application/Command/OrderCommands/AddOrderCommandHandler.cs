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
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Response<ResponsDefault>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        public AddOrderCommandHandler(IOrderRepository orderRepository,
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            User user = _userRepository.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound,
                    @object = new ResponsDefault()
                    {
                        Data = "user identify is not Exist"
                    }
                };
            }
            Order order = new Order()
            {
                UserId = request.UserId,
                OrderCode = "111",
                OrderDate = DateTime.Now,
                OrderNote = request.OrderNote,
                OrderState = 1
            };
            _orderRepository.AddOrder(order);
            int i = await _orderRepository.unitOfWork.SaveAsync();
            if(i<0)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.BadRequest,
                    @object = new ResponsDefault()
                    {
                        Data = "Add Order Error before add orderDetail"
                    }
                };
            }
            foreach (OrderDetailModel detail in request.orderDetails)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Amount = detail.Amount,
                    OrderDetailNote = detail.OrderDetailNote,
                    Price = detail.Price,
                    ProductId = detail.ProductId,
                    StoreId = detail.StoreId,
                    OrderId = order.Id
                };
                _orderRepository.AddOrderDetail(orderDetail);
            }
            
            int update = await _orderRepository.unitOfWork.SaveAsync();
            if (update > 0)
            {
                return new Response<ResponsDefault>()
                {
                    Message = ErrorCode.Success,
                    State = true,
                    @object = new ResponsDefault()
                    {
                        Data = "Add order success"
                    }
                };
            }
            return new Response<ResponsDefault>()
            {
                State = false,
                Message = ErrorCode.ExcuteDB,
                @object = new ResponsDefault()
                {
                    Data = "excute database error"
                }
            };
        }
    }
}
