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
    public class StatusUserCommandHandler : IRequestHandler<StatusUserCommand,Response<ResponsDefault>>
    {
        private readonly IUserRepository _userRepository;

        public StatusUserCommandHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
        }
        public async Task<Response<ResponsDefault>> Handle(StatusUserCommand request, CancellationToken cancellationToken)
        {
            User user = _userRepository.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                return new Response<ResponsDefault>()
                {
                    State = false,
                    Message = ErrorCode.NotFound
                };
            }
            user.LockoutEnabled = !user.LockoutEnabled;
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
                        Data = user.Id.ToString() + "Look: "+ user.LockoutEnabled.ToString()
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
