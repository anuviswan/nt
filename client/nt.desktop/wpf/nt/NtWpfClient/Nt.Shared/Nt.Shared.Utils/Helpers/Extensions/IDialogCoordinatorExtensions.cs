using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using Nt.Shared.Utils.Helpers.Enumerations;

namespace Nt.Shared.Utils.Helpers.Extensions
{
    public static class IDialogCoordinatorExtensions
    {
        public static async Task<LoginDialogData> ShowNtLogin(this IDialogCoordinator source, object context)
        {
            return await source.ShowLoginAsync(context, "LOGIN", string.Empty);
        }
        public static async Task<DialogResponse> ShowNtOkDialog(this IDialogCoordinator source, object parent, string title, string message)
        {
            var dialogSettings = new MetroDialogSettings
            {
                AffirmativeButtonText = nameof(DialogResponse.Ok),
            };

            var _ = await source.ShowMessageAsync(parent, title, message, MessageDialogStyle.Affirmative, dialogSettings);
            return DialogResponse.Ok;
        }
    }
}
