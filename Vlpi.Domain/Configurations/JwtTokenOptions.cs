using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Vlpi.Domain.Configurations
{
    public class JwtTokenOptions
    {
        public const string ISSUER = "VlpiServer"; 
        public const string AUDIENCE = "VlpiClient";
        const string KEY = "vlpiPZ452021testsecretkey!!";   
        public const int LIFETIME = 44000;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
