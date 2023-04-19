using ErrorOr;

namespace DinnerRes.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.Error",
            description: "Something went wrong.");
    }
}