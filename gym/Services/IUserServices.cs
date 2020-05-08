using gym.Dtos;
using gym.Entity;

namespace gym.Services
{
    public interface IUserServices
    {
        User Authenticate(string ContactNo, string Password);
        AdminDto SignUp(AdminDto adminDto, string Password);
        MemberDto SignUp(MemberDto memberDto, string Password);
        bool VerifyPasswordHash(string Password, byte[] storedHash, byte[] storedSalt);
    }
}