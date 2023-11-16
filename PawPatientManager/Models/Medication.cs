﻿using System;
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
        private int _amount;
        public uint ID { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public int Amount { get { return _amount; } set { _amount = value; } }
        public Medication(uint id, string name, string description, int amount) 
        {
            _id = id;
            _name = name;
            _description = description;
            _amount = amount;
        }
    }
}
