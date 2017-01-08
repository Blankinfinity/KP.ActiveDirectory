using System;
using System.Text;

namespace KP.ActiveDirectory
{
    class ADDateConversion
    {
        /// <summary>
        /// Method that formats a date in the required format
        /// needed (AAAAMMDDMMSSSS.0Z) to compare dates in AD.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Date in valid format for AD</returns>
        public string ToADDateString(DateTime date)
        {
            string year = date.Year.ToString();
            int month = date.Month;
            int day = date.Day;
            StringBuilder sb = new StringBuilder();
            sb.Append(year);
            if (month < 10)
            {
                sb.Append("0");
            }
            sb.Append(month.ToString());
            if (day < 10)
            {
                sb.Append("0");
            }
            sb.Append(day.ToString());
            sb.Append("000000.0Z");
            return sb.ToString();
        }
    }
}
