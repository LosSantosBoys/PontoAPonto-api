namespace PontoAPonto.Domain.Helpers
{
    public static class DateHelper
    {
        public static (bool isSuccess, DateTime dateTime) ConvertStringToDateTimeddMMyyyy(string date)
        {
            if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                return (true, dateTime);
            }

            return (false, default);
        }
    }
}
