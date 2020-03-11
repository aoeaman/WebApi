using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CarPool.Data.Models;
using CarPool.Services.Contracts;
using CarPool.Helpers;
using CarPool.Application.Models;
using AutoMapper;
using CodeFirst;

namespace CarPool.Services.Providers
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        readonly Context _context;
        private readonly IMapper _mapper;

        public UserService(Context context,IOptions<AppSettings> appSettings,IMapper mapper)
        {          
            _appSettings = appSettings.Value;
            _context = context;
            _mapper = mapper;
        }
        public User Add(User user)
        {
            var _user = _mapper.Map<UserDBO>(user);
            try
            {

                _user.Role = user.DrivingLiscenceNumber == null ?Role.User :Role.Admin;

                _user.IsActive = true;
                _context.Users.Add(_user);
                _context.SaveChanges();
                return _mapper.Map<User>(_user);
            }
            catch (Exception)
            {
                _context.Users.Remove(_user);
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
                List<User> Users = new List<User>();
                foreach (var user in _context.Users)
                {
                    Users.Add(_mapper.Map<User>(user));
                }
                return Users.WithoutPasswords();
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
                return _mapper.Map<User>(_context.Users.Find(id)).WithoutPassword();
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

                return _mapper.Map < User > (user).WithoutPassword();
            }
            catch (Exception)
            {
                return null;
            }


        }
    }
}
