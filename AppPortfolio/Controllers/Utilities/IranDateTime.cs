using System;

namespace AppPortfolio.Controllers.Utilities {
    public class IranDateTime {
        public static DateTime Now {
            get {
                return GetDate();
            }
        }

        private static DateTime GetDate() {
            var iran = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, iran);
        }
    }
}