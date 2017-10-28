using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Data;


namespace LRPBookLibrary
{
    public class DataUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeValue(string dateFormat, string dateString)
        {
            DateTime tempDate = DateTime.MinValue;

            if (dateString.Length == 0)
                return DateTime.MinValue;

            if (!DateTime.TryParseExact(dateString, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate))
            {
                return DateTime.MinValue;
            }

            return tempDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static int GetInt32Value(string intString)
        {
            int tempInt = int.MinValue;

            if( intString == null || intString.Trim() == "")
            {
                return int.MinValue;
            }
            
            if (!int.TryParse(intString, out tempInt))
            {
                return int.MinValue;
            }

            return tempInt;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static Decimal GetDecimalValue(string decimalString)
        {
            Decimal tempDecimal = Decimal.MinValue;

            if(decimalString==null || decimalString.Trim()=="")
            {
                return Decimal.MinValue;
            }

            if (decimalString.Length == 0)
                return Decimal.MinValue;

            if (!Decimal.TryParse(decimalString, out tempDecimal))
            {
                return Decimal.MinValue;
            }

            return tempDecimal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static Double GetDoubleValue(string doubleString)
        {
            Double tempDouble = Double.MinValue;

            if (doubleString.Length == 0)
                return Double.MinValue;

            if (!Double.TryParse(doubleString, out tempDouble))
            {
                return Double.MinValue;
            }

            return tempDouble;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static string GetTimeValue(string timeFormat, string timeString)
        {
            DateTime tempDate = DateTime.MinValue;

            if (timeString.Length == 0)
                return "";

            if (!DateTime.TryParseExact(timeString, timeFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate))
            {
                return "";
            }

            return tempDate.ToLongTimeString();
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetString(string str)
        {
            if (str == null) return "";
            return str;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetString(object str)
        {
            if (str == null) return "";

            return str.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool GetBooleanString(string booleanString)
        {
            if(booleanString==null)
            {
                return false;

            }
            if(booleanString!=null) { 
                booleanString = booleanString.Trim().ToLower();
            }

            if (booleanString.Trim().ToLower() == "yes" || booleanString.Trim().ToLower() == "y" || booleanString.Trim().ToLower() == "true")
            {
                return true;
            }
            else if (booleanString.Trim().ToLower() == "no" || booleanString.Trim().ToLower() == "n" || booleanString.Trim().ToLower() == "false")
            {
                return false;
            }

            return false;
        }
    }
}
