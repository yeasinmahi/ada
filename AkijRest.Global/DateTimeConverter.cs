using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkijRest.Global
{
    public class Datetime
    {
        public static DateTime ToDateTime(string date)
        {
            DateTime dateTime = DateTime.ParseExact
            (
                date,
                GetStringFromDateFormat(Format.dd_MM_yyyy),
                System.Globalization.CultureInfo.InvariantCulture
            );
            return dateTime;
        }
        public static DateTime ToDateTime(string date, Format format)
        {
            DateTime dateTime = DateTime.ParseExact
            (
                date,
                GetStringFromDateFormat(format),
                System.Globalization.CultureInfo.InvariantCulture
            );
            return dateTime;
        }
        public static string ToString(DateTime date)
        {
            return date.ToString(GetStringFromDateFormat(Format.dd_MM_yyyy));
        }
        public static string ToString(DateTime date, Format format)
        {
            return date.ToString(GetStringFromDateFormat(format));
        }
        public enum Format
        {
            // ReSharper disable once InconsistentNaming
            dd_MM_yyyy,
        }

        private static string GetStringFromDateFormat(Format format)
        {
            switch (format)
            {
                case Format.dd_MM_yyyy:
                    return "dd/MM/yyyy";

                    default: return "dd/MM/yyyy";
            }
        }
    }
}
