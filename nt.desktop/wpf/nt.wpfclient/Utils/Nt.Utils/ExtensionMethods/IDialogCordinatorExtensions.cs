using MahApps.Metro.Controls.Dialogs;
using Nt.Utils.Helper.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Utils.ExtensionMethods
{
    public static class IDialogCordinatorExtensions
    {
        public static async Task<LoginDialogData> ShowNtLogin(this IDialogCoordinator source,object context)
        {
            return await source.ShowLoginAsync(context, "LOGIN", string.Empty);
        }

        public static async Task<DialogResponse> ShowNtOkDialog(this IDialogCoordinator source,object parent, string title,string message)
        {
            var dialogSettings = new MetroDialogSettings
            {
                AffirmativeButtonText = "Ok",
            };

            var _ = await source.ShowMessageAsync(parent, title, message, MessageDialogStyle.Affirmative, dialogSettings);
            return DialogResponse.Ok;
        }
    }
}
