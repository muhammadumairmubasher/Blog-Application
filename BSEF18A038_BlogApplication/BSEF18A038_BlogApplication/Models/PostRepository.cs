using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSEF18A038_BlogApplication.Models
{
    static public class PostRepository
    {
        public static List<Post> posts = new List<Post>();
        static PostRepository()
        {
        }
    }
}
