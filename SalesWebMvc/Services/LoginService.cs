using Microsoft.CodeAnalysis.Scripting;
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

        public Login ValidateCadastroLogin(Login login)
        {           
            var passwordHash = HashPassword(login.PasswordHash);
            var confirmPasswordHash = HashPassword(login.ConfirmPasswordHash);
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

            AjustarPrimeiroCadastroLogin(login, passwordHash, confirmPasswordHash);

            return login;
        }

        public Login AjustarPrimeiroCadastroLogin(Login login , string passwordHash, string confirmPasswordHash)
        {
            login.PasswordHash = passwordHash;
            login.ConfirmPasswordHash = confirmPasswordHash;
            login.Ativo = true;
            login.TipoUsuario = Models.Enums.TipoUsuario.PADRAO;
            return login;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public Login ValidateLogin(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || !IsValidEmail(login.Email))
            {
                throw new ValidateLoginException("E-mail inválido.");
            }

            var usuario = _context.Login.SingleOrDefault(u => u.Email == login.Email);
            if (usuario == null)
            {
                throw new ValidateLoginException("E-mail não cadastrado.");
            }

            if (!VerifyPassword(login.PasswordHash, usuario.PasswordHash))
            {
                throw new ValidateLoginException("Senha incorreta.");
            }

            return usuario;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
