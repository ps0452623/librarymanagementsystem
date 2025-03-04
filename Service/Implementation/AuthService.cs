using AutoMapper;
using DataAcessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DataAccessLayer.Repository;
using DataAcessLayer.Entities;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Service.Implementation
{
    public class AuthService : IAuthService
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager,  SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            
        }

        public async Task<string> RegisterAsync(RegistrationDto model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);


            if (!result.Succeeded)
                return null; // Registration failed
            await _userManager.AddToRoleAsync(user, model.Role);
            return "User registered successfully!";
        }

        public async Task<string> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return null; // Invalid credentials

            return GenerateJwtToken(user);
        }


        private string GenerateJwtToken(ApplicationUser user)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

                 

          
        }
    }

