namespace Knoex.ViewModels
{
    public abstract class BaseViewModel
    {
        public TimeZoneInfo TimeZone => TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        public string GetTimestamp(DateTime date) => TimeZoneInfo
            .ConvertTime(date, TimeZone)
            .ToString("dd-MM-yyyy HH:mm");
    }
}