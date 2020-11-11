using Caliburn.Micro;
using MahApps.Metro.Controls.Dialogs;
using Nt.Utils.ServiceInterfaces;
using Nt.WpfClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            _unityContainer.RegisterInstance((Caliburn.Micro.IWindowManager)new WindowManager());
            _unityContainer.RegisterInstance<IEventAggregator>(new EventAggregator(), new ContainerControlledLifetimeManager());

            // Register Services
            ServiceLoader.RegisterServices(_unityContainer);

            // MahApps
            _unityContainer.RegisterInstance<IDialogCoordinator>(new DialogCoordinator());
            //View Models
            _unityContainer.RegisterInstance(new ShellViewModel(_unityContainer.Resolve<IDialogCoordinator>(),
                                                                _unityContainer.Resolve<ICurrentUserService>(),
                                                                _unityContainer.Resolve<IEventAggregator>()));


            foreach (var vm in ViewModelLoader.GetViewModels())
            {
                _unityContainer.RegisterInstance(vm.GetType(), vm);
            }

            LogManager.GetLog = type => new BootstrapLogger(type);
            ConfigureNameTransformer();

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
            var otherAssembliesToSearch = ViewModelLoader.GetAssemblies();
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
