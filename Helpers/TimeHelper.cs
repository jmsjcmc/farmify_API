namespace Farmify_Api.Helpers
{
    public static class TimeHelper
    {
        public static DateTime getphilippinestandardtime()
        {
            var phptimezone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            DateTime utcnow = DateTime.UtcNow;
            DateTime phptime = TimeZoneInfo.ConvertTimeFromUtc(utcnow, phptimezone);
            return phptime;
        }
    }
}
