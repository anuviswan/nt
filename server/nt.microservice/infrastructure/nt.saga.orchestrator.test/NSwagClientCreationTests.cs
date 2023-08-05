using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Configuration;

namespace nt.saga.orchestrator.test;

[TestClass]
public class NSwagClientCreationTests
{
    private ApiSetting _userServiceSettings = null!;
    private ApiSetting _authServiceSettings = null!;


    [TestInitialize]
    public void Initialize()
    {
        var fileMap = new ExeConfigurationFileMap
        {
            ExeConfigFilename = @"appSettings.config"
        };

        var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

        var outputFolder = config.AppSettings.Settings["OutputDirectory"].Value;

        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }


        _userServiceSettings = new ApiSetting(config.AppSettings.Settings["UserServiceSwaggerUri"].Value,
                                             Path.Combine(outputFolder, config.AppSettings.Settings["UserServiceSwaggerOutputFileName"].Value));

        _authServiceSettings = new ApiSetting(config.AppSettings.Settings["AuthServiceSwaggerUri"].Value,
                                             Path.Combine(outputFolder, config.AppSettings.Settings["AuthServiceSwaggerOutputFileName"].Value));


    }

    [TestMethod]
   // [Ignore] 
    public async Task CreateApiService_For_UserService()
    {
        await GenerateCSharpClient(_userServiceSettings.SwaggerJsonUrl, _userServiceSettings.OutputFilePath);
    }

    [TestMethod]
    // [Ignore] 
    public async Task CreateApiService_For_AuthService()
    {
        await GenerateCSharpClient(_authServiceSettings.SwaggerJsonUrl, _authServiceSettings.OutputFilePath);
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

        await File.WriteAllTextAsync(outputFilePath, sourceCode);

    }

    public record ApiSetting(string SwaggerJsonUrl, string OutputFilePath);
}