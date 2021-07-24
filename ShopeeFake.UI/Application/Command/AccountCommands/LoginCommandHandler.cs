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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<ResponseToken>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityServices _identityService;
        private readonly IStoreQueries _storeQueries;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IIdentityServices identityService,
            IStoreQueries storeQueries)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _identityService = identityService ?? throw new ArgumentException(nameof(identityService));
            _storeQueries = storeQueries ?? throw new ArgumentException(nameof(storeQueries));
        }

        public async Task<Response<ResponseToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Users
                .FirstOrDefaultAsync
                (x => x.UserName == request.UserName || x.Email == request.UserName);

            if (user == null)
            {
                return new Response<ResponseToken>()
                {
                    State = false,
                    Message = ErrorCode.NotFound
                };
            }
            else if (user.LockoutEnabled == true)
            {
                return new Response<ResponseToken>()
                {
                    State = false,
                    Message = ErrorCode.LockoutUser
                };
            }

            if (_identityService.VerifyMD5Hash(user.Password,
                _identityService.GetMD5(request.Password)))
            {
                int timeOut = 60 * 60;

                List<Role> roles = await _storeQueries.GetRoleByUserId(user.Id);

                string token = _identityService.GenerateToken(
                    user,
                    roles.Select(x => x.RoleName).ToList(),
                    timeOut);

                DateTimeOffset dateTimeOffset = new DateTimeOffset(
                    DateTime.Now.AddSeconds(timeOut));
                return new Response<ResponseToken>()
                {
                    State = true,
                    Message = ErrorCode.Success,
                    @object = new ResponseToken()
                    {
                        AccessToken = token,
                        FullName = user.FullName,
                        Expires = dateTimeOffset.ToUnixTimeSeconds()
                    }
                };
            }
            else
            {
                return new Response<ResponseToken>()
                {
                    State = false,
                    Message = ErrorCode.BadRequest,
                };
            }
        }
    }
}
