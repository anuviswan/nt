using Microsoft.Extensions.Configuration;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.OperationNameGenerators;
using nt.saga.orchestrator.test.Configurations;

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

        _outputFolder = config["OutputDirectory"];
        if (Directory.Exists(_outputFolder))
        {
            Directory.CreateDirectory(_outputFolder);
        }

        var userSettings = config.GetSection("ApiSettings").Get<IEnumerable<ApiSetting>>()!;

        _userServiceSettings = userSettings.First(x => string.Equals(x.Key, "UserService"));
        _authServiceSettings = userSettings.First(x => string.Equals(x.Key, "AuthService"));

    }

    [TestMethod]
    [Ignore] 
    public async Task CreateApiService_For_UserService()
    {
        await GenerateCSharpClient(_userServiceSettings).ConfigureAwait(false);
    }

    [TestMethod]
    [Ignore] 
    public async Task CreateApiService_For_AuthService()
    {
        await GenerateCSharpClient(_authServiceSettings).ConfigureAwait(false);
    }

    private async Task GenerateCSharpClient(ApiSetting apiSettings)
    {
        var openAiDocument = await OpenApiDocument.FromUrlAsync(apiSettings.Uri);

        var settings = new CSharpClientGeneratorSettings
        {
            UseBaseUrl = false,
            ClassName = apiSettings.ClassName,
            CSharpGeneratorSettings =
            {
                Namespace = apiSettings.Namespace,
                ClassStyle = NJsonSchema.CodeGeneration.CSharp.CSharpClassStyle.Poco,
            },
        };

        var codeGenerator = new CSharpClientGenerator(openAiDocument,settings);
        var sourceCode = codeGenerator.GenerateFile();

        await File.WriteAllTextAsync(Path.Combine(_outputFolder, apiSettings.FileName), sourceCode).ConfigureAwait(false);

    }

    public record OpenApiSetting(string SwaggerJsonUrl, string OutputFilePath);
}