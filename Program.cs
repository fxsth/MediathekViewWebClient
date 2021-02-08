using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // query specific fields by setting the variables
        MediathekClient clientFirstOption = new MediathekClient {
            topic = "Der Tatortreiniger",
            channel = Channel.NDR
        };
        // or use the functions to define the query
        MediathekClient clientSecondOption = new MediathekClient()
        .searchTitleOrTopic("Sturm der Liebe")
        .searchChannel(Channel.NDR)
        .setMaximumResults(20)
        .orderResultsBy("timestamp", SortOrder.desc);
        // send query and retrieve response
        var response = await clientFirstOption.sendQuery();
        var count = response.countResults();
        Console.WriteLine("Anzahl Ergebnisse: " + count.ToString());
        Console.WriteLine("Aufgetretene Fehler: " + response?.err?.ToString());
        if(count==0){return;}
        foreach (var res in response.result.results)
        {
            Console.WriteLine(res.title);
            Console.WriteLine(res.topic);
            Console.WriteLine(res.channel);
            Console.WriteLine(res.timestamp);
            Console.WriteLine(res.url_video_hd);
            Console.WriteLine(res.duration);
            Console.WriteLine();
        }
    }
}
