using HospitalBB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalBB.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly HospitalBBContextClass _context;

        private const string DoctorsRole = "Doctor";
        private const string PatientsRole = "Patient";
        private const string AdminRole = "Admin";
        public TokenController(IConfiguration config, HospitalBBContextClass context)
        {
            _configuration = config;
            _context = context;
        }
        [HttpPost("Doctor")]
        public async Task<IActionResult> Post(Doctor _userData)
        {
            if (_userData != null && _userData.DocEmail != null && _userData.DocPassword != null)
            {
                var user = await GetUser(_userData.DocEmail, _userData.DocPassword);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("DocId", user.DocId.ToString()),
                        new Claim("DocEmail", user.DocEmail),
                        new Claim("DocPassword",user.DocPassword),
                        new Claim(ClaimTypes.Role, DoctorsRole)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        private async Task<Doctor> GetUser(string name, string password)
        {
            return await _context.Doctors.FirstOrDefaultAsync(x => x.DocEmail == name && x.DocPassword == password);

        }
        [HttpPost("Patients")]
        public async Task<IActionResult> Post(Patient _userData)
        {
            if (_userData != null && _userData.PatEmail != null && _userData.PatPassword != null)
            {
                var user = await GetUsers(_userData.PatEmail, _userData.PatPassword);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("PatId", user.PatId.ToString()),
                        new Claim("PatEmail", user.PatEmail),
                        new Claim("Password",user.PatPassword),
                        new Claim(ClaimTypes.Role, PatientsRole)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Patient> GetUsers(string name, string password)
        {
            return await _context.Patients.FirstOrDefaultAsync(x => x.PatEmail == name && x.PatPassword == password);

        }
        [HttpPost("Admin")]
        public async Task<IActionResult> PostStaff(Admin staffData)
        {
            if (staffData != null && !string.IsNullOrEmpty(staffData.AdminEmail) && !string.IsNullOrEmpty(staffData.AdminPassword))
            {
                if (staffData.AdminEmail == "Roger@gamil.com" && staffData.AdminPassword == "Roger@123")
                {
                    var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("AdminId", "1001"),
                new Claim("Admin_name", staffData.AdminName),
                new Claim("Admin_Password", staffData.AdminPassword),
                new Claim(ClaimTypes.Role, AdminRole)
            };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:ValidIssuer"],
                        _configuration["Jwt:ValidAudience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        private async Task<Admin> GetStaff(string adminName, string adminPassword)
        {
            return await _context.Admins.FirstOrDefaultAsync(s => s.AdminEmail == adminName && s.AdminPassword == adminPassword);
        }
    }
}
