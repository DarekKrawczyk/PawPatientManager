using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Models
{
    public class MedicalReceipt
    {
        private uint _id;
        private DateTime _signed;
        private Medication _medication;
        // Dawkowanie...

        public uint Id { get { return _id; } set { _id = value; } }
        public DateTime Signed { get { return _signed; } set { _signed = value; } }
        public Medication Medication { get { return _medication; } set { _medication = value; } }
        public MedicalReceipt(uint id, DateTime signed, Medication medication) 
        { 
            _id = id;
            _signed = signed;
            _medication = medication;
        }

    }
}
