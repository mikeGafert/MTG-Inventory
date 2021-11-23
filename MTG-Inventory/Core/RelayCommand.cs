using System;
using System.Windows.Input;

namespace MTG_Inventory.Core
{
    class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;       

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }       

        public bool CanExecute(object parameter) => this._canExecute?.Invoke(parameter) ?? true;
        

        public void Execute(object parameter) => this._execute?.Invoke(parameter);
        
    }
}
