using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Stores
{
    public class AccountStore
    {
        private Vet _account;
        public Vet CurrentAccount { get { return _account; } set { _account = value; } }
        public AccountStore() 
        {
        }
    }
}
