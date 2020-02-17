using CarPoolApplication.Concerns;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolApplication.Helpers
{
    public static class ExtensionMethods
    {
        public static List<User> WithoutPasswords(this List<User> users)
        {
            if (users == null) return null;

            users.ForEach(x => x.WithoutPassword());
            return users;
        }

        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}