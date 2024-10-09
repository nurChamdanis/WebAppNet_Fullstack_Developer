using WebAppNet.DBContext;
using WebAppNet.Models;
using Microsoft.EntityFrameworkCore;
using WebAppNet.Interface;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace WebAppNet.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        IList<Users> IUserRepo.Authenticate(string email, string password)
        {
            var sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sql", "getUser.sql");
            var sql = File.ReadAllText(sqlFilePath);
             
            var users = _context.Users.FromSqlRaw(sql, new SqlParameter("@Email", email), new SqlParameter("@Password", password)).ToList();

            return users;
        }

        bool IUserRepo.Register(string email, string password)
        {
            try
            {
                var sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sql", "registUser.sql");
                var sql = File.ReadAllText(sqlFilePath);

                // Log the SQL command for debugging
                Console.WriteLine("Executing SQL: " + sql);

                // Use parameterized query to prevent SQL injection
                var parameters = new[]
                {
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Password", password)
                };

                // Execute the command and check the result
                var result = _context.Database.ExecuteSqlRaw(sql, parameters);

                return result > 0; // Returns true if the command affected rows
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, rethrow, etc.)
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
