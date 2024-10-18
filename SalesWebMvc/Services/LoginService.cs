using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

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

        public Login ValidateLogin(Login login)
        {           
            var cpf = login.CpfRegex(login.Cpf);
            var cpfExistente = _context.Login.Any(v => v.Cpf == cpf);
            if (cpfExistente)
            {
                throw new ValidateLoginException("CPF já existe em nossa base de dados!");
            }

            var userExistente = _context.Login.Any(v => v.User == login.User);
            if (userExistente)
            {
                throw new ValidateLoginException("Nome de Usuário já existe em nossa base de dados!");
            }

            AjustarPrimeiroCadastroLogin(login);

            return login;
        }

        public Login AjustarPrimeiroCadastroLogin(Login login)
        {
            login.Ativo = true;
            login.TipoUsuario = Models.Enums.TipoUsuario.PADRAO;
            return login;
        }
    }
}
