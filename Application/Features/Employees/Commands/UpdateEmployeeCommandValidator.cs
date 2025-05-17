using FluentValidation;

namespace Application.Features.Employees.Commands
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not a valid email address.");
            RuleFor(e => e.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than 0.");
            RuleFor(e => e.Position)
                .IsInEnum().WithMessage("Invalid job position.");
            RuleFor(e => e.DepartmentId)
                .GreaterThan(0).WithMessage("Department ID must be greater than 0.");
        }
    }
}
