using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

namespace CarPoolApplication.Services
{
    public class UserService : IUserService
    {
        readonly IServiceScope _scope;
        public UserService(IServiceProvider service)
        {          
            _scope = service.CreateScope();
        }
        public HttpResponseException Add(User user)
        {
            Context _context;
            try
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadGateway);
            }
            try
            {               
                user.IsActive = true;
                _context.Users.Add(user);
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);
            }
            catch (Exception)
            {
                _context.Users.Remove(user);
                return new HttpResponseException(System.Net.HttpStatusCode.Conflict );
            }

        }
        public HttpResponseException Delete(int id)
        {
            Context _context;
            try
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadGateway);
            }
            try
            {             
                _context.Users.Find(id).IsActive=false;
                return new HttpResponseException(System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            
        }
        public List<User> GetAll()
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Users.ToList();
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
                return _scope.ServiceProvider.GetRequiredService<Context>().Users.Find(id);
            }
            catch (Exception)
            {
                return null;
            }           
        }
    }
}
