using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Utility
{
    public class Globals
    {
        public static DateTime GetVisitDateTime(DateTime date, HourViewModel hour)
        {
            DateTime timePart = DateTime.ParseExact(hour.Hour, "H:mm", null);
            DateTime dateTime = new DateTime(date.Year, date.Month, date.Day,
                timePart.Hour, timePart.Minute, timePart.Second);
            return dateTime;
        }
    }
}
