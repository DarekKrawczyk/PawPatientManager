using PawPatientManager.Models;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Commands
{
    public struct OwnerRegistratorViewModelCommands
    {
        // TODO: some kind of magic for initializing all of the commands within this structure.

        public class ClearData : CommandBase
        {
            private OwnerRegistrationViewModel _ownerRegistrationModel;
            public ClearData(OwnerRegistrationViewModel ownerRegistrationModel)
            {
                _ownerRegistrationModel = ownerRegistrationModel;
            }
            public override void Execute(object? parameter)
            {
                _ownerRegistrationModel.Name = string.Empty;
                _ownerRegistrationModel.Surname = string.Empty;
                _ownerRegistrationModel.Gender = false;
                _ownerRegistrationModel.BirthDate = DateTime.Now;
                _ownerRegistrationModel.Adress = string.Empty;
                _ownerRegistrationModel.PhoneNumber = string.Empty;
                _ownerRegistrationModel.Email = string.Empty;
                _ownerRegistrationModel.PESEL = string.Empty;
            }
        }
        public class RegisterOwner : CommandBase
        {
            private static uint _idIterator;
            private VetSystem _vetSystem;
            private OwnerRegistrationViewModel _ownerRegistrationViewModel;
            public RegisterOwner(VetSystem vetSystem, OwnerRegistrationViewModel ownerRegistrationViewModel)
            {
                /*  We need reference to VetSystem because any operation requires access methods/fields
                 *  of out system to change/modify it. *ViewModelBase* is required because the data from
                 *  which for example *Owner* will be created is located there.
                 */
                _vetSystem = vetSystem;
                _ownerRegistrationViewModel = ownerRegistrationViewModel;
                _ownerRegistrationViewModel.PropertyChanged += _ownerRegistrationViewModel_PropertyChanged;
            }

            private void _ownerRegistrationViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                /*  Walk through - in line *_ownerRegistrationViewModel.PropertyChanged += _ownerRegistrationViewModel_PropertyChanged;*
                 *  we are subscribing to event *PropertyChanged* in *OwnerRegistrationViewModel* (BaseClass) wchich is called whenever
                 *  any property in UI has changed. So whenever it changes this method is launched. Next we check if *PropertyName*
                 *  that has changed is called like *OwnerRegistrationViewModel.Name* because we are trying to determine, did
                 *  *Name* changed. (We dont want to call this function on every property change...). If it is this property, then 
                 *  *OnCanExecutedChange()* function is called which causes *CanExecute()* event ti fire?? not sure.
                 */
                if (e.PropertyName == nameof(OwnerRegistrationViewModel.Name))
                {
                    OnCanExecutedChange();
                }
            }

            public override bool CanExecute(object? parameter)
            {
                return !string.IsNullOrEmpty(_ownerRegistrationViewModel.Name) && base.CanExecute(parameter);
            }

            public override void Execute(object? parameter)
            {
                Owner newOwner = new Owner(_idIterator++,
                    _ownerRegistrationViewModel.Name,
                    _ownerRegistrationViewModel.Surname,
                    _ownerRegistrationViewModel.Gender,
                    _ownerRegistrationViewModel.BirthDate,
                    _ownerRegistrationViewModel.Adress,
                    _ownerRegistrationViewModel.PhoneNumber,
                    _ownerRegistrationViewModel.Email,
                    _ownerRegistrationViewModel.PESEL
                    );
                _vetSystem.AddOwner(newOwner);

                // TODO: if sucess -> clear data and messagebox; else messagebox?
            }
        }
    }
}
