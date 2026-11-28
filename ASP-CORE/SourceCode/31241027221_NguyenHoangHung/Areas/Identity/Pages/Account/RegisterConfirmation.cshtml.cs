using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
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
    private readonly IEmailSender _emailSender;
    private readonly ILogger<RegisterConfirmationModel> _logger;

    public RegisterConfirmationModel(
        UserManager<IdentityUser> userManager,
        IHostEnvironment environment,
        IEmailSender emailSender,
        ILogger<RegisterConfirmationModel> logger)
    {
        _userManager = userManager;
        _environment = environment;
        _emailSender = emailSender;
        _logger = logger;
    }

    public string Email { get; private set; } = string.Empty;
    public bool DisplayConfirmAccountLink { get; private set; }
    public bool EmailConfirmed { get; private set; }
    public string EmailConfirmationUrl { get; private set; } = string.Empty;
    public string ReturnUrl { get; private set; } = string.Empty;
    public string EmailDeliveryStatus { get; private set; } = string.Empty;
    public string ResendStatus { get; private set; } = string.Empty;

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
        ReturnUrl = GetLocalReturnUrl(returnUrl);
        EmailDeliveryStatus = TempData["EmailDeliveryStatus"] as string ?? string.Empty;
        ResendStatus = TempData["ResendStatus"] as string ?? string.Empty;
        EmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        DisplayConfirmAccountLink = _environment.IsDevelopment() && !EmailConfirmed;
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
            values: new { area = "Identity", userId, code = encodedCode, returnUrl = ReturnUrl },
            protocol: Request.Scheme) ?? string.Empty;

        return Page();
    }

    public async Task<IActionResult> OnPostResendAsync(string email, string returnUrl = null)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return RedirectToPage("/Index", new { area = "" });
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return NotFound("Không tìm thấy tài khoản cần xác nhận.");
        }

        var localReturnUrl = GetLocalReturnUrl(returnUrl);
        if (await _userManager.IsEmailConfirmedAsync(user))
        {
            TempData["ResendStatus"] = "confirmed";
            return RedirectToPage("RegisterConfirmation", new { email, returnUrl = localReturnUrl });
        }

        try
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId, code = encodedCode, returnUrl = localReturnUrl },
                protocol: Request.Scheme);

            if (string.IsNullOrWhiteSpace(callbackUrl))
            {
                throw new InvalidOperationException("Could not create the email confirmation URL.");
            }

            var message = $"<p>Chào bạn,</p><p>Hãy xác nhận địa chỉ email để kích hoạt tài khoản Góc Phim Hưng.</p><p><a href=\"{HtmlEncoder.Default.Encode(callbackUrl)}\">Xác nhận email</a></p>";
            await _emailSender.SendEmailAsync(email, "Xác nhận email - Góc Phim Hưng", message);
            TempData["ResendStatus"] = "sent";
        }
        catch (Exception)
        {
            _logger.LogError("Confirmation email resend preparation or submission failed.");
            TempData["ResendStatus"] = "failed";
        }

        return RedirectToPage("RegisterConfirmation", new { email, returnUrl = localReturnUrl });
    }

    private string GetLocalReturnUrl(string returnUrl) =>
        !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? returnUrl
            : Url.Content("~/");
}
