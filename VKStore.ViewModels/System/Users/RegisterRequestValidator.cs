using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.System.Users
{
    public class RegisterRequestValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Họ và tên không được trống");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email không được trống")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email chưa đúng định dạng");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại không được trống");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Tài khoản không được trống");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Mật khẩu không được trống")
                .MinimumLength(6).WithMessage("Mật khẩu phải ít nhất 6 ký tự");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Nhập lại mật khẩu không đúng");
                }
            });
        }
    }
}
