using Caliburn.Micro;
using Nt.Controls.Login;
using Nt.Utils.ControlInterfaces;
using Nt.WpfClient.ViewModels;
using Nt.WpfClient.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace Nt.WpfClient.Utils.Bootstrap
{
    public class BootstrapperUnity : BootstrapperBase
    {
        private IUnityContainer _unityContainer;
        #region Constructor
        public BootstrapperUnity()
        {
            Initialize();
        }
        #endregion

        #region Overrides
        protected override void Configure()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterInstance<IWindowManager>(new WindowManager());
            _unityContainer.RegisterInstance<IEventAggregator>(new EventAggregator(), new ContainerControlledLifetimeManager());

            //View Models
            _unityContainer.RegisterInstance(new ShellViewModel());
            var vms = typeof(LoginControl).Assembly
                .GetTypes()
                .Where(x => typeof(NtControlBase).IsAssignableFrom(x))
                .Select(x => Activator.CreateInstance(x)).Cast<NtControlBase>().Select(x => x.ViewModel);
            foreach (var vm in vms)
            {
                _unityContainer.RegisterInstance(vm.GetType(), vm);
            }

            LogManager.GetLog = type => new DebugLogger(type);
            ConfigureNameTransformer();
        }
        public class DebugLogger : ILog
        {
            private readonly Type _type;

            public DebugLogger(Type type)
            {
                _type = type;
            }

            public void Info(string format, params object[] args)
            {
                if (format.StartsWith("No bindable"))
                    return;
                if (format.StartsWith("Action Convention Not Applied"))
                    return;
                Debug.WriteLine("INFO: " + format, args);
            }

            public void Warn(string format, params object[] args)
            {
                Debug.WriteLine("WARN: " + format, args);
            }

            public void Error(Exception exception)
            {
                Debug.WriteLine("ERROR: {0}\n{1}", _type.Name, exception);
            }
        }
        private void ConfigureNameTransformer()
        {
            ViewLocator.NameTransformer.AddRule("Model$", string.Empty);
        }

        protected override void BuildUp(object instance)
        {
            _unityContainer.BuildUp(instance);
            base.BuildUp(instance);
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrEmpty(key) ? _unityContainer.Resolve(service, key) : _unityContainer.Resolve(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _unityContainer.ResolveAll(service);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var baseList = base.SelectAssemblies().ToList();
            var otherAssembliesToSearch = new Assembly[] { typeof(LoginControl).Assembly };
            baseList.AddRange(otherAssembliesToSearch);
            return baseList;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
        #endregion
    }
}
