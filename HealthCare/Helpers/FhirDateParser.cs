using HealthCare.Helpers.Models;

namespace HealthCare.Helpers
{
    public static class FhirDateParser
    {
        private static DateTime ToUtc(DateTime dt)
        {
            return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }

        public static FhirDateRange? Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            var ops = new[] { "ge", "gt", "le", "lt", "ne", "eq" };

            string op = "eq";
            string value = input;

            foreach (var o in ops)
            {
                if (input.StartsWith(o))
                {
                    op = o;
                    value = input.Substring(o.Length);
                    break;
                }
            }

            if (!TryParseRange(value, out var from, out var to))
                return null;

            return new FhirDateRange
            {
                Operator = op,
                From = from,
                To = to
            };
        }

        private static bool TryParseRange(string input, out DateTime from, out DateTime to)
        {
            from = default;
            to = default;

            // YYYY
            if (input.Length == 4 && int.TryParse(input, out var year))
            {
                from = ToUtc(new DateTime(year, 1, 1));
                to = ToUtc(from.AddYears(1));
                return true;
            }

            // YYYY-MM
            if (input.Length == 7 && DateTime.TryParse(input + "-01", out var month))
            {
                from = ToUtc(new DateTime(month.Year, month.Month, 1));
                to = ToUtc(from.AddMonths(1));
                return true;
            }

            // YYYY-MM-DD
            if (DateTime.TryParse(input, out var day))
            {
                from = ToUtc(day.Date);
                to = ToUtc(from.AddDays(1));
                return true;
            }

            return false;
        }
    }

}
