namespace Mcm.Interactions.Domain.Extensions
{
    public static class DateTimeExtension
    {
        extension(DateTime? date)
        {
            public string GetHour()
            {
                return date?.ToString("HH:mm") ?? string.Empty;
            }

            public string GetDate() => date?.ToString("yyyy-MM-dd") ?? string.Empty;
        }
        extension(DateTime date)
        {
            public string GetHour()
            {
                return date.ToString("HH:mm");
            }

            public string GetDate() => date.ToString("yyyy-MM-dd");
        }
    } 
}