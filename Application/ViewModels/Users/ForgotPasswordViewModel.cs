using System.ComponentModel.DataAnnotations;

namespace StockApp.Core.Application.ViewModels.Users
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Debe colocar el email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}
