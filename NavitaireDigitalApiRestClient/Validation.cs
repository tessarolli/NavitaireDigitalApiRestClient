using System.ComponentModel.DataAnnotations;

namespace NavitaireDigitalApi;

public static class Validation
{
    public static (bool, ICollection<ValidationResult>) Validate(object input)
    {
        var validationContext = new ValidationContext(input, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(input, validationContext, validationResults, validateAllProperties: true);

        return (isValid, validationResults);
    }
}
