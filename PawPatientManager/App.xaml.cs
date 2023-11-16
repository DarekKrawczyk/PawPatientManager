using Microsoft.EntityFrameworkCore;
using PawPatientManager.Components;
using PawPatientManager.DbContexts;
using PawPatientManager.Models;
using PawPatientManager.Services;
using PawPatientManager.Services.MedicationConflicters;
using PawPatientManager.Services.MedicationCreators;
using PawPatientManager.Services.MedicationProviders;
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
        private static string ConnectionString = "Data Source=medication.db";
        private AccountStore _accountStore;
        private VetSystem _vetSystem;
        private NavigationStore _navigationStore;
        private INavigationService<LoginViewModel> _firstNavService;
        #region Fields - Database
        private IMedicationProvider _medicationProvider;
        private IMedicationCreator _medicationCreator;
        private IMedicationConflicter _medicationConflicter;
        private MedicationDbContextFactory _medicationDbContextFactory;
        #endregion

        public App()
        {
            _medicationDbContextFactory = new MedicationDbContextFactory(ConnectionString);
            _medicationCreator = new DatabaseMedicationCreator(_medicationDbContextFactory);
            _medicationProvider = new DatabaseMedicationProvider(_medicationDbContextFactory);
            _medicationConflicter = new DatabaseMedicationConflicter(_medicationDbContextFactory);

            _vetSystem = new VetSystem(_medicationProvider, _medicationCreator, _medicationConflicter);

            _vetSystem.Owners.Add(new Owner(0, "Mariusz", "Pudzianowski", true, DateTime.Now, "Gliwice ul.Pszczyńska 23", "+48424525252", "mariusz.pudzian@gmail.com", "9923523582385"));

            _vetSystem.Pets.Add(new Pet(0, "Bolek", true, _vetSystem.Owners[0], DateTime.Now, "Dog", "German shepard", "9293492394"));
            _vetSystem.Pets.Add(new Pet(1, "Taciek", true, _vetSystem.Owners[0], DateTime.Now, "Cat", "Dachowiec", "23452345"));
            _vetSystem.Pets.Add(new Pet(2, "Masny", true, _vetSystem.Owners[0], DateTime.Now, "Frog", "Green frog", "2323465216"));
            _vetSystem.Pets.Add(new Pet(3, "Bogol", true, _vetSystem.Owners[0], DateTime.Now, "Horse", "Big horse", "3644363245"));

            _vetSystem.Vets.Add(new Vet(0, "Jarek", "Marek"));

            _vetSystem.Visits.Add(new Visit(0, _vetSystem.Pets[0], _vetSystem.Vets[0], DateTime.Now, null));

            //_vetSystem.Meds.Add(new Medication(0, "Majeranek", "Na ból głowy", 20));
            //_vetSystem.Meds.Add(new Medication(1, "XANAX", "Go sleep bonobo", 2137));
            //_vetSystem.Meds.Add(new Medication(2, "Pawulonix", "O_O", 33));
            //_vetSystem.Meds.Add(new Medication(3, "APAP", "Na ból dupy", 40));
            //_vetSystem.Meds.Add(new Medication(4, "Witamina C", "Be healthy bro", 204));

            _navigationStore = new NavigationStore();
            _accountStore = new AccountStore();

            _firstNavService = CreateLoginNavService();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(ConnectionString).Options;
            using (MedicationDbContext medsDbContext = new MedicationDbContext(options))
            {
                medsDbContext.Database.Migrate();
            };


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
        #region Factories - Visits
        private INavigationService<RegisterVisitViewModel> CreateRegisterVisitNavService()
        {
            return new LayoutNavigationService<RegisterVisitViewModel>(
                _navigationStore,
                () => new RegisterVisitViewModel(
                    _vetSystem,
                    CreateVisitsNavService()
                    ),
                CreateNavBarVM);
        }
        private INavigationService<VisitsViewModel> CreateVisitsNavService()
        {
            return new LayoutNavigationService<VisitsViewModel>(
                _navigationStore,
                () => new VisitsViewModel(
                    _vetSystem,
                    CreateRegisterVisitNavService(),
                    CreateEditVisitNavService()
                    ),
                CreateNavBarVM
                );
        }

        private LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel> CreateEditVisitNavService()
        {
            return new LayoutNavigationServiceParam<VisitViewModel, EditVisitViewModel>(
                _navigationStore,
                (paramater) => new EditVisitViewModel(
                    _vetSystem,
                    paramater,
                    CreateVisitsNavService()
                    ),
                CreateNavBarVM);
        }
        #endregion
        #region Factories - Meds
        private INavigationService<MedsViewModel> CreateMedsNavService()
        {
            return new LayoutNavigationService<MedsViewModel>(
                _navigationStore,
                () => MedsViewModel.LoadMedsViewModel(_vetSystem),
                CreateNavBarVM);
            //return new LayoutNavigationService<MedsViewModel>(
            //    _navigationStore,
            //    () => new MedsViewModel(
            //        _vetSystem
            //        ),
            //    CreateNavBarVM);
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
                CreatePetsNavService(),
                CreateVisitsNavService(),
                CreateMedsNavService()
                );
        }
        #endregion
    }
}
