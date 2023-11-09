using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PawPatientManager.Commands
{
    /*  Base class for Commands, they are used to execure certain code. For example
     *  Every button has to have an command binding.
     */
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            /*  If *CanExecute()* returns false, then button is disabled!!
             */
            return true;
        }
        protected void OnCanExecutedChange()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
        public abstract void Execute(object? parameter);
    }
}
