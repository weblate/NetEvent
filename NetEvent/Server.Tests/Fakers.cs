﻿using System;
using Bogus;
using NetEvent.Server.Models;
using NetEvent.Shared.Dto;

namespace NetEvent.Server.Tests
{
    internal static class Fakers
    {
        public static Faker<ApplicationUser> ApplicationUserFaker() => new Faker<ApplicationUser>()
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
            .RuleFor(u => u.PasswordHash, (f, u) => f.Internet.Password())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
            .RuleFor(u => u.UserName, (f, u) => u.Email)
            .RuleFor(u => u.Id, (f, u) => Guid.NewGuid().ToString());

        public static Faker<User> UserFaker() => new Faker<User>()
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
            .RuleFor(u => u.UserName, (f, u) => u.Email)
            .RuleFor(u => u.ProfileImage, (f, u) => f.Random.Bytes(10))
            .RuleFor(u => u.Id, (f, u) => Guid.NewGuid().ToString());
    }
}
