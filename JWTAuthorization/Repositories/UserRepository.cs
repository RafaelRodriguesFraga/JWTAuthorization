using JWTAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JWTAuthorization.Repositories
{
    public static class UserRepository
    {
        public static User GetUser(string username, string paswword)
        {
            List<User> users = new List<User>();        
            users.Add(new User { Id = 1, Username = "Dean Winchester", Password = "Supernatural", Role = "manager"});
            users.Add(new User { Id = 2, Username = "Sam Winchester", Password = "Supernatural", Role = "employee" });

            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password.ToLower() == paswword.ToLower()).FirstOrDefault();

        }
    }
}
