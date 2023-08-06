using Microsoft.Extensions.Configuration;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace nt.saga.orchestrator.test;

[TestClass]
public class NSwagClientCreationTests
{
    private string _outputFolder = null!;
    private ApiSetting _userServiceSettings = null!;
    private ApiSetting _authServiceSettings = null!;


    [TestInitialize]
    public void Initialize()
    {
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json");
        var config = configurationBuilder.Build();

        var outputFolder = config["OutputDirectory"];
        var userSettings = config.GetSection("ApiSettings").Get<IEnumerable<ApiSetting>>()!;

        _userServiceSettings = userSettings.First(x => string.Equals(x.Key, "UserService"));
        _authServiceSettings = userSettings.First(x => string.Equals(x.Key, "AuthService"));

    }

    [TestMethod]
   // [Ignore] 
    public async Task CreateApiService_For_UserService()
    {
        await GenerateCSharpClient(_userServiceSettings.Uri, _userServiceSettings.OutputFileName).ConfigureAwait(false);
    }

    [TestMethod]
    // [Ignore] 
    public async Task CreateApiService_For_AuthService()
    {
        await GenerateCSharpClient(_authServiceSettings.Uri, _authServiceSettings.OutputFileName).ConfigureAwait(false);
    }

    private async Task GenerateCSharpClient(string url, string outputFilePath)
    {
        var openAiDocument = await OpenApiDocument.FromUrlAsync(url);

        var settings = new CSharpClientGeneratorSettings
        {
            UseBaseUrl = false
        };

        var codeGenerator = new CSharpClientGenerator(openAiDocument,settings);
        var sourceCode = codeGenerator.GenerateFile();

        await File.WriteAllTextAsync(outputFilePath, sourceCode).ConfigureAwait(false);

    }

    public record OpenApiSetting(string SwaggerJsonUrl, string OutputFilePath);
}