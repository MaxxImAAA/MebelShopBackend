using BackMebel.Service.Dtos.UserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackMebel.Service.Tools.FluentValidation
{
    public class UserRegistrationValidation : AbstractValidator<UserRegisterForm>
    {
        public UserRegistrationValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(15)
                .Must(BeOnlyLetters).WithMessage("Имя должно содержать только буквы");
                


            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x=>x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);
        }

        private bool BeOnlyLetters(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true;
            }
            return name.All(char.IsLetter);
        }
    }
}
