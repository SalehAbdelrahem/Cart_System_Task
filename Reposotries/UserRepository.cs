﻿using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels.Userr;

namespace Reposotries
{
    public class UserRepository : IUserRepository
    {
        UserManager<User> UserManager;
        RoleManager<Role> roleManager;
        Project_Context Context;
      
        public IConfiguration Configuration { get; }
        public UserRepository(UserManager<User> userManager,
            IConfiguration configuration, RoleManager<Role> _roleManager,
            Project_Context context)
        {
            UserManager = userManager;
            Configuration = configuration;
            roleManager = _roleManager;
            Context = context;
           
        }


        public async Task<AuthModel> SignUp(SignUpModel signUpModel)
        {

            if (await UserManager.FindByEmailAsync(signUpModel.Email) is not null)
                return new AuthModel { Message = "Email is already registered" };


            if (await UserManager.FindByNameAsync(signUpModel.Full_Name) is not null)
                return new AuthModel { Message = "UserName is already registered Please Select another one" };

            User Temp = signUpModel.ToModel();
            var result = await UserManager.CreateAsync(Temp, signUpModel.Password);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var err in result.Errors)
                {
                    errors += $"{err.Description} , ";
                }
                return new AuthModel
                {
                    Message = errors
                };
            }

            await UserManager.AddToRoleAsync(Temp, "User");


            var userClaims = await UserManager.GetClaimsAsync(Temp);
            var roles = await UserManager.GetRolesAsync(Temp);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var SigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            var Claims = new List<Claim>()
            {

                new Claim(ClaimTypes.Name,signUpModel.Full_Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email,signUpModel.Email),
                new Claim("uid",$"{Temp.Id}")
            }
            .Union(userClaims)
            .Union(roleClaims);



            var Token = new JwtSecurityToken
                (
                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(14),
                    signingCredentials: new SigningCredentials(SigninKey, SecurityAlgorithms.HmacSha256Signature),
                    claims: Claims
                );


            return new AuthModel
            {
                Email = Temp.Email,
                ExpiresOn = Token.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(Token),
                Username = Temp.UserName,
                User_ID = Temp.Id

            };
        }
        public async Task<AuthModel> Login(LoginModel model)
        {
            var authModel = new AuthModel();

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user is null || !(await UserManager.CheckPasswordAsync(user, model.Password)))
            {
                authModel.Message = "Email or password is inncorrect";
                authModel.IsAuthenticated = false;
                return authModel;
            }

            var userClaims = await UserManager.GetClaimsAsync(user);
            var roles = await UserManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var SigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid",$"{user.Id}")
            }
            .Union(userClaims)
            .Union(roleClaims);



            var Token = new JwtSecurityToken
                (
                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(14),
                    signingCredentials: new SigningCredentials(SigninKey, SecurityAlgorithms.HmacSha256Signature),
                    claims: Claims
                );
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = Token.ValidTo;
            authModel.Roles = roles.ToList();
            authModel.User_ID = user.Id;
            return authModel;

        }

    }


}
