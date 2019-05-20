using System;
using System.Windows.Input;

namespace Whid.Helpers
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };

        private Action _targetExecuteMethod;
        private Func<bool> _targetCanExecuteMethod;

        public RelayCommand(Action targetExecuteMethod)
        {
            _targetExecuteMethod = targetExecuteMethod;
        }

        public RelayCommand(Action targetExecuteMethod, Func<bool> targetCanExecuteMethod)
        {
            _targetExecuteMethod = targetExecuteMethod;
            _targetCanExecuteMethod = targetCanExecuteMethod;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod != null)
            {
                return _targetCanExecuteMethod();
            }
            return _targetExecuteMethod != null;
        }

        public void Execute(object parameter)
        {
            _targetExecuteMethod?.Invoke();
        }
    }
}
