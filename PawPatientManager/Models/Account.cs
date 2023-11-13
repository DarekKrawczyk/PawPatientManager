using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Models
{
    public class Account
    {
        private string _username;
        private string _password;
        public string Username { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public Account(string username, string password) 
        {
            _username = username;
            _password = password;
        }
        public bool IsValid()
        {
            //if (_username == null && _password == null) return true;
            //else return false;
            return true;
        }
    }
}
