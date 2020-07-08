﻿using System.Text.RegularExpressions;

namespace RentCars
{
	public static class CheckStringFormat
	{
		public static bool IsBase64String(string s)
		{
			s = s.Trim();
			return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
		}
	}
}
