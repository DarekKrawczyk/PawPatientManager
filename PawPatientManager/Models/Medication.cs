using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Models
{
    public class Medication
    {
        private uint _id;
        private string _name;
        private string _description;

        public Medication(uint id, string name, string description) 
        {
            _id = id;
            _name = name;
            _description = description;
        }
    }
}
