using AutoMapper;
using Nt.Domain.Entities.Entities;
using Nt.Infrastructure.WebApi.Profiles;

namespace Nt.Infrastructure.Tests.Services;
public class ServiceTestBase<TEntityCollection> where TEntityCollection : BaseEntity, new()
{
    protected IMapper Mapper { get; set; }
    protected ITestOutputHelper Output { get;}

    public ServiceTestBase(ITestOutputHelper output)
    {
        Output = output;
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new UserEntityProfile());
        });

        Mapper = mappingConfig.CreateMapper();
        InitializeCollection();
    }

    protected virtual void InitializeCollection()
    {

    }
    public List<TEntityCollection> EntityCollection { get; set; }
}
