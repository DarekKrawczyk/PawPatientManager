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

    public abstract class AsyncCommandBase : CommandBase
    {
        private bool _isExecuting; 
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                OnCanExecutedChange();
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }
        public override async void Execute(object parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExecuting = false;
            }
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
