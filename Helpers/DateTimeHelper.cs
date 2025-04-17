namespace Farmify_Api.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetPhilippineStandardTime()
        {
            var phpTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

            DateTime utcNow = DateTime.UtcNow;
            DateTime phpTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, phpTimeZone);

            return phpTime;
        } 
    }
}
