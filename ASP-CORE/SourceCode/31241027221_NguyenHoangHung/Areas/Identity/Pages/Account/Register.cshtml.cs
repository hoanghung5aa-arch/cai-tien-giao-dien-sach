using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace _31241027221_NguyenHoangHung.Areas.Identity.Pages.Account;

[AllowAnonymous]
public sealed class RegisterModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(
        UserManager<IdentityUser> userManager,
        IEmailSender emailSender,
        ILogger<RegisterModel> logger)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public string ReturnUrl { get; private set; } = string.Empty;

    public sealed class InputModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email chưa đúng định dạng.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare(nameof(Password), ErrorMessage = "Hai mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public void OnGet(string returnUrl = null)
    {
        ReturnUrl = returnUrl ?? Url.Content("~/");
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        ReturnUrl = returnUrl ?? Url.Content("~/");
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var email = Input.Email.Trim();
        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, Input.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, ToVietnameseError(error));
            }

            return Page();
        }

        _logger.LogInformation("A new Identity account was created; email confirmation is required.");
        TempData["EmailDeliveryStatus"] = "failed";

        try
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedCode = WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId, code = encodedCode, returnUrl = ReturnUrl },
                protocol: Request.Scheme);

            if (string.IsNullOrWhiteSpace(callbackUrl))
            {
                throw new InvalidOperationException("Could not create the email confirmation URL.");
            }

            var encodedCallbackUrl = HtmlEncoder.Default.Encode(callbackUrl);
            var message = $"<p>Chào bạn,</p><p>Hãy xác nhận địa chỉ email để kích hoạt tài khoản Góc Phim Hưng.</p><p><a href=\"{encodedCallbackUrl}\">Xác nhận email</a></p>";
            await _emailSender.SendEmailAsync(email, "Xác nhận email - Góc Phim Hưng", message);
            TempData["EmailDeliveryStatus"] = "sent";
        }
        catch (Exception)
        {
            // The account already exists. Keep the user on a recovery path without exposing SMTP details.
            _logger.LogError("Confirmation email preparation or submission failed for a newly created account.");
        }

        return RedirectToPage("RegisterConfirmation", new { email, returnUrl = ReturnUrl });
    }

    private static string ToVietnameseError(IdentityError error) => error.Code switch
    {
        "DuplicateUserName" or "DuplicateEmail" => "Email này đã được đăng ký.",
        "PasswordTooShort" => "Mật khẩu phải có ít nhất 6 ký tự.",
        "PasswordRequiresDigit" => "Mật khẩu cần có ít nhất một chữ số.",
        "PasswordRequiresLower" => "Mật khẩu cần có ít nhất một chữ thường.",
        "PasswordRequiresUpper" => "Mật khẩu cần có ít nhất một chữ hoa.",
        "PasswordRequiresNonAlphanumeric" => "Mật khẩu cần có ít nhất một ký tự đặc biệt.",
        _ => error.Description
    };
}
