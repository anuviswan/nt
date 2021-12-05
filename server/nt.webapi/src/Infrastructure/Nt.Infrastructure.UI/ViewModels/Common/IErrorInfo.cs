namespace Nt.Infrastructure.WebApi.ViewModels.Common;
public interface IErrorInfo
{
    public bool HasError { get; }
    List<string> Errors { get; set; }
}
