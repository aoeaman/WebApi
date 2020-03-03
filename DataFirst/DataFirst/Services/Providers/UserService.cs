﻿using System;
using System.Collections.Generic;
using CarPoolApplication.Concerns;
using CodeFirst.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CarPoolApplication.Helpers;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarPoolApplication.Services
{
    public class UserService : IUserService
    {
        readonly IServiceScope _scope;
        private readonly AppSettings _appSettings;
        readonly Context _context;

        public UserService(IServiceProvider service,IOptions<AppSettings> appSettings)
        {          
            _scope = service.CreateScope();
            _appSettings = appSettings.Value;
            _context = _scope.ServiceProvider.GetRequiredService<Context>();
        }
        public User Add(User user)
        {
            try
            {               
                user.IsActive = true;
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                _context.Users.Remove(user);
                return null;
            }

        }
        public string Delete(int id)
        {
            try
            {
                if (_context.Users.Find(id).IsActive)
                {
                    _context.Users.Find(id).IsActive = false;
                    return Status.Ok.ToString();
                }
                else
                {
                    return Status.UnableToPerformAction.ToString();
                }
            }
            catch (Exception)
            {
                return Status.DbError.ToString();
            }
            
        }
        public List<User> GetAll()
        {
            try
            {
                return _context.Users.ToList().WithoutPasswords();
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        public User GetByID(int id)
        {
            try
            {
                return _context.Users.Find(id).WithoutPassword();
            }
            catch (Exception)
            {
                return null;
            }           
        }
        public User Authenticate(string username, string password)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user.WithoutPassword();
            }
            catch (Exception)
            {
                return null;
            }


        }
    }
}
