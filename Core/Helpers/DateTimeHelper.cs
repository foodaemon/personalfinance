using System;

namespace Core.Helpers
{
	public static class DateTimeHelper
	{
		public static DateTime StartOfMonth(int year, int month)
		{
			var startOfMonth = new DateTime(year, month, 1);
			return startOfMonth;
		}

		public static DateTime EndOfMonth(int year, int month)
		{
			var endOfMonth = new DateTime (year, month, DateTime.DaysInMonth (year, month));
			return endOfMonth;
		}
	} // class
} // namespace