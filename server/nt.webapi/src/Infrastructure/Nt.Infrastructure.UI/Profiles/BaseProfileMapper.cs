using AutoMapper;

namespace Nt.Infrastructure.WebApi.Profiles;
public class BaseProfileMapper:Profile
{
    public BaseProfileMapper()
    {
        ToEntity();
        FromEntity();
    }

    protected virtual void ToEntity()
    {

    }
    protected virtual void FromEntity()
    {

    }
}
