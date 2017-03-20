using System;

using System.Windows.Input;

namespace LTEDatabase.Command
{
    class BaseRoutedCommand : RoutedCommand
    {
        private Action<object, object> execute;
        private Predicate<object> canExecute;

        public BaseRoutedCommand(Action<object, object> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object sender, object args)
        {
            execute(sender, args);
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }
            else
            {
                return true;
            }
        }
    }
}