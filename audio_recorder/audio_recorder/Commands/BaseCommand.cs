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
            m_command = command;
            m_predicate = null;
        }

        public BaseCommand(Action<object> command, Func<object, bool> predicate)
        {
            m_command = command;
            m_predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return m_predicate == null ? true : m_predicate(parameter);
        }

        public void Execute(object parameter)
        {
            m_command(parameter);
        }

        private Func<object, bool> m_predicate;
        private Action<object> m_command;

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}