using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Dtos;
using CoreLayer.IService;
using CoreLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharedLayer.Configurations;
using SharedLayer.Dtos;
using SharedLayer.HelperMethods;

namespace ServiceLayer.Services
{
    public class TokenCreateService:ITokenCreateService
    {
        private readonly TokenSettingConfig _tokenConfig;

        public TokenCreateService(TokenSettingConfig tokenConfig)
        {
            _tokenConfig = tokenConfig;
        }
        
        public async Task<UserTokenDto> CreateTokenForUser(AppUser user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenConfig.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenConfig.RefreshTokenExpiration);
            var refreshToken = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
            SecurityKey securityKey = HelperMethod.GetSymmetricSecurityKey(_tokenConfig.SecurityKey);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer:_tokenConfig.Issuer,
                expires:accessTokenExpiration,
                notBefore:DateTime.Now,
                claims:GetUserClaims(user,_tokenConfig.Audience),
                signingCredentials:new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature)
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token=handler.WriteToken(jwtSecurityToken);
            return new UserTokenDto()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };
        }

        public async Task<ClientTokenDto> CreateTokenForClient(ClientConfig clientConfig)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenConfig.AccessTokenExpiration);
            SecurityKey securityKey = HelperMethod.GetSymmetricSecurityKey(_tokenConfig.SecurityKey);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer:_tokenConfig.Issuer,
                expires:accessTokenExpiration,
                notBefore:DateTime.Now,
                claims: GetClientClaims(clientConfig),
                signingCredentials:new SigningCredentials(securityKey,algorithm:SecurityAlgorithms.HmacSha256Signature)
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var token=handler.WriteToken(jwtSecurityToken);
            return new ClientTokenDto() { AccessToken = token, Expiration = accessTokenExpiration };
        }
        private IEnumerable<Claim> GetClientClaims(ClientConfig client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audience.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, client.ClientId.ToString()));
            return claims;
        }

        private IEnumerable<Claim> GetUserClaims(AppUser user,List<string> audience)
        {
            var userClaimList = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            userClaimList.AddRange(audience.Select(i=>new Claim(JwtRegisteredClaimNames.Aud,i.ToString())));
            return userClaimList;
        }
    }
}
