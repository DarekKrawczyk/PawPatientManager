using PawPatientManager.Models;
using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //private NavigationBarViewModel _navigationBarVM;
        private NavigationStore _navigator;
        private VetSystem _vetSystem;
        public ViewModelBase CurrentViewModel {  get { return _navigator.CurrentViewModel; } }
        public MainViewModel(VetSystem vetSystem, NavigationStore navigator) 
        {
            //_navigationBarVM = navigationBarVM;
            _vetSystem = vetSystem;
            _navigator = navigator;

            /*  This object is subscribint to *CurrentViewModelChanged* event, and everytime this
             *  event will be fired, *_navigator_CurrentViewModelChanged()* function will be called.
             */
            _navigator.CurrentViewModelChanged += _navigator_CurrentViewModelChanged;
        }

        private void _navigator_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
