using FluentValidation;
using galactica_test.Models.Request;
using galactica_test.Utils;

namespace galactica_test.Validators;

public class EditEmployeeCarRequestValidator : AbstractValidator<EditEmployeeCarRequest>
{
    public EditEmployeeCarRequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().GreaterThan(0);
        RuleFor(x => x.NewLicensePlate)
            .NotNull()
            .Matches(Constants.RussianLicensePlateRegex)
            .When(x => string.IsNullOrEmpty(x.OldLicensePlate));
        
        RuleFor(x => x.OldLicensePlate)
            .NotNull()
            .Matches(Constants.RussianLicensePlateRegex)
            .When(x => string.IsNullOrEmpty(x.NewLicensePlate));
    }
}