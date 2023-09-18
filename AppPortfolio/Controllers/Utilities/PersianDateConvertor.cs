using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AppPortfolio.Controllers {

    public struct PersianTime {

        public PersianTime(DateTime dt) {
            var pc = new PersianCalendar();
            Year = pc.GetYear(dt);
            Month = pc.GetMonth(dt);
            Day = pc.GetDayOfMonth(dt);
            Hour = pc.GetHour(dt);
            Minute = pc.GetMinute(dt);
            Second = pc.GetSecond(dt);
        }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

        public string ToLongDateTime() {
            return string.Format("{0}/{1}/{2} {3}:{4}:{5}", 
                Year.ToString("0000"), 
                Month.ToString("00"), 
                Day.ToString("00"), 
                Hour.ToString("00"), 
                Minute.ToString("00"),
                Second.ToString("00"));
        }

        public string ToShortDate() {
            return string.Format("{0}/{1}/{2}", 
                Year.ToString("0000"), 
                Month.ToString("00"), 
                Day.ToString("00"));
        }

        public static DateTime GetMiladi(int Year, int Month, int Day) {
            var pc = new PersianCalendar();
            return pc.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
        }
    }

    public static class PDC {
        public static PersianTime ToFullPersianDate(this DateTime dt) {
            return new PersianTime(dt);
        }

        private static string GetPersianMonthName(int month) {
            switch (month) {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date">Format must be yyyy/MM/dd</param>
        /// <returns></returns>
        public static string GetPersinaDate(string date) {
            var regex = new Regex(@"\d{4}/\d{2}/\d{2}");
            if (!regex.IsMatch(date)) throw new FormatException();
            string[] date_parts = date.Split('/');
            date_parts[1] = GetPersianMonthName(Convert.ToInt32(date_parts[1]));
            var sb = new StringBuilder().Append(date_parts[2])
                .Append(" ")
                .Append(date_parts[1])
                .Append(" ")
                .Append(date_parts[0]);
            return sb.ToString();
        }

        public static string GetPersinaDateYearMonth(string date) {
            var regex = new Regex(@"\d{4}/\d{2}");
            if (!regex.IsMatch(date)) throw new FormatException();
            string[] date_parts = date.Split('/');
            date_parts[1] = GetPersianMonthName(Convert.ToInt32(date_parts[1]));
            var sb = new StringBuilder().Append(date_parts[1])
                .Append(" ")
                .Append(date_parts[0]);
            return sb.ToString();
        }
    }
}