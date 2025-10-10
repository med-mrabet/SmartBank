using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartBank.Application.Identity;
using SmartBank.Shared.Dtos;
using SmartBank.Shared.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Identity.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _conf;
        public AuthService(UserManager<ApplicationUser> userManager,
            IConfiguration conf,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _conf = conf;
            _signInManager = signInManager;
        }

        public async Task<AuthResponse> LoginAsync(LoginDto credentials)
        {
            var user = new ApplicationUser();
            try
            {
             user = await _userManager.FindByEmailAsync(credentials.Email);

            }catch(Exception ex)
            {
                throw;
            }


            if (user == null)
            {
                throw new Exception($"User with email : {credentials.Email} not found");
                
            }

            var res = await _signInManager.CheckPasswordSignInAsync(user, credentials.Password,false);
            if (res.Succeeded == false) {
              throw new Exception($"credention for user {credentials.Email} is invalid");
            
            }

            JwtSecurityToken token = await GenerateToken(user);

            var response = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                UserName = user.UserName
            };

            return response;



            throw new NotImplementedException();
        }


        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);


            var roleClaims = roles.Select(p => new Claim(ClaimTypes.Role, p)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }.Union(roleClaims)
            .Union(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["JwtSettings:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
             issuer: _conf["JwtSettings:Issuer"],
             audience: _conf["JwtSettings:Audience"],
             claims: claims,
             expires: DateTime.Now.AddMinutes(15),
             signingCredentials: signingCredentials);
            return jwtSecurityToken;


        }
    }
}
