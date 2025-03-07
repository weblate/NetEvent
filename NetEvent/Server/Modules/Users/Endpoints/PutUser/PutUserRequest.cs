﻿using MediatR;
using Microsoft.Toolkit.Diagnostics;
using NetEvent.Shared.Dto;

namespace NetEvent.Server.Modules.Users.Endpoints.PutUser
{
    public class PutUserRequest : IRequest<PutUserResponse>
    {
        public PutUserRequest(string id, User user)
        {
            Guard.IsNotNullOrEmpty(id, nameof(id));
            Guard.IsNotNull(user, nameof(user));

            Id = id;
            User = user;
        }

        public string Id { get; }
        public User User { get; }
    }
}
