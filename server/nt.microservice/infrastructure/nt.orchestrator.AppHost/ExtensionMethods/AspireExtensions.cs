namespace nt.orchestrator.AppHost.ExtensionMethods;

public static class AspireExtensions
{
    public static IResourceBuilder<T> WaitForAll<T>(this IResourceBuilder<T> builder, IEnumerable<IResourceBuilder<ProjectResource>>  dependencies) 
        where T: IResource, IResourceWithWaitSupport
    {
        foreach (var dep in dependencies)
            builder.WaitFor(dep);

        return builder;
    }
}
