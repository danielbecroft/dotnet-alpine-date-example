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
            Console.WriteLine($"DateTime.Today = {DateTime.Today}");

            var dtf = CultureInfo.CurrentCulture.DateTimeFormat;
            var dtfi = DateTimeFormatInfo.GetInstance(CultureInfo.CurrentCulture);

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
                Console.WriteLine($"date = {date}, dd-MMM-yyyy = {date.ToString("dd-MMM-yyyy")}, dd-MMMM-yyyy = {date.ToString("dd-MMMM-yyyy")}");
            }

            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures).Where(c => c.TwoLetterISOLanguageName == "en"))
            {
                var a = culture.DateTimeFormat.AbbreviatedMonthNames;
                var b = culture.DateTimeFormat.AbbreviatedMonthGenitiveNames;

                if (a.Any(_ => !b.Contains(_)) || b.Any(_ => !a.Contains(_)))
                {
                    Console.WriteLine($"culture == {culture}");
                    Console.WriteLine($"\t AbbreviatedMonthNames => {string.Join(',', a)}");
                    Console.WriteLine($"\t AbbreviatedMonthGenitiveNames => {string.Join(',', b)}");
                }
                else
                {
                    Console.WriteLine($"culture == {culture} matches.");
                }

            }
        }
    }
}