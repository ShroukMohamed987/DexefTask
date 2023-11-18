using DexefTask.BL.DTOS.Identity;
using DexefTask.BL.IReposatories;
using DexefTask.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexefTask.BL.Reposatories
{
    public class LoginServices : ILoginServices
    {
        private readonly UserManager<DexefUser> _userManager;
        private readonly IConfiguration _config;
        public LoginServices(UserManager<DexefUser> userManager,IConfiguration config)
        {
            _userManager=userManager;
            _config = config;
            
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var DexefUser = await _userManager.FindByNameAsync(loginDto.UserName);
            
            if (DexefUser == null)
            {
                throw new Exception("No user found By This username");
            }
            else
            {
                var CheckedPassword = await _userManager.CheckPasswordAsync(DexefUser, loginDto.Password);
                if (!CheckedPassword)
                {
                    throw new Exception("your password doesn't match");
                    
                }

                var loginClaims = await _userManager.GetClaimsAsync(DexefUser);

                var key = _config.GetValue<string>("secretKey");
                //conver key to byte
                var keyAsByte = Encoding.ASCII.GetBytes(key);
                //convert key byte to object
                var keyObject = new SymmetricSecurityKey(keyAsByte);


                var signingCreditional = new SigningCredentials(keyObject, SecurityAlgorithms.Aes128CbcHmacSha256);


                var expiring = DateTime.Now;
                var token = new JwtSecurityToken
                    (
                    signingCredentials: signingCreditional,
                    claims: loginClaims,
                    expires: expiring
                    );


                var tokenHandler = new JwtSecurityTokenHandler();
                var returningToken = tokenHandler.WriteToken(token);

                return new TokenDto(returningToken, expiring);

            }
        }
    }
}
