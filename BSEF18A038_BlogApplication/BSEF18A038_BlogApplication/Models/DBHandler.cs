using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace BSEF18A038_BlogApplication.Models
{
    static public class DBHandler
    {
        static string connectionString = string.Empty;
        static SqlConnection conn;
        static DBHandler()
        {
            connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                conn = new SqlConnection(connectionString);
            }
            catch (Exception e)
            {

            }
        }
        static public bool registerUser(User u)
        {
            string query = $"insert into Users(Username, Email, Password, picture) values('{u.Username}', '{u.Email}', '{u.Password}', 'UserIcon.jpg')";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }
        static public (bool, User) validUser(string email, string password)
        {
            string query = $"select * from Users where Email = '{email}' and Password = '{password}'";
            User u = new User();
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {

                    u.Id = (int)dr.GetValue(0);
                    u.Username = (string)dr.GetValue(1);
                    u.Email = (string)dr.GetValue(2);
                    u.Password = (string)dr.GetValue(3);
                    u.pictureFileName = (string)dr.GetValue(4);
                    conn.Close();
                    return (true, u);
                }
                else
                {
                    conn.Close();
                    return (false, u);
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return (false, u);
            }

        }

        static public User GetUser(string email)
        {
            string query = $"select * from Users where Email='{email}'";
            User u = new User();
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader dr = sqlCommand.ExecuteReader();

                if (dr.Read())
                {
                    u.Id = (int)dr.GetValue(0);
                    u.Username = (string)dr.GetValue(1);
                    u.Email = (string)dr.GetValue(2);
                    u.Password = (string)dr.GetValue(3);
                    u.pictureFileName = (string)dr.GetValue(4);
                    conn.Close();
                    return (u);
                }
                conn.Close();
                return (u);

            }
            catch (Exception e)
            {
                conn.Close();
                return (u);
            }
        }
        static public bool UpdateUser(User u ,string newPassword , string filename)
        {
            string query = $"Update Users set Username = '{u.Username}' , Password = '{newPassword}', picture = '{filename}' where Email='{u.Email}'";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        static public bool UpdateUserByAdmin(User u)
        {
            string query = $"Update Users set Username = '{u.Username}' , Password = '{u.Password}' where Id='{u.Id}'";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        static public List<User> getUsersList()
        {
            string query = $"select * from Users";
            List<User> users = new List<User>();
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader dr = sqlCommand.ExecuteReader();

                while (dr.Read())
                {
                    User u = new User();
                    u.Id = (int)dr.GetValue(0);
                    u.Username = (string)dr.GetValue(1);
                    u.Email = (string)dr.GetValue(2);
                    u.Password = (string)dr.GetValue(3);
                    u.pictureFileName = (string)dr.GetValue(4);
                    users.Add(u);

                }
                conn.Close();
                return (users);

            }
            catch (Exception e)
            {
                conn.Close();
                return (users);
            }
        }

        static public bool DeleteUser(int id)
        {
            string query = $"Delete from Users where Id='{id}'";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }

        }

        static public bool addPost(Post p)
        {
            string query = $"insert into Posts(Title, Content, UserId, Email, Date, UserProfilePicture) values('{p.Title}', '{p.Content}', '{p.UserId}','{p.UserEmail}','{System.DateTime.Now.ToString("MMMM dd, yyyy")}', '{p.UserProfilePicture}')";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }
        static public List<Post> GetPosts()
        {
            string query = $"select * from Posts";
            List<Post> posts = new List<Post>();
            try
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader dr = sqlCommand.ExecuteReader();

                while (dr.Read())
                {
                    Post p = new Post();
                    p.Id = (int)dr.GetValue(0);
                    p.Title = (string)dr.GetValue(1);
                    p.Content = (string)dr.GetValue(2);
                    p.UserId = (int)dr.GetValue(3);
                    p.UserEmail = (string)dr.GetValue(4);
                    p.Date = (string)dr.GetValue(5);
                    p.UserProfilePicture = (string)dr.GetValue(6);
                    posts.Add(p);

                }
                conn.Close();
                return (posts);

            }
            catch (Exception e)
            {
                conn.Close();
                return (posts);
            }
        }
        static public bool UpdatePost(Post p)
        {
            string query = $"Update Posts set Title = '{p.Title}' , Content = '{p.Content}' where Id='{p.Id}'";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }

        }
        static public bool DeletePost(int id)
        {
            string query = $"Delete from Posts where Id='{id}'";
            try
            {
                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                int r = command.ExecuteNonQuery();
                if (r > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }

        }
    }
}
