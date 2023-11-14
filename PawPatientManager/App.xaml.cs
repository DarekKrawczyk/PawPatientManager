using PawPatientManager.Components;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Stores;
using PawPatientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace PawPatientManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AccountStore _accountStore;
        private VetSystem _vetSystem;
        private NavigationStore _navigationStore;
        //private NavigationBarViewModel _navigationBarVM;
        private INavigationService<LoginViewModel> _firstNavService;

        public App()
        {
            _vetSystem = new VetSystem();
            _navigationStore = new NavigationStore();
            _accountStore = new AccountStore();
            //_navigationBarVM = new NavigationBarViewModel(_vetSystem, 
            //    CreateHomeNavService(),
            //    CreateManageOwnersNavService());
            _firstNavService = CreateLoginNavService();
            //_homeNavService = new LayoutNavigationService<HomeViewModel>(_navigationStore, ()=>new HomeViewModel(), CreateNavBarVM);

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //INavigationService<OwnerRegistrationViewModel> navOwnerRegister = new LayoutNavigationService<OwnerRegistrationViewModel>(navigationStore, () => new OwnerRegistrationViewModel(vetSystem, navigationStore));
            //INavigationService<ManageOwnersViewModel> navService = new LayoutNavigationService<ManageOwnersViewModel>(navigationStore, () => new ManageOwnersViewModel(navigationStore, vetSystem));

            _firstNavService.Navigate();

            //navigationStore.CurrentViewModel = new HomeViewModel();
            //navigationStore.CurrentViewModel = new HomeViewModel(navigationBarVM);
            //navigationStore.CurrentViewModel = new ManageOwnersViewModel(navigationStore, vetSystem, navigationBarVM);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_vetSystem, _navigationStore)
                //DataContext = new MainViewModel(vetSystem, navigationStore, navigationBarVM)
            };

            MainWindow.Show();
        }
        private INavigationService<HomeViewModel> CreateHomeNavService()
        {
            return new LayoutNavigationService<HomeViewModel>(
                _navigationStore,
                () => new HomeViewModel(_accountStore),
                CreateNavBarVM);
        }
        private INavigationService<OwnerRegistrationViewModel> CreateOwnerRegisterService()
        {
            return new LayoutNavigationService<OwnerRegistrationViewModel>(
                _navigationStore,
                () => new OwnerRegistrationViewModel(
                    _vetSystem,
                    CreateManageOwnersNavService()),
                CreateNavBarVM);
        }
        private INavigationService<ManageOwnersViewModel> CreateManageOwnersNavService()
        {
            return new LayoutNavigationService<ManageOwnersViewModel>(
                _navigationStore,
                () => new ManageOwnersViewModel(
                    _vetSystem,
                    CreateOwnerRegisterService(),
                    CreateEditOwnerNavService()
                    ),
                CreateNavBarVM);
        }
        private LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel> CreateEditOwnerNavService()
        {
            return new LayoutNavigationServiceParam<OwnerViewModel, EditOwnerViewModel>(
                _navigationStore,
                (paramater) => new EditOwnerViewModel(
                    _vetSystem,
                    paramater,
                    CreateManageOwnersNavService()
                    ),
                CreateNavBarVM);
        }

        private INavigationService<LoginViewModel> CreateLoginNavService()
        {
            return new NavigationService<LoginViewModel>(
                _navigationStore,
                () => new LoginViewModel(_accountStore, CreateHomeNavService()));
        }
        private NavigationBarViewModel CreateNavBarVM()
        {
            return new NavigationBarViewModel(_vetSystem,
                CreateHomeNavService(),
                CreateManageOwnersNavService(),
                CreateLoginNavService()
                );
        }
    }
}
