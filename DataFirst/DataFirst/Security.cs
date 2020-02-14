using CodeFirst.Models;
using System.Linq;


namespace CarPoolApplication
{
    public class Security
    {
        public static bool login(string username,string password)
        {
            using (var context=new Context())
            {
                return context.Users.Any(_ => _.Username == username && _.Password == password);
            }
        }
    }
}
