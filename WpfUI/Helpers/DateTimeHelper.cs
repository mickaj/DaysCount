﻿using System;

namespace WpfUI.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetNowDateOnly()
        {
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day);
        }

        public static int GetRemainingDays(DateTime eventDate)
        {
            return (eventDate - GetNowDateOnly()).Days;
        }
    }
}
