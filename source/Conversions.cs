using System;

namespace BedrockServer2000
{
	public static class Conversions
	{
		// TIme convertions
		public static int HourToMilliseconds(int hour) => hour * 3600000;
		public static int MinuteToMilliseconds(int minute) => minute * 60000;
		public static AutoBackupTimeUnit StringToAutoBackupTimeUnit(string timeUnit)
		{
			if (timeUnit.ToLower() == "minute") return AutoBackupTimeUnit.Minute;
			else if (timeUnit.ToLower() == "hour") return AutoBackupTimeUnit.Hour;
			else return AutoBackupTimeUnit.None;
		}

		// Converts backup folder formatted as "day_month_year-hour_minute_second" to DateTime value
		//! Method is obsolete since it is no longer used in the program
		public static DateTime GetBackupCreationDate(string directoryName)
		{
			string date = directoryName.Split("-", StringSplitOptions.RemoveEmptyEntries)[0];
			string time = directoryName.Split("-", StringSplitOptions.RemoveEmptyEntries)[1];

			int year = Convert.ToInt32(date.Split("_", StringSplitOptions.RemoveEmptyEntries)[2]);
			int month = Convert.ToInt32(date.Split("_", StringSplitOptions.RemoveEmptyEntries)[1]);
			int day = Convert.ToInt32(date.Split("_", StringSplitOptions.RemoveEmptyEntries)[0]);

			int hour = Convert.ToInt32(time.Split("_", StringSplitOptions.RemoveEmptyEntries)[0]);
			int minute = Convert.ToInt32(time.Split("_", StringSplitOptions.RemoveEmptyEntries)[1]);
			int second = Convert.ToInt32(time.Split("_", StringSplitOptions.RemoveEmptyEntries)[2]);

			return new DateTime(year, month, day, hour, minute, second);
		}
	}
}