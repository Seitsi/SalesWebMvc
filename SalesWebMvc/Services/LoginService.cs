using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class LoginService
    {
        private readonly SalesWebMvcContext _context;

        public LoginService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Login login)
        {
            _context.Add(login);
            await _context.SaveChangesAsync();
        }

        public bool ValidateLogin(Login login)
        {
            var valid = login.IsCpfValid(login.Cpf);
            return valid;
        }
    }
}
