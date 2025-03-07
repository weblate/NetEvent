﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using NetEvent.Server.Models;

namespace NetEvent.Server.Modules.Authorization.Endpoints.PostLoginUser
{
    public class PostLoginUserHandler : IRequestHandler<PostLoginUserRequest, PostLoginUserResponse>
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly ILogger<PostLoginUserHandler> _Logger;

        public PostLoginUserHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<PostLoginUserHandler> logger)
        {
            _UserManager = userManager;
            _SignInManager = signInManager; ;
            _Logger = logger;
        }

        public async Task<PostLoginUserResponse> Handle(PostLoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = (await _UserManager.FindByNameAsync(request.LoginRequest.UserName).ConfigureAwait(false)) ??
                        await _UserManager.FindByEmailAsync(request.LoginRequest.UserName).ConfigureAwait(false);

            if (user == null)
            {
                var errorMessage = "User does not exist.";
                _Logger.LogError(errorMessage);
                return new PostLoginUserResponse(ReturnType.Error, errorMessage);
            }

            var singInResult = await _SignInManager.CheckPasswordSignInAsync(user, request.LoginRequest.Password, false);

            if (!singInResult.Succeeded)
            {
                var errorMessage = "Invalid password.";
                _Logger.LogError(errorMessage);
                return new PostLoginUserResponse(ReturnType.Error, errorMessage);
            }

            await _SignInManager.SignInAsync(user, request.LoginRequest.RememberMe).ConfigureAwait(false);
            return new PostLoginUserResponse();
        }
    }
}
