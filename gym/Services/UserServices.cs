using AutoMapper;
using gym.Dtos;
using gym.Entity;
using gym.Helpers;
using gym.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace gym.Services
{
    public class UserServices : IUserServices
    {
        private ICommonRepository<User> _userRepository;
        private ICommonRepository<Member> _memberRepository;
        private ICommonRepository<Admin> _adminRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public UserServices(
            ICommonRepository<User> userRepository,
            ICommonRepository<Member> memberRepository,
            ICommonRepository<Admin> adminRepository,
            ICommonRepository<Gym> gymRepository,
            IOptions<AppSettings> appsettings)
        {
            _userRepository = userRepository;
            _memberRepository = memberRepository;
            _adminRepository = adminRepository;
            _appSettings = appsettings;
        }

        public MemberDto SignUp(MemberDto memberDto,string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
                throw new ApplicationException("Password is Required");

            var result = _userRepository.Get(x => x.MobileNumber == memberDto.MobileNumber);

            if (result!=null)
                throw new ApplicationException("Mobile number" + memberDto.MobileNumber + "is alredy taken");

            //var AllowedMembers=
            var CountAllMembers = _memberRepository.GetAll().Count();
            //if(CountAllMembers>=AllowedMembers)

            Member memberToAdd = Mapper.Map<Member>(memberDto);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(Password, out passwordHash, out passwordSalt);

            memberToAdd.PasswordSalt = passwordHash;
            memberToAdd.PasswordHash = passwordHash;

            _memberRepository.Add(memberToAdd);
            _memberRepository.Save();

            return Mapper.Map<MemberDto>(memberToAdd);
        }

        public AdminDto SignUp(AdminDto adminDto, string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
                throw new ApplicationException("Password is Required");

            var result = _userRepository.Get(x => x.MobileNumber == adminDto.MobileNumber);

            if (result != null)
                throw new ApplicationException("Mobile number" + adminDto.MobileNumber + "is alredy taken");

            Admin adminToAdd = Mapper.Map<Admin>(adminDto);

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(Password, out passwordHash, out passwordSalt);

            adminToAdd.PasswordSalt = passwordHash;
            adminToAdd.PasswordHash = passwordHash;

            _adminRepository.Add(adminToAdd);
            _adminRepository.Save();

            return Mapper.Map<AdminDto>(adminToAdd);
        }

        public User Authenticate(short ContactNo, string Password)
        {
            if ( string.IsNullOrEmpty(Password))
                throw new ApplicationException("Contact No or Password cannot be empty");

            var user = _userRepository.Get(x => x.MobileNumber == ContactNo).SingleOrDefault();

            if (user == null)
                return null;

            if (!VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt)) return null;

            var tokenHandeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandeler.CreateToken(tokenDescriptor);
            user.Token = tokenHandeler.WriteToken(token);

            return user;
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string Password,byte[] storedHash,byte[] storedSalt)
        {
            if (Password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                for(int i = 0; i < ComputeHash.Length; i++)
                {
                    if (ComputeHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
