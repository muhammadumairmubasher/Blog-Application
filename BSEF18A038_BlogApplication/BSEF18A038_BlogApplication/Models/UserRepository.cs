using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSEF18A038_BlogApplication.Models
{
    static public class UserRepository
    {
        public static List<User> users = new List<User>();
        static UserRepository()
        {
        }
    }
}
