using FluentValidation;
using galactica_test.Models.Request;
using galactica_test.Utils;

namespace galactica_test.Validators;

public class CreateEmployeeLicensePlateRequestValidator : AbstractValidator<CreateEmployeeLicensePlateRequest>
{
    public CreateEmployeeLicensePlateRequestValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(x => x.NewLicensePlate).NotNull().Matches(Constants.RussianLicensePlateRegex);
    }
}