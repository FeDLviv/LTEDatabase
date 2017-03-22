using System;
using System.Windows;

namespace LTEDatabase
{
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            MessageBox.Show(ex.Message,"Помилка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            Environment.Exit(1);
        }
    }
}
