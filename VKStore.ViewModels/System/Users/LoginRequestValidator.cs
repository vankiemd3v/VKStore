using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.System.Users
{
    public class LoginRequestValidator: AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Nhập tài khoản");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Nhập mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự");
        }
    }
}
