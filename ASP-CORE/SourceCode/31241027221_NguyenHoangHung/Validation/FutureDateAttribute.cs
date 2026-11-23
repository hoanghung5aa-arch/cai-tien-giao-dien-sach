using System.ComponentModel.DataAnnotations;

namespace _31241027221_NguyenHoangHung.Validation;

public sealed class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is not DateTime date)
        {
            return false;
        }

        return date.Date > DateTime.Today;
    }

    public override string FormatErrorMessage(string name)
        => string.IsNullOrWhiteSpace(ErrorMessage)
            ? "Ngày phải sau hôm nay."
            : ErrorMessageString;
}
