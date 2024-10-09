using WebAppNet.Models;

namespace WebAppNet.Interface
{
    public interface IHobyRepo
    {
        IList<Hobby> getHobby(string email);
        IList<HobbyGenerate> generateHobby();

        bool saveHobby(string Name, string Gender, string Hobby_detail, string Age);
    }
}
