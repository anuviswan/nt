using System;
using System.Windows;

namespace Nt.Desktop.Bootstrap
{
    public abstract class BootstrapBase
    {
        bool isInitialized;
        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;
            PrepareApplication();
            Configure();
        }

        protected Application Application { get; set; }

        protected virtual object GetInstance(Type type)
        {
            return default;
        }

        protected virtual void Configure()
        {

        }
        protected void PrepareApplication()
        {
            Application = Application.Current;
            Application.Startup += OnStartup;

            IoC.GetInstance = GetInstance;
        }

        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
        }

        protected void DisplayRootView<TViewModel>()
        {

        }
    }
}
