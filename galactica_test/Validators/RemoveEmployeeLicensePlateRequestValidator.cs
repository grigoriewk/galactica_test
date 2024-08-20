using FluentValidation;
using galactica_test.Models.Request;

namespace galactica_test.Validators;

public class RemoveEmployeeLicensePlateRequestValidator : AbstractValidator<RemoveEmployeeLicensePlateRequest>
{
    public RemoveEmployeeLicensePlateRequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(x => x.LicensePlateToRemove).NotNull().NotEmpty();
    }
}