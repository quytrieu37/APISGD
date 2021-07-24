using MediatR;
using ShopeeFake.Domain.Entities;
using ShopeeFake.Domain.Repositories;
using ShopeeFake.UI.Infrastructure;
using ShopeeFake.UI.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopeeFake.UI.Application.Command.AccountCommands
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response<ResponsDefault>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityServices _identityService;
        private readonly IStoreQueries _storeQueries;

        public ChangePasswordCommandHandler(
            IUserRepository userRepository,
            IIdentityServices identityService,
            IStoreQueries storeQueries)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _identityService = identityService ?? throw new ArgumentException(nameof(identityService));
            _storeQueries = storeQueries ?? throw new ArgumentException(nameof(storeQueries));
        }
        public async Task<Response<ResponsDefault>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            User user = _userRepository.Users.FirstOrDefault(x => x.Id == request.UserId);
            if(user== null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound
                };
            }
            if (!_identityService.VerifyMD5Hash(request.OldPassword, user.Password))
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.InvalidVerifyPassword
                };
            }
            user.Password = _identityService.GetMD5(request.OldPassword);
            _userRepository.EditUser(user);
            int result = await _userRepository.unitOfWork.SaveAsync(cancellationToken);

            if (result > 0)
            {
                return new Response<ResponsDefault>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    @object = new ResponsDefault()
                    {
                        Data = user.Id.ToString()
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
