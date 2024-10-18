using SalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Login
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Usuário é obrigatório.")]
        public string User { get; set; }
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Celular é obrigatório.")]
        [RegularExpression(@"^\+?[0-9\s\-\(\)]*$", ErrorMessage = "Insira um número de telefone válido.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public bool Logado { get; set; }

        [Required(ErrorMessage = "Confirme a sua senha.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPasswordHash { get; set; }

        public Login() { }
        public Login(int id, string user, string passwordHash, string email, string phoneNumber, string cpf, bool ativo, TipoUsuario tipoUsuario, bool logado, string confirmPasswordHash)
        {
            Id = id;
            User = user;
            PasswordHash = passwordHash;
            Email = email;
            PhoneNumber = phoneNumber;
            Cpf = cpf;
            Ativo = true;
            TipoUsuario = tipoUsuario;
            Logado = false;
            ConfirmPasswordHash = confirmPasswordHash;
        }

        public string CpfRegex(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");           
            return cpf;
        }

    }
}
