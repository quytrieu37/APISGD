using MediatR;
using ShopeeFake.Domain.Entities;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.StoreCommands
{
    public class AddStoreCommandHandler : IRequestHandler<AddStoreCommand,Response<ResponsDefault>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        public AddStoreCommandHandler(IStoreRepository storeRepository,
            IUserRepository userRepository)
        {
            _storeRepository = storeRepository;
            _userRepository = userRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(AddStoreCommand request, CancellationToken cancellationToken)
        {
            User user = _userRepository.Users.FirstOrDefault(x => x.Id == request.UserId);
            if(user==null)
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
            Store store = new Store()
            {
                StoreAvatar = request.StoreAvatar,
                CreateDate = DateTime.Now,
                StoreName = request.StoreName,
                StoreState = 1,
                UserId = request.UserId
            };
            _storeRepository.AddStore(store);
            int update = await _storeRepository.unitOfWork.SaveAsync();
            if (update > 0)
            {
                return new Response<ResponsDefault>()
                {
                    Message = ErrorCode.Success,
                    State = true,
                    @object = new ResponsDefault()
                    {
                        Data = "Create store success"
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
