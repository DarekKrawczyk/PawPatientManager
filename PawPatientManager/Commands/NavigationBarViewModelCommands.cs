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

    public class NavigateCommandParam<TParameter, TViewModel> : CommandBase
    where TViewModel : ViewModelBase
    {
        private LayoutNavigationServiceParam<TParameter, TViewModel> _navigationService;
        private TParameter _parameter;
        public NavigateCommandParam(LayoutNavigationServiceParam<TParameter, TViewModel> navigationService, TParameter tparameter)
        {
            _navigationService = navigationService;
            _parameter = tparameter;
        }
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate(_parameter);
        }
    }

}
