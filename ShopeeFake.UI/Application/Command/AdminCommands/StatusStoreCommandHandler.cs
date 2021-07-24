using MediatR;
using ShopeeFake.Domain.Entities;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.AdminCommands
{
    public class StatusStoreCommandHandler : IRequestHandler<StatusStoreCommand, Response<ResponsDefault>>
    {
        private readonly IStoreRepository _storeRepository;
        public StatusStoreCommandHandler(
               IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository ?? throw new ArgumentException(nameof(storeRepository));
        }
        public async Task<Response<ResponsDefault>> Handle(StatusStoreCommand request, CancellationToken cancellationToken)
        {
            Store store = _storeRepository.Stores.FirstOrDefault(x => x.Id == request.StoreId);
            if (store == null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound
                };
            }
            if(store.StoreState ==0)
            {
                store.StoreState = 1;
            }
            else
            {
                store.StoreState = 0;
            }
            _storeRepository.EditStore(store);
            int result = await _storeRepository.unitOfWork.SaveAsync(cancellationToken);

            if (result > 0)
            {
                return new Response<ResponsDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    @object = new ResponsDefault()
                    {
                        Data = store.Id.ToString() + "status: " + store.StoreState.ToString()
                    }
                };
            }
            else
            {
                return new Response<ResponsDefault>()
                {
                    State = true,
                    Message = ErrorCode.ExcuteDB
                };
            }
        }
    }
}
