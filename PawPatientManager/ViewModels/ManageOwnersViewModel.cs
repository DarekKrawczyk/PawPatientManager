using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawPatientManager.ViewModels
{
    public class ManageOwnersViewModel : ViewModelBase
    {
        /*  To display list of owners, list type *ObservableCollection* will be used because it can
         *  provide notifications whenever list is updated - this means whenever we add item or remove it
         *  from list, UI is going to be automatically updated.
         */
        #region 
        private ObservableCollection<OwnerViewModel> _owners;
        private VetSystem _vetSystem;
        #endregion
        #region Properties
        /*  Just a Property to get all of the owners, in this case notification is not required, 
         *  because it will be mostly used to iterate through and check smth. Plus it provides interface.
         */
        public IEnumerable<OwnerViewModel> Owners {  get { return _owners; } }
        #endregion
        #region Commands
        public ICommand CommandAddOwner { get; }
        public ICommand CommandDeleteOwner { get; }
        public ICommand CommandEditOwner { get; }
        #endregion
        public ManageOwnersViewModel(VetSystem vetSystem) 
        { 
            _vetSystem = vetSystem;
            _owners = new ObservableCollection<OwnerViewModel>()
            {
                new OwnerViewModel(new Models.Owner(0, "Maciek", "Surma", true, DateTime.Now, "Gliwice", "662943136", "MaciekSurma@gmail.com", "53562764523")),
                new OwnerViewModel(new Models.Owner(1, "Adrian", "Kieszonka", false, DateTime.Now, "Katowice", "662943136", "Adrian_Kieszonq@hotmail.com", "535278964623")),
                new OwnerViewModel(new Models.Owner(2, "Jurek", "Piernik", true, DateTime.Now, "Zabrze", "662943136", "666Jureczko666@gmail.com", "96705744523"))
            };
            // TODO: Commands!!
        }
    }
}
