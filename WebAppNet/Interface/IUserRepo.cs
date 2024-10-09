using WebAppNet.Models;

namespace WebAppNet.Interface
{
    public interface IUserRepo
    {
        IList<Users> Authenticate(string email, string password); 
        bool Register(string email, string password);
    }
}
