using HospitalBB.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalBB.Repo
{
    public class AdminRepo:IAdmin
    {
        private readonly HospitalBBContextClass _contextClass;
        private readonly IWebHostEnvironment _environment;

        public AdminRepo(HospitalBBContextClass contextClass, IWebHostEnvironment environment)
        {
            _contextClass = contextClass;
            _environment = environment;
        }
        public async Task<Admin> CreateAdmin(Admin admin)
        {
            try
            {
                if (_contextClass.Admins == null)
                {
                    throw new NullReferenceException("Entity set 'HospitalContext.patients' is null.");
                }

                _contextClass.Admins.Add(admin);
                await _contextClass.SaveChangesAsync();

                return admin;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
