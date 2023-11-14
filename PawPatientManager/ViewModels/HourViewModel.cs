using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class HourViewModel
    {
        private string _hour;
        public string Hour { get { return _hour; } set { _hour = value; } }
        public static IEnumerable<HourViewModel> GenerateHours()
        {
            IEnumerable<HourViewModel> hours = new List<HourViewModel>()
                {
                    new HourViewModel() { Hour = "7:00"},
                    new HourViewModel() { Hour = "7:30"},
                    new HourViewModel() { Hour = "8:00"},
                    new HourViewModel() { Hour = "8:30"},
                    new HourViewModel() { Hour = "9:00"},
                    new HourViewModel() { Hour = "9:30"},
                    new HourViewModel() { Hour = "10:00"},
                    new HourViewModel() { Hour = "10:30"},
                    new HourViewModel() { Hour = "11:00"},
                    new HourViewModel() { Hour = "11:30"},
                    new HourViewModel() { Hour = "12:00"},
                    new HourViewModel() { Hour = "12:30"},
                    new HourViewModel() { Hour = "13:00"},
                    new HourViewModel() { Hour = "13:30"},
                    new HourViewModel() { Hour = "14:00"},
                    new HourViewModel() { Hour = "14:30"},
                    new HourViewModel() { Hour = "15:00"},
                    new HourViewModel() { Hour = "15:30"},
                    new HourViewModel() { Hour = "16:00"},
                };
            return hours;
        }
    }
}
