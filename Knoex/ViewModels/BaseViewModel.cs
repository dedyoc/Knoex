namespace Knoex.ViewModels
{
    public abstract class BaseViewModel
    {
        public TimeZoneInfo TimeZone => TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
    }
}