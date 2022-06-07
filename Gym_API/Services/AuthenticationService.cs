using System;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gym_API.Dto;
using Gym_API.Services.Interfaces;
using Gym_API.Models;
using Gym_API.Shared;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using Gym_API.Contexts;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Gym_API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public AuthenticationService()
        {
        }

        public AuthenticationService(
           UserManager<User> userManager,
           RoleManager<Role> roleManager,
           IConfiguration configuration,
           ApplicationDbContext db
           )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _db = db;
        }

        public async Task<Response> Register(RegisterDto model)
        {
            var userExist = await this._userManager.FindByNameAsync(model.Username);

            if (userExist != null)
            {
                throw new HttpRequestException("User already existed", null, HttpStatusCode.Forbidden);
            }

            User user = new User()
            {
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = BCryptNet.HashPassword(model.Password)
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            var role = await this._roleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                throw new HttpRequestException($"Role {model.RoleId} not found", null, HttpStatusCode.NotFound);
            }

            switch (role.Name)
            {
                case UserRoles.Coach:
                    var coach = new Coach
                    {
                        Fullname = model.Fullname,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        GenderId = model.GenderId,
                        PhoneNumber = model.PhoneNumber,
                        StatusId = model.StatusId,
                        SpecializationId = model.SpecializationId
                    };

                    user.Coach = coach;
                    await this._userManager.AddToRoleAsync(user, UserRoles.Coach);
                    break;
                case UserRoles.Supervisor:
                    var supervisor = new Supervisor
                    {
                        Fullname = model.Fullname,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        GenderId = model.GenderId,
                        PhoneNumber = model.PhoneNumber,
                        StatusId = model.StatusId,
                    };

                    user.Supervisor = supervisor;
                    await this._userManager.AddToRoleAsync(user, UserRoles.Supervisor);
                    break;
                default:
                    var customer = new Customer
                    {
                        Fullname = model.Fullname,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        GenderId = model.GenderId,
                        PhoneNumber = model.PhoneNumber,
                    };

                    user.Customer = customer;
                    await this._userManager.AddToRoleAsync(user, UserRoles.Customer);
                    break;
            }

            return new Response { Status = "Success", Message = "User Created Successfully" };
        }

        public async Task<Response> RegisterAdmin(AdminRegisterDto model)
        {
            var userExist = await this._userManager.FindByNameAsync(model.Username);

            if (userExist != null)
            {
                throw new HttpRequestException("User already existed", null, HttpStatusCode.Forbidden);
            }

            User user = new User()
            {
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await this._userManager.CreateAsync(user, model.Password);


            if (!result.Succeeded)
            {
                throw new HttpRequestException("User creation faild", null, HttpStatusCode.Forbidden);
            }

            if (!await this._roleManager.RoleExistsAsync(UserRoles.Admin))
                await this._roleManager.CreateAsync(new Role(UserRoles.Admin));

            if (await this._roleManager.RoleExistsAsync(UserRoles.Admin))
                await this._userManager.AddToRoleAsync(user, UserRoles.Admin);

            return new Response { Status = "Success", Message = "Admin Created Successfully" };
        }

        public async Task<ResLoginDto> Login(LoginDto model)
        {
            var user = await this._userManager.FindByNameAsync(model.Username);

            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Username or password is incorrect");
            }

            var userRoles = await this._userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                    issuer: this._configuration["JWT:ValidIssuer"],
                    audience: this._configuration["JWT:ValidAudience"],
                    claims: authClaims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                );

            return new ResLoginDto { token = new JwtSecurityTokenHandler().WriteToken(token) };
        }
    }
}

