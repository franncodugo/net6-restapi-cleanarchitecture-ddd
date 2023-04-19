using ErrorOr;

namespace DinnerRes.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Authentication.Invalid",
            description:"Invalid"
        );
    }
}