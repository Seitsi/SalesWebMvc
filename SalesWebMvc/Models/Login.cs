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
        public string Password { get; set; }
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
        public string ConfirmPassword { get; set; }

        public Login() { }
        public Login(int id, string user, string password, string email, string phoneNumber, string cpf, bool ativo, TipoUsuario tipoUsuario, bool logado)
        {
            Id = id;
            User = user;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            Cpf = cpf;
            Ativo = true;
            TipoUsuario = tipoUsuario;
            Logado = false;
        }

        //public void ValidaLogin(string user, string password)
        //{
        //    if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        //    {
        //        thwo
        //    }
        //}

        public void CriarNovoLogin(Login user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

        }

        public string CpfRegex(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");           
            return cpf;
        }

    }
}
