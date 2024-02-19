using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace projet_net.Data
{
    public class Auth : IAuth
    {
        private readonly DataContext dataBase;
        private readonly IConfiguration config;
        public Auth(DataContext _dataBase,IConfiguration conf)
        {
            config = conf;
            dataBase = _dataBase;

        }
        public async Task<string> Login(string userName, string password)
        {
            var user=await dataBase.Users.FirstOrDefaultAsync(ele=>ele.userName.ToLower().Equals(userName.ToLower()));
            if(user is null)
                throw new Exception("user does not exist");

            if(verifpassword(password,user.passwordHash,user.passwordSalt))
                return createToken(user);
            else 
                return string.Empty;
        }

        public async Task<int> Register(User user, string password)
        {
            if(await UserExists(user.userName))
                throw new Exception("user exist");
                
            cryptePassword(password,out byte[]passwordHash,out byte [] passwordSalt);
            user.passwordSalt=passwordSalt;
            user.passwordHash=passwordHash;
            dataBase.Users.Add(user);
            await dataBase.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await dataBase.Users.AnyAsync(ele=>ele.userName.ToLower()==username.ToLower()))
                return true;
            return false;
        }

        private void cryptePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var pas = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt=pas.Key;
                passwordHash = pas.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }
        private bool verifpassword(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using(var pas=new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var pwdHash=pas.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return pwdHash.SequenceEqual(passwordHash);
            }
        }

        private string createToken(User user)
        {
            var claims=new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.userName),
            };

            var appSettingToken=config.GetSection("AppSettings:Token").Value;
            if(appSettingToken is null)
                throw new Exception("App setting token key is null");
            
            SymmetricSecurityKey key=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingToken));

            SigningCredentials creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription=new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };

            JwtSecurityTokenHandler tokenHandler=new JwtSecurityTokenHandler();
            SecurityToken token=tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);

        }
    }
}