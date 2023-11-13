using PawPatientManager.Commands;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        #region Fields
       // private NavigationStore _navigator;
        private INavigationService<HomeViewModel> _navHomeService;
        private INavigationService<ManageOwnersViewModel> _navManageOwnersService;
        private INavigationService<LoginViewModel> _navLoginService;
        private VetSystem _vetSystem;
        #endregion
        #region Commands
        public ICommand CommandNavigateHome { get; }
        public ICommand CommandNavigateOwners { get; }
        public ICommand CommandNavigatePets { get; }
        public ICommand CommandNavigateVisits { get; }
        public ICommand CommandNavigateMeds { get; }
        public ICommand CommandLogout { get; }
        #endregion
        #region Constructor
        public NavigationBarViewModel(VetSystem vetSystem, INavigationService<HomeViewModel> navHomeSercice, INavigationService<ManageOwnersViewModel> navManageOwnersService,
            INavigationService<LoginViewModel> navLoginService) 
        {
            _navLoginService = navLoginService;
            _navHomeService = navHomeSercice;
            _navManageOwnersService = navManageOwnersService;
            _vetSystem = vetSystem;
            //CommandNavigateHome = new NavigationService<HomeViewModel>(_navigator, () => new HomeViewModel());
            CommandNavigateHome = new NavigateCommand<HomeViewModel>(_navHomeService);
            //CommandNavigateHome = new NavigateCommand<HomeViewModel>(new NavigationService<HomeViewModel>(_navigator, () => new HomeViewModel()));
            CommandNavigateOwners = new NavigateCommand<ManageOwnersViewModel>(_navManageOwnersService);
            CommandLogout = new NavigateCommand<LoginViewModel>(_navLoginService);
            //CommandNavigateOwners = new NavigateCommand<ManageOwnersViewModel>(new NavigationService<ManageOwnersViewModel>(_navigator, () => new ManageOwnersViewModel(_navigator, _vetSystem)));
            //CommandNavigatePets = new NavigateCommand<HomeViewModel>(_navigator, () => new HomeViewModel());
            //CommandNavigateVisits = new NavigateCommand<HomeViewModel>(_navigator, () => new HomeViewModel());
            //CommandNavigateMeds = new NavigateCommand<HomeViewModel>(_navigator, () => new HomeViewModel());
        }
        #endregion
    }
}
