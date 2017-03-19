using LTEDatabase.Command;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace LTEDatabase.ViewModel
{
    class ExitCommand : BaseRoutedCommand
    {
        public ExitCommand() : base(DoMainWindowClosing)
        {
        }

        private static void DoMainWindowClosing(object sender, object args)
        {
            Window temp = sender as Window;
            CancelEventArgs ev = args as CancelEventArgs;
            if (temp != null && ev != null)
            {
                MessageBoxResult result = MessageBox.Show("Вийти з програми?", temp.Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Database.Close();
                }
                else
                {
                    ev.Cancel = true;
                }
            }
        }
    }
}