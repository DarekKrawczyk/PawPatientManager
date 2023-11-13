using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawPatientManager.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private INavigationService<TViewModel> _navigationService;
        public NavigateCommand(INavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }

    public struct NavigationBarViewModelCommands
    {
        //public class GoHome : CommandBase
        //{
        //    private NavigationStore _navigator;
        //    //private NavigationBarViewModel _navigationBarVM;
        //    public GoHome(NavigationStore navigator)
        //    {
        //        //_navigationBarVM = navigationBarVM;
        //        _navigator = navigator;
        //    }
        //    public override void Execute(object? parameter)
        //    {
        //        _navigator.CurrentViewModel = new HomeViewModel();
        //        //_navigator.CurrentViewModel = new HomeViewModel(_navigationBarVM);
        //    }
        //}
        //public class GoOwners : CommandBase
        //{
        //    private NavigationStore _navigator;

        //    //private NavigationBarViewModel _navigationBarVM;
        //    private VetSystem _vetSystem;
        //    public GoOwners(NavigationStore navigator, VetSystem vetSystem)
        //    {
        //        _navigator = navigator;
        //        _vetSystem = vetSystem;
        //        //_navigationBarVM = navigationBarVM;
        //    }
        //    public override void Execute(object? parameter)
        //    {
        //        _navigator.CurrentViewModel = new ManageOwnersViewModel(_navigator, _vetSystem);
        //        //_navigator.CurrentViewModel = new ManageOwnersViewModel(_navigator, _vetSystem, _navigationBarVM);
        //    }
        //}
        //public class GoPets : CommandBase
        //{
        //    public override void Execute(object? parameter)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        //public class GoVisits : CommandBase
        //{
        //    public override void Execute(object? parameter)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        //public class GoMeds : CommandBase
        //{
        //    public override void Execute(object? parameter)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
