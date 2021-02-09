## Client for calling the MediathekViewWeb-API
Can also download the queried stuff.

Example: (Program.cs)

```C#
// query specific fields by setting the variables
MediathekClient clientFirstOption = new MediathekClient {
    title = "Frage trifft Antwort - Wie sieht es in einer Karsthöhle aus?",
    topic = "Planet Schule - Natur & Umwelt",
    channel = Channel.SWR,
    size = 1
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
// download stuff
MediathekDownloader downloader = new MediathekDownloader();
MediathekDownloadOptions options = new MediathekDownloadOptions();
options.SetQualityLD();
options.NameFileAfterTopicTitle();
foreach (var res in response.result.results)
{
    Console.WriteLine(res.title);
    Console.WriteLine(res.topic);
    Console.WriteLine(res.channel);
    Console.WriteLine(res.timestamp);
    Console.WriteLine(res.url_video_hd);
    Console.WriteLine(res.duration);
    Console.WriteLine();
    // sequential download
    downloader.Download(res, options);
    // async
    await downloader.DownloadAsync(res, "Planet Schule Folge X.mp4");
}

```
