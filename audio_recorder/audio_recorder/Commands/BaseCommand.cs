using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;

namespace audio_recorder.Command
{
    class BaseCommand : ICommand
    {
        public BaseCommand(Action<object> command)
        {
            _command = command;
            _predicate = null;
        }

        public BaseCommand(Action<object> command, Func<object, bool> predicate)
        {
            _command = command;
            _predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return _predicate == null ? true : _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _command(parameter);
        }

        private Func<object, bool> _predicate;
        private Action<object> _command;

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}