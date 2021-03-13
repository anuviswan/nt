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
        }

        protected Application Application { get; set; }

        protected void PrepareApplication()
        {
            Application = Application.Current;
            Application.Startup += OnStartup;
        }

        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
        }
    }
}
