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
using System.Security.Cryptography;

namespace Gym_API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;

        public AuthenticationService(
           UserManager<User> userManager,
           RoleManager<Role> roleManager,
           IConfiguration configuration,
           ApplicationDbContext db,
           IUserService userService
           )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userService = userService;
            _db = db;
        }

        public async Task<Response> RegisterCustomer(RegisterCustomerDto model)
        {
            var userExist = await GetUserByUsernameAsync(model.Username);

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

            var customer = new Customer
            {
                Id = user.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                GenderId = model.GenderId,
                PhoneNumber = model.PhoneNumber,
            };

            user.Customer = customer;

            await this._userManager.AddToRoleAsync(user, UserRoles.Customer);

            return new Response { Status = "Success", Message = "Customer Created Successfully" };
        }

        public async Task<Response> RegisterStuff(RegisterStuffDto model)
        {
            var userExist = await GetUserByUsernameAsync(model.Username);

            if (userExist != null)
            {
                throw new HttpRequestException("User already existed", null, HttpStatusCode.Forbidden);
            }

            User user = new User()
            {
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = BCryptNet.HashPassword(model.Password),
                IsStuff = true,
                IsCustomer = false
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
                case UserRoles.SeniorCoach:
                case UserRoles.HeadCoach:

                    var specializations = _db.Specializations
                        .Where(specialization => model.SpecializationIds.Contains(specialization.Id))
                        .ToList();

                    var coach = new Coach
                    {
                        Id = user.Id,
                        Firstname = model.Firstname,
                        Lastname = model.Lastname,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        GenderId = model.GenderId,
                        PhoneNumber = model.PhoneNumber,
                        StatusId = model.StatusId,
                        Specializations = specializations,
                        WorkingHours = (int)model.WorkingHours,
                    };

                    user.Coach = coach;
                    await this._userManager.AddToRoleAsync(user, role.Name);
                    break;
                case UserRoles.Supervisor:
                case UserRoles.SeniorSupervisor:
                    var supervisor = new Supervisor
                    {
                        Id = user.Id,
                        Firstname = model.Firstname,
                        Lastname = model.Lastname,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        GenderId = model.GenderId,
                        PhoneNumber = model.PhoneNumber,
                        StatusId = model.StatusId,
                    };

                    user.Supervisor = supervisor;

                    await this._userManager.AddToRoleAsync(user, role.Name);
                    break;
            }
            return new Response { Status = "Success", Message = "Stuff Created Successfully" };
        }

        public async Task<Response> RegisterSeniorSupervisor(RegisterSupervisorDto model)
        {
            var userExist = await GetUserByUsernameAsync(model.Username);

            if (userExist != null)
            {
                throw new HttpRequestException("User already existed", null, HttpStatusCode.Forbidden);
            }

            User user = new User()
            {
                Email = model.Email,
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = BCryptNet.HashPassword(model.Password),
                IsStuff = true,
                IsCustomer = false
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            var supervisor = new Supervisor
            {
                Id = user.Id,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                DateOfBirth = model.DateOfBirth,
                Email = model.Email,
                GenderId = model.GenderId,
                PhoneNumber = model.PhoneNumber,
                StatusId = model.StatusId,
            };

            user.Supervisor = supervisor;

            await this._userManager.AddToRoleAsync(user, UserRoles.SeniorSupervisor);

            return new Response { Status = "Success", Message = "Senior Supervisor Created Successfully" };
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

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            var token = this.CreateToken(authClaims);
            var refreshToken = this.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            var userInfo = await this._userService.GetUserInfoAsync(user.Id);

            return new ResLoginDto
            {
                User = userInfo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }

        protected async Task<User> GetUserByUsernameAsync(string username)
        {
            if (username.Contains(" "))
            {
                throw new HttpRequestException("Username could not contains spaces", null, HttpStatusCode.BadRequest);
            }

            var user = await this._userManager.FindByNameAsync(username);

            return user;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            if (tokenDto is null)
            {
                throw new HttpRequestException("Invalid client request", null, HttpStatusCode.BadRequest);
            }

            string? accessToken = tokenDto.AccessToken;
            string? refreshToken = tokenDto.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                throw new HttpRequestException("Invalid access token or refresh token", null, HttpStatusCode.BadRequest);
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new HttpRequestException("Invalid access token or refresh token", null, HttpStatusCode.BadRequest);
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWT:SecretKey"]));

            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}

