using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        MediathekClient client = new MediathekClient { channel = "ard", size=100};
        var response = await client.query();
        var count = response.countResults();
        Console.WriteLine("Anzahl Ergebnisse: " + count.ToString());
        Console.WriteLine("Aufgetretene Fehler: " + response?.err?.ToString());
        if(count==0){return;}
        // foreach (var res in response.result.results)
        // {
        //     Console.WriteLine(res.title);
        //     Console.WriteLine(res.channel);
        //     Console.WriteLine(res.topic);
        //     Console.WriteLine(res.timestamp);
        //     Console.WriteLine(res.url_video_hd);
        //     Console.WriteLine();
        // }
    }



}
