using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("api/Auth/[Action]")]
[ApiController]
public class AuthController : Controller
{
    private Context db;
    private string _secretKey = "Salam in Secret Key man HasTeSh Masalan , badan baIad AvazeSh Konam";
    public AuthController(Context _db)
    {
        db = _db;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult login(Login UserPass)
    {
        if (db.Users_tbl.Count() == 0)
        {
            db.Users_tbl.Add(new Users
            {
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin" + "admin")
            });
            db.SaveChanges();
        }

        Users? check = db.Users_tbl.FirstOrDefault(x => x.Username == UserPass.Username);
        if (check != null)
        {
            if (BCrypt.Net.BCrypt.Verify(UserPass.Password+UserPass.Username, check.Password))
            {
                return Ok(new {token = newToken(check.Id, check.Username)});
            }
        }
        return Unauthorized("ورود غیرمجاز");
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ValidateToken()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "*****",
                    ValidAudience = "*****",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
                }, out SecurityToken validatedToken);

                return Ok(new { valid = true });
            }
            catch
            {
                return Unauthorized(); // اشتباه
            }
        }
        return Unauthorized(); // خالی
    }

    private string newToken(int userId, string Username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, Username),
                    new Claim("UserID", userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(3), 
            Issuer = "*****",
            Audience = "*****",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}