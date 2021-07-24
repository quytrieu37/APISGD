using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class RegisterUserCommancHandler : IRequestHandler<RegisterUserCommand,Response<ResponsDefault>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityServices _identityService;
        private readonly IStoreQueries _storeQueries;

        public RegisterUserCommancHandler(
            IUserRepository userRepository,
            IIdentityServices identityService,
            IStoreQueries storeQueries)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _identityService = identityService ?? throw new ArgumentException(nameof(identityService));
            _storeQueries = storeQueries ?? throw new ArgumentException(nameof(storeQueries));
        }

        public async Task<Response<ResponsDefault>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var checkExistUserOrEmail = await _userRepository.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName || x.Email == request.Email);

            if (checkExistUserOrEmail != null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.ExistUserOrEmail
                };
            }

            var user = new User()
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = _identityService.GetMD5(request.Password),
                LockoutEnabled = false,
                DateOfBirth = request.DateOfBirth
            };

            _userRepository.AddUser(user);

            await _userRepository.unitOfWork.SaveAsync(cancellationToken);

            UserRole userRole = new UserRole()
            {
                UserId = user.Id,
                RoleId = 2
            };

            _userRepository.AddUserRole(userRole);

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
