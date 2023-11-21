using PawPatientManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Models
{
    public class MedicalReceipt
    {
        private Guid _id;
        private DateTime _signed;
        private string _recommendation;
        // Dawkowanie...

        public Guid ID { get { return _id; } set { _id = value; } }
        public DateTime Signed { get { return _signed; } set { _signed = value; } }
        public string Recommendation { get { return _recommendation; } set { _recommendation = value; } }
        public MedicalReceipt(Guid id, DateTime signed, string recommendation) 
        { 
            _id = id;
            _signed = signed;
            _recommendation = recommendation;
        }        
        public MedicalReceipt(MedicalReceiptDTO receipt) 
        { 
            _id = receipt.ID;
            _signed = receipt.Signed;
            _recommendation = receipt.Recommendation;
        }

    }
}
