﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using NetEvent.Server.Data;
using NetEvent.Shared;

namespace NetEvent.Server.Modules.Users.Endpoints.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
    {
        private readonly ApplicationDbContext _UserDbContext;
        private readonly ILogger<GetUsersHandler> _Logger;

        public GetUsersHandler(ApplicationDbContext userDbContext, ILogger<GetUsersHandler> logger)
        {
            _UserDbContext = userDbContext;
            _Logger = logger;
        }

        public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            var allUsers = await _UserDbContext.Users.ToListAsync(cancellationToken);
            var convertedUsers = allUsers.Select(DtoMapper.Mapper.ApplicaitonUserToUser);
            return new GetUsersResponse(convertedUsers);
        }
    }
}
