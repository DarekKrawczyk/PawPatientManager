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
        private INavigationService<LoginViewModel> _firstNavService;

        public App()
        {
            _vetSystem = new VetSystem();

            _vetSystem.Pets.Add(new Pet(0, "Bolek", true, null, DateTime.Now, "Dog", "German shepard", "9293492394"));
            _vetSystem.Pets.Add(new Pet(1, "Taciek", true, null, DateTime.Now, "Cat", "Dachowiec", "23452345"));
            _vetSystem.Pets.Add(new Pet(2, "Masny", true, null, DateTime.Now, "Frog", "Green frog", "2323465216"));
            _vetSystem.Pets.Add(new Pet(3, "Bogol", true, null, DateTime.Now, "Horse", "Big horse", "3644363245"));

            _vetSystem.Owners.Add(new Owner(0, "Mariusz", "Pudzianowski", true, DateTime.Now, "Gliwice ul.Pszczyńska 23", "+48424525252", "mariusz.pudzian@gmail.com", "9923523582385"));

            _navigationStore = new NavigationStore();
            _accountStore = new AccountStore();

            _firstNavService = CreateLoginNavService();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _firstNavService.Navigate();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_vetSystem, _navigationStore)
            };

            MainWindow.Show();
        }
        #region Factories - Owner
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
        #endregion
        #region Factories - Pet
        private INavigationService<RegisterPetViewModel> CreateRegisterPetNavService()
        {
            return new LayoutNavigationService<RegisterPetViewModel>(
                _navigationStore,
                () => new RegisterPetViewModel(
                    _vetSystem,
                    CreatePetsNavService()
                    ),
                CreateNavBarVM);
        }
        private INavigationService<PetsViewModel> CreatePetsNavService()
        {
            return new LayoutNavigationService<PetsViewModel>(
                _navigationStore,
                () => new PetsViewModel(
                    _vetSystem,
                    CreateRegisterPetNavService(),
                    CreateEditPetNavService()
                    ),
                CreateNavBarVM
                );
        }

        private LayoutNavigationServiceParam<PetViewModel, EditPetViewModel> CreateEditPetNavService()
        {
            return new LayoutNavigationServiceParam<PetViewModel, EditPetViewModel>(
                _navigationStore,
                (paramater) => new EditPetViewModel(
                    _vetSystem,
                    paramater,
                    CreatePetsNavService()
                    ),
                CreateNavBarVM);
        }        
        #endregion
        #region Factories - Home
        private INavigationService<HomeViewModel> CreateHomeNavService()
        {
            return new LayoutNavigationService<HomeViewModel>(
                _navigationStore,
                () => new HomeViewModel(_accountStore),
                CreateNavBarVM);
        }
        #endregion
        #region Factories - Login
        private INavigationService<LoginViewModel> CreateLoginNavService()
        {
            return new NavigationService<LoginViewModel>(
                _navigationStore,
                () => new LoginViewModel(_accountStore, CreateHomeNavService()));
        }
        #endregion
        #region Factories - Navigation bar
        private NavigationBarViewModel CreateNavBarVM()
        {
            return new NavigationBarViewModel(_vetSystem,
                CreateHomeNavService(),
                CreateManageOwnersNavService(),
                CreateLoginNavService(),
                CreatePetsNavService()
                );
        }
        #endregion
    }
}
