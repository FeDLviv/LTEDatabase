using System.Windows;
using System.Windows.Interactivity;

namespace LTEDatabase.Command
{
    class CommandEventArgs : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(BaseRoutedCommand), typeof(CommandEventArgs), new PropertyMetadata(null));

        public BaseRoutedCommand Command
        {
            get { return (BaseRoutedCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }    

        protected override void Invoke(object parameter)
        {
            if (Command != null && Command.CanExecute(parameter))
            {
                Command.Execute(AssociatedObject, parameter);
            }
        }
    }
}