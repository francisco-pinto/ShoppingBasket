using Application.Utils;

namespace Infrastructure;

public class Clock : IClock
{
    public DateTime Now => DateTime.UtcNow;
}