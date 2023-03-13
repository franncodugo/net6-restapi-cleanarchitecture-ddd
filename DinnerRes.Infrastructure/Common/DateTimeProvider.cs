using DinnerRes.Application.Common.Interfaces;

namespace DinnerRes.Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}