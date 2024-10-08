﻿using PawPatientManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Models
{
    public class Medication
    {
        #region Fields
        private Guid _id;
        private string _name;
        private string _description;
        private int _amount;
        #endregion
        #region Properties
        public Guid ID { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public int Amount { get { return _amount; } set { _amount = value; } }
        #endregion
        #region Constructor
        public Medication(Guid id, string name, string description, int amount) 
        {
            _id = id;
            _name = name;
            _description = description;
            _amount = amount;
        }
        public Medication(MedicationDTO med)
        {
            _id = med.ID;
            _name = med.Name;
            _description = med.Description;
            _amount = med.Amount;
        }
        #endregion
    }
}
