using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Net;

namespace nt.saga.orchestrator.test;

[TestClass]
public class NSwagClientCreationTests
{
    [TestMethod]
    [Ignore] 
    public void CreateCSharpClient()
    {

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
}