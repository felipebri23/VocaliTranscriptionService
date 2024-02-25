using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

internal class Program
{
    private static void Main(string[] args)
    {
        int port;

        if (args.Length == 0 || !int.TryParse(args[0], out port)) 
        {
            port = 8080;
        }

        var server = WireMockServer.Start(port);
        Console.WriteLine("WireMockServer running at {0}", string.Join(",", server.Ports));

        server
            .Given(Request.Create().WithPath(u => u.Contains("transcript")).UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{ ""file"": ""fichero1""}"));

        Console.WriteLine("Press any key to stop the server");
        Console.ReadKey();

        Console.WriteLine("Displaying all requests");
        var allRequests = server.LogEntries;
        Console.WriteLine(JsonConvert.SerializeObject(allRequests, Formatting.Indented));

        Console.WriteLine("Press any key to quit");
        Console.ReadKey();
    }
}