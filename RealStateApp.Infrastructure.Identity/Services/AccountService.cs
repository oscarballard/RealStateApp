using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.Roles;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Options;
using RealStateApp.Core.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace RealStateApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;


        public AccountService
        (
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            IEmailService emailService,
            IOptions<JWTSettings> jwtSettings
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay usuario registrado con este usuario: {request.UserName}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales invalidas {request.UserName}";
                return response;
            }

            if (_jwtSettings.Key != null)
            {
                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
                if (_jwtSettings.Key != null)
                {
                    response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                }
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.Photo = user.Photo;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;


            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<UsersViewModel>> GetUsersAsync()
        {
            var users = await _userManager.Users.Select(user => new UsersViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsActive = user.EmailConfirmed
                //Identification = user.Identification
            }).OrderBy(u => u.FirstName).ToListAsync();

            foreach (var u in users)
            {
                var user = await _userManager.FindByIdAsync(u.Id);
                u.Type = await _userManager.GetRolesAsync(user);
            }

            return users;
        }

        public async Task<UsersViewModel> GetUserByIdAsync(string Id)
        {
            var users = await _userManager.FindByIdAsync(Id);
            UsersViewModel user = new();
            user.Id = users.Id;
            user.FirstName = users.FirstName;
            user.LastName = users.LastName;
            user.Email = users.Email;
            user.Phone = users.PhoneNumber;
            user.Photo = users.Photo;
            return user;
        }
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"El username '{request.UserName}' Ya se está usando.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, request.Password);

            var rolName = await _roleManager.FindByIdAsync(request.RolId);
            //await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
            //userWithSameUserName = await _userManager.FindByIdAsync(user.Id);

            if (user.Id != "" && user != null && request.Photo != null)
            {
                request.Photo = UploadFile(request.File, user.Id);
                await this.UpdateUser(request, user.Id);
            }

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }
            await _userManager.AddToRoleAsync(user, rolName.Name);

            return response;
        }

        public async Task<RegisterResponse> UpdateUser(RegisterRequest request, string Id)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByIdAsync(Id);

            userWithSameUserName.Email = request.Email;
            userWithSameUserName.FirstName = request.FirstName;
            userWithSameUserName.LastName = request.LastName;
            userWithSameUserName.UserName = request.UserName;
            userWithSameUserName.Photo = request.Photo; 
            userWithSameUserName.IsActive = request.IsActive;

            var result = await _userManager.UpdateAsync(userWithSameUserName);

            if (userWithSameUserName == null)
            {
                response.HasError = true;
                response.Error = $"El username '{request.UserName}' no se encuentra registrado.";
                return response;
            }

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;

        }

        public async Task Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.EmailConfirmed = false;
            await _userManager.UpdateAsync(user);
        }
        public async Task Inactive(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.IsActive = false;
            await _userManager.UpdateAsync(user);
        }

        public async Task Active(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.IsActive = true;
            await _userManager.UpdateAsync(user);
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred wgile confirming {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new RealStateApp.Core.Application.DTOs.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "reset password"
            });


            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        #region PrivateMethods

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }


        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        #endregion
        public async Task<List<RolesViewModel>> GetAllRoles()
        {
            var rolesList = await _roleManager.Roles.ToListAsync();
            List<RolesViewModel> roles = new();
            rolesList.ForEach(item => roles.Add(
                new RolesViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                }
            ));
            return roles;
        }

        public async Task<RolesViewModel> GetRolByName(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return new RolesViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public async Task<List<UsersViewModel>> GetUserByRol(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);

            return users.Select(user => new UsersViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsActive = user.EmailConfirmed,
                Photo = user.Photo
                //Identification = user.Identification
            }).OrderBy(u => u.FirstName).ToList();
        }

        private string UploadFile(IFormFile file, string id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Users/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }

    }


}
