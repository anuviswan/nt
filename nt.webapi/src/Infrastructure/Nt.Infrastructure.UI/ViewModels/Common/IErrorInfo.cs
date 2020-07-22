namespace Nt.Infrastructure.WebApi.ViewModels.Common
{
    public interface IErrorInfo
    {
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        string ErrorMessage { get; set; }
    }
}
