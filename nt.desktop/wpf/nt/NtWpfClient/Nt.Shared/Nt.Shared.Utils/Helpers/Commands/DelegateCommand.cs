using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Nt.Shared.Utils.Helpers.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> _action;
        public DelegateCommand(Action<object> action)
        {
            _action = action;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
