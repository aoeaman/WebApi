using CarPool.Data.Models;
using System.Collections.Generic;

namespace CarPool.Helpers
{ 
    public static class ExtensionMethods
    {
        public static List<UserDBO> WithoutPasswords(this List<UserDBO> users)
        {
            if (users == null) return null;

            users.ForEach(x => x.WithoutPassword());
            return users;
        }

        public static UserDBO WithoutPassword(this UserDBO user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}