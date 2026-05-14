namespace VideoGame.Models.DTOs
{
    public class ResetPassword
    {

        public string UserName { get; set; } = string.Empty;
        public string PasswordHashed { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
