using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace _31241027221_NguyenHoangHung.Areas.Identity.Pages.Account;

[AllowAnonymous]
public sealed class RegisterConfirmationModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHostEnvironment _environment;

    public RegisterConfirmationModel(UserManager<IdentityUser> userManager, IHostEnvironment environment)
    {
        _userManager = userManager;
        _environment = environment;
    }

    public string Email { get; private set; } = string.Empty;
    public bool DisplayConfirmAccountLink { get; private set; }
    public string EmailConfirmationUrl { get; private set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return NotFound("Không tìm thấy tài khoản cần xác nhận.");
        }

        Email = email;
        DisplayConfirmAccountLink = _environment.IsDevelopment();
        if (!DisplayConfirmAccountLink)
        {
            return Page();
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        EmailConfirmationUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId, code = encodedCode, returnUrl },
            protocol: Request.Scheme) ?? string.Empty;

        return Page();
    }
}
