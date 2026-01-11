namespace KhachHangWeb.Models;

public class LoginViewModel
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
}

public class RegisterViewModel
{
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string? Email { get; set; }
    public string? Cccd { get; set; }
    public string? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string ConfirmPassword { get; set; } = "";
}
