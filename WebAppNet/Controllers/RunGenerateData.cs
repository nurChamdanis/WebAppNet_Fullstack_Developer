using WebAppNet.Models;

namespace WebAppNet.Controllers
{
    public class RunGenerateData
    {
        private static Random random = new Random();
        private static string[] names = { "Alice", "Bob", "Charlie", "David", "Eva", "Frank", "Grace", "Hannah", "Ian", "Jack" };
        private static string[] genders = { "Pria", "Wanita" };
        private static string[] hobbies = { "SepakBola", "Badminton", "Tennis", "Renang", "Bersepeda", "Tidur" };
         

        public static List<HobbyGenerate> GenerateRandomUsers(int count)
        {
            var users = new List<HobbyGenerate>();

            for (int i = 0; i < count; i++)
            {
                var user = new HobbyGenerate
                {
                    Name = GetRandomName(),
                    Gender = GetRandomGender(),
                    Hobby_detail = GetRandomHobby(),  
                    Age = GetRandomAge()
                };

                users.Add(user);
            }

            return users;
        }

        private static string GetRandomName()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var randomName = new char[names.Length]; 
            for (int i = 0; i < names.Length; i++)
            {
                randomName[i] = chars[random.Next(chars.Length)];
            } 
            return new string(randomName); 
        }

        private static string GetRandomGender()
        {
            return genders[random.Next(genders.Length)];
        }

        private static string GetRandomHobby()  
        {
            return hobbies[random.Next(hobbies.Length)];
        }

        private static int GetRandomAge()
        {
            return random.Next(18, 41);  
        }
    }

}