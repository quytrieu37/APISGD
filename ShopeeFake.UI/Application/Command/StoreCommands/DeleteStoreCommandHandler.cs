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
    public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand,Response<ResponsDefault>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        public DeleteStoreCommandHandler(IStoreRepository storeRepository,
            IUserRepository userRepository)
        {
            _storeRepository = storeRepository;
            _userRepository = userRepository;
        }
        public async Task<Response<ResponsDefault>> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            Store store = _storeRepository.Stores.FirstOrDefault(x => x.Id == request.StoreId);
            if (store == null)
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
            _storeRepository.RemoveStore(store);
            int update = await _storeRepository.unitOfWork.SaveAsync();
            if (update > 0)
            {
                return new Response<ResponsDefault>()
                {
                    Message = ErrorCode.Success,
                    State = true,
                    @object = new ResponsDefault()
                    {
                        Data = "delete store success"
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
