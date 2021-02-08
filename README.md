# Client for calling the MediathekViewWeb-API
Example:

<code>
MediathekClient clientFirstOption = new MediathekClient {  
    topic = "Der Tatortreiniger",  
    channel = Channel.NDR  
};

MediathekClient clientSecondOption = new MediathekClient()  
.searchTitleOrTopic("Sturm der Liebe")  
.searchChannel(Channel.NDR)  
.setMaximumResults(20)  
.orderResultsBy("timestamp", SortOrder.desc);

var response = await clientFirstOption.sendQuery();

if(response.countResults()==0){return;}

foreach (var res in response.result.results)
{
    Console.WriteLine(res.title);
    Console.WriteLine(res.topic);
    Console.WriteLine(res.timestamp);
    Console.WriteLine(res.url_video_hd);
    Console.WriteLine();
}
</code>
