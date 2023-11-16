using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Models
{
    public class Medication
    {
        private Guid _id;
        private string _name;
        private string _description;
        private int _amount;
        public Guid ID { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public int Amount { get { return _amount; } set { _amount = value; } }
        public Medication(Guid id, string name, string description, int amount) 
        {
            _id = id;
            _name = name;
            _description = description;
            _amount = amount;
        }
        public bool Conflicts(Medication other)
        {
            if (_name == other.Name && _description == other.Description && _amount == other.Amount) return true;
            else return false;
        }
    }
}
