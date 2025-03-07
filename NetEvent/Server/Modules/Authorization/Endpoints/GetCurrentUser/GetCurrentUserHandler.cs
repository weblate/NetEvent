﻿using MediatR;
using NetEvent.Shared;

namespace NetEvent.Server.Modules.Authorization.Endpoints.GetCurrentUser
{
    public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserRequest, GetCurrentUserResponse>
    {
        private readonly ILogger<GetCurrentUserHandler> _Logger;

        public GetCurrentUserHandler(ILogger<GetCurrentUserHandler> logger)
        {
            _Logger = logger;
        }

        public async Task<GetCurrentUserResponse> Handle(GetCurrentUserRequest request, CancellationToken cancellationToken)
        {
            if (request.User == null)
            {
                var errorMessage = "User not found.";
                _Logger.LogError(errorMessage);
                return new GetCurrentUserResponse(ReturnType.Error, errorMessage);
            }

            var currentUser = DtoMapper.Mapper.ClaimsPrincipalToCurrentUser(request.User);
            currentUser.Claims = request.User.Claims.ToDictionary(c => c.Type, c => c.Value);
            return new GetCurrentUserResponse(currentUser);
        }
    }
}
