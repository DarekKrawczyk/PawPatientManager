using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public static bool IsNameValid(string name)
        {
            if(name == null) return false;
            return Regex.IsMatch(name, "^[a-zA-ZąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]+$"); ;
        }

        public static bool IsSurnameValid(string surname)
        {
            if (surname == null) return false;
            return Regex.IsMatch(surname, "^[a-zA-Z ]+$");
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            if (phoneNumber == null) return false;
            return Regex.IsMatch(phoneNumber, @"^\(\+\d{2}\) \d{9}$");
        }

        public static bool IsEmailValid(string email)
        {
            if (email == null) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public static bool IsPeselValid(string pesel)
        {
            if (pesel == null) return false;
            if (pesel.Length != 11)
            {
                return false;
            }

            if (!pesel.All(char.IsDigit))
            {
                return false;
            }
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3, 1 };
            int controlSum = 0;

            for (int i = 0; i < 10; i++)
            {
                controlSum += weights[i] * (pesel[i] - '0');
            }

            int controlDigit = (10 - (controlSum % 10)) % 10;

            return controlDigit == pesel[10] - '0';
        }
    }
}
