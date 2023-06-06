// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Globalization;

namespace dotnet_date_format
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"CultureInfo.CurrentCulture = {CultureInfo.CurrentCulture}");
            Console.WriteLine($"CultureInfo.CurrentCulture.CompareInfo.Version.FullVersion = {CultureInfo.CurrentCulture.CompareInfo.Version.FullVersion}");
            Console.WriteLine($"CultureInfo.CurrentCulture.CompareInfo.Version.SortId = {CultureInfo.CurrentCulture.CompareInfo.Version.SortId}");

            Console.WriteLine($"CultureInfo.InvariantCulture.CompareInfo.Version.FullVersion = {CultureInfo.InvariantCulture.CompareInfo.Version.FullVersion}");
            Console.WriteLine($"CultureInfo.InvariantCulture.CompareInfo.Version.SortId = {CultureInfo.InvariantCulture.CompareInfo.Version.SortId}");
            
            Console.WriteLine($"ICUMode() == {ICUMode()}");

            Console.WriteLine($"DateTime.Today = {DateTime.Today}");
            Console.WriteLine($"DateTime.Today.ToShortDateString() = {DateTime.Today.ToShortDateString()}");

            var dtf = CultureInfo.CurrentCulture.DateTimeFormat;
            var dtfi = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentCulture);

            Console.WriteLine($"ShortDatePattern = {dtf.ShortDatePattern}");
            Console.WriteLine($"FullDateTimePattern = {dtf.FullDateTimePattern}");
            Console.WriteLine($"AbbreviatedMonthNames = {string.Join(",", dtf.AbbreviatedMonthNames)}");
            Console.WriteLine($"AbbreviatedMonthGenitiveNames = {string.Join(",", dtf.AbbreviatedMonthGenitiveNames)}");

            var property = typeof(DateTimeFormatInfo).GetProperty("FormatFlags", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Console.WriteLine($"property.Value = {property.GetValue(dtfi)}");

            foreach (var format in new string[] { "dd-MMM-yyyy", "dd-MMMM-yyyy" })
            {
                Console.WriteLine($"format = {format} ==> {DateTime.Today.ToString(format)}");
            }

            for (int i = 1; i <= 12; i++)
            {
                var date = new DateTime(2023, i, 1);

                Console.WriteLine($"date = {date}\tMMM = {date.ToString("MMM")}\tdd-MMM-yyyy = {date.ToString("dd-MMM-yyyy")}\tdd-MMMM-yyyy = {date.ToString("dd-MMMM-yyyy")}");
            }
        }

        public static bool ICUMode()
        {
            SortVersion sortVersion = CultureInfo.InvariantCulture.CompareInfo.Version;
            byte[] bytes = sortVersion.SortId.ToByteArray();
            int version = bytes[3] << 24 | bytes[2] << 16 | bytes[1] << 8 | bytes[0];
            return version != 0 && version == sortVersion.FullVersion;
        }
    }
}