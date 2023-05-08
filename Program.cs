using Azure.Core;
using Azure.Identity;

namespace CheckAzureCreds;

internal class Program
{
    private static void Main()
    {
        // Azure.Identity.AzureIdentityEventSource.Singleton.IsEnabled = true;

        Console.WriteLine("---------- DefaultAzureCredential -----------");
        TestCredential(new DefaultAzureCredential());

        Console.WriteLine("");
        Console.WriteLine("---------- EnvironmentCredential -----------");
        TestCredential(new EnvironmentCredential());

        Console.WriteLine("");
        Console.WriteLine("---------- ManagedIdentityCredential -----------");
        TestCredential(new ManagedIdentityCredential());

        Console.WriteLine("");
        Console.WriteLine("---------- SharedTokenCacheCredential -----------");
        TestCredential(new SharedTokenCacheCredential());

        Console.WriteLine("");
        Console.WriteLine("---------- SharedTokenCacheCredential-Username -----------");
        var sharedTokenCacheCredentialUsername = new SharedTokenCacheCredential(new SharedTokenCacheCredentialOptions
        {
            Username = "willard.johnson@eso.com"
        });
        TestCredential(sharedTokenCacheCredentialUsername);

        Console.WriteLine("");
        Console.WriteLine("---------- VisualStudioCredential -----------");
        TestCredential(new VisualStudioCredential());

        Console.WriteLine("");
        Console.WriteLine("---------- VisualStudioCodeCredential -----------");
        TestCredential(new VisualStudioCodeCredential());

        Console.WriteLine("");
        Console.WriteLine("---------- AzureCliCredential -----------");
        TestCredential(new AzureCliCredential());

        Console.WriteLine("");
        Console.WriteLine("---------- AzurePowerShellCredential -----------");
        TestCredential(new AzurePowerShellCredential());
    }

    private static void TestCredential(TokenCredential credential)
    {
        try
        {
            var token = credential.GetToken(
                new TokenRequestContext(new[] 
                    {
                        "https://management.azure.com//.default"
                    },
                    null),
                CancellationToken.None);
            Console.WriteLine("Authentication succeeded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Authentication failed: " + ex.Message);
            if (ex.InnerException != null)
                Console.WriteLine("Inner exception message: " + ex.InnerException.Message);
        }
    }
}