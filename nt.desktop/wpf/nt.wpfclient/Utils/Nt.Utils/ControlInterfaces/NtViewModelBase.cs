using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Nt.Utils.ControlInterfaces
{
    public class NtViewModelBase : Screen
    {
        private NtControlBase control;

        public NtControlBase Control
        {
            get => control;
            set
            {
                control = value;
                control.PropertyChanged += ControlPropertyChanged;
            }
        }

        protected virtual void ControlPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyOfPropertyChange(nameof(e.PropertyName));
        }
    }
    public class NtViewModelBase<TControl> : NtViewModelBase, INotifyDataErrorInfo where TControl:NtControlBase
    {
        private Lazy<Dictionary<string, IEnumerable<string>>> _errorDictionary;

        public NtViewModelBase()
        {
            _errorDictionary = new Lazy<Dictionary<string, IEnumerable<string>>>();
        }

        public TControl TypedControl
        {
            get => (TControl)Control;
            set => Control = value;
        }

        protected IDictionary<string, IEnumerable<string>> ErrorDictionary => _errorDictionary.Value; 

        public bool HasErrors => ErrorDictionary.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors([CallerMemberName]string propertyName=null)
        {
            if (ErrorDictionary.ContainsKey(propertyName))
            {
                return ErrorDictionary[propertyName];
            }
            return null;
        }   

        protected virtual Task ValidateProperty(object propertyValue, [CallerMemberName] string propertyName = null)
        {
            if (ErrorDictionary.ContainsKey(propertyName)) ErrorDictionary.Remove(propertyName);

            var context = new ValidationContext(TypedControl) { MemberName = propertyName };
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(propertyValue, context, results))
            {
                ErrorDictionary[propertyName] = results.Select(x => x.ErrorMessage).ToList();
            }

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            return Task.CompletedTask;
        }

        public virtual void Cancel()
        {
            TryClose(false);
        }
    }
}
