﻿using PawPatientManager.Commands;
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
        private INavigationService<PetsViewModel> _navPetsVMService;
        private INavigationService<VisitsViewModel> _navVisitsVMService;
        private INavigationService<MedsViewModel> _navMedsVMService;
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
            INavigationService<LoginViewModel> navLoginService, INavigationService<PetsViewModel> navPetsVMService, INavigationService<VisitsViewModel> navVisitsVMService,
            INavigationService<MedsViewModel> navMedsVMService) 
        {
            _navLoginService = navLoginService;
            _navHomeService = navHomeSercice;
            _navManageOwnersService = navManageOwnersService;
            _navPetsVMService = navPetsVMService;
            _navVisitsVMService = navVisitsVMService;
            _navMedsVMService = navMedsVMService;
            _vetSystem = vetSystem;

            CommandNavigateHome = new NavigateCommand<HomeViewModel>(_navHomeService);
            CommandNavigateOwners = new NavigateCommand<ManageOwnersViewModel>(_navManageOwnersService);
            CommandNavigatePets = new NavigateCommand<PetsViewModel>(_navPetsVMService);
            CommandNavigateVisits = new NavigateCommand<VisitsViewModel>(_navVisitsVMService);
            CommandNavigateMeds = new NavigateCommand<MedsViewModel>(_navMedsVMService);
            CommandLogout = new NavigateCommand<LoginViewModel>(_navLoginService);
        }
        #endregion
    }
}
