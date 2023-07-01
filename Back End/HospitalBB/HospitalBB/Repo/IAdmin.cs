using HospitalBB.Models;

namespace HospitalBB.Repo
{
    public interface IAdmin
    {
        Task<Admin>CreateAdmin(Admin admin);
    }
}
    