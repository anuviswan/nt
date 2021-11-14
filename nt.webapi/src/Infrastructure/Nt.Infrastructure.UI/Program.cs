﻿using Microsoft.AspNetCore;

namespace Nt.WebApi;
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging=>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
}
