
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAppNet.Controllers;
using WebAppNet.DBContext;
using WebAppNet.Interface;
using WebAppNet.Models;

namespace WebAppNet.Repository
{
    public class HobbyRepo : IHobyRepo
    {
        private readonly ApplicationDbContext _context;

        public HobbyRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        IList<HobbyGenerate> IHobyRepo.generateHobby()
        {
            var randomUsers = RunGenerateData.GenerateRandomUsers(10);  
            return randomUsers;
        }


        IList<Hobby> IHobyRepo.getHobby(string email)
        {
            var sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sql", "getHobby.sql");
            var sql = File.ReadAllText(sqlFilePath);
            sql = sql.Replace("{Email}", email);
            // Directly assign the result to IList<Users>
            IList<Hobby> hobby = (IList<Hobby>)_context.Users.FromSqlRaw(sql).ToList();
            return hobby;
        }



        bool IHobyRepo.saveHobby(string Name, string Gender, string Hobby_detail, string Age)
        {
            try
            {
                var sqlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sql", "addHobby.sql");
                var sql = File.ReadAllText(sqlFilePath);

                // Log the SQL command for debugging
                Console.WriteLine("Executing SQL: " + sql);

                // Use parameterized query to prevent SQL injection
                var parameters = new[]
                {
                    new SqlParameter("@Email", Name),
                    new SqlParameter("@Gender", Gender),
                    new SqlParameter("@Hobby_detail", Hobby_detail),
                    new SqlParameter("@Age", Age)
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