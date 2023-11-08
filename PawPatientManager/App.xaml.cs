using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PawPatientManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Owner pioter = new Owner(0, "Piotrek", "Baniak", true, DateTime.Now, "Lubliniec", "piotrek@gmail.com", "94251558235");
            Pet pet = new Pet(0, "Szarik", true, pioter, DateTime.Now, "Pies", "Owczarek Niemiecki", "997");
            pioter.AddPet(pet);

            Vet lekarz = new Vet(0, "Jarek", "Lekarek");
            Medication xanax = new Medication(0, "Xanax", "Healing strong!");
            Visit wizyta = new Visit(0, pet, lekarz, DateTime.Now, new List<Medication>() { xanax });
        }
    }
}
