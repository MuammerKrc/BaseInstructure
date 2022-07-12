using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using CoreLayer.IService;
using CoreLayer.IUnitOfWorks;
using CoreLayer.Models.IdentityModels;
using CoreLayer.Models.JwtModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLayer.Configurations;
using SharedLayer.Dtos;

namespace ServiceLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenCreateService _tokenCreateService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<ClientConfig> _clientConfig;

        public AuthenticationService(UserManager<AppUser> userManager, TokenCreateService tokenCreateService, IUnitOfWork unitOfWork, IOptions<List<ClientConfig>> clientConfig)
        {
            _userManager = userManager;
            _tokenCreateService = tokenCreateService;
            _unitOfWork = unitOfWork;
            _clientConfig = clientConfig.Value;
        }

        public async Task<Response<UserTokenDto>> UserAuthentication(UserLoginDto userLogin)
        {
            var errorResponse = Response<UserTokenDto>.ErrorResponse("user and email not matched");
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            if (user == null)
                return errorResponse;
            var passwordResult = await _userManager.CheckPasswordAsync(user, userLogin.Password);
            if (!passwordResult)
                return errorResponse;
            var token = await _tokenCreateService.CreateTokenForUser(user);
            var result = await _unitOfWork.UserRefreshTokenRepository.WhereQueryable(i => i.UserId == user.Id).FirstOrDefaultAsync();
            if (result != null)
            {
                result.RefreshToken = token.RefreshToken;
                result.Expiration = token.RefreshTokenExpiration;
            }
            else
            {
                _unitOfWork.UserRefreshTokenRepository.Add(new UserRefreshToken()
                {
                    UserId = user.Id,
                    RefreshToken = token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                });
            }
            await _unitOfWork.SaveChangeAsync();
            return Response<UserTokenDto>.SuccessResponse(token);
        }

        public async Task<Response<ClientTokenDto>> ClientAuthentication(ClientLoginDto clientLogin)
        {
            var errorResponse = Response<ClientTokenDto>.ErrorResponse("Client and password not matched");

            var client = _clientConfig.FirstOrDefault(i => i.ClientId == clientLogin.ClientId);
            if (client == null)
            {
                return errorResponse;
            }

            if (clientLogin.ClientSecret != client.ClientSecret)
            {
                return errorResponse;
            }

            var token = await _tokenCreateService.CreateTokenForClient(client);

            return Response<ClientTokenDto>.SuccessResponse(token);
        }

        public async Task<Response<UserTokenDto>> RefreshTokenAuthentication(UserRefreshToken refreshToken)
        {

            var response = await
                _unitOfWork.UserRefreshTokenRepository.WhereQueryable(i => i.RefreshToken == refreshToken.RefreshToken)
                    .FirstOrDefaultAsync();
            if (response == null)
                throw new Exception("Not Found");
            if (response.Expiration < refreshToken.Expiration)
            {
                await _unitOfWork.UserRefreshTokenRepository.Remove(response.Id);
            }

            var user = await _userManager.FindByIdAsync(response.UserId.ToString());
            if (user == null)
                throw new Exception("Not Found");

            var token=await _tokenCreateService.CreateTokenForUser(user);
            response.RefreshToken = token.RefreshToken;
            response.Expiration = token.RefreshTokenExpiration;
            await _unitOfWork.SaveChangeAsync();
            return Response<UserTokenDto>.SuccessResponse(token);
        }
    }
}
