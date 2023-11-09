using PawPatientManager.Models;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Commands
{
    public struct ManageOwnersViewModelCommands
    {
        public class AddOwner : CommandBase
        {
            private NavigationStore _navigator;
            //private NavigationBarViewModel _navigationBarVM;
            private VetSystem _vetSystem;

            public AddOwner(NavigationStore navigator, VetSystem vetSystem)
            {
                _navigator = navigator;
                _vetSystem = vetSystem;
                //_navigationBarVM = navigationBarVM;
            }
            public override void Execute(object? parameter)
            {
                _navigator.CurrentViewModel = new OwnerRegistrationViewModel(_vetSystem, _navigator);
                //_navigator.CurrentViewModel = new OwnerRegistrationViewModel(_vetSystem, _navigator, _navigationBarVM);
            }
        }
    }
}
