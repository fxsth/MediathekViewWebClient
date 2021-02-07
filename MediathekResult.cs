using System.Collections.Generic;
public class MediaElement
{
    public string channel{get;set;}
    public string topic{get;set;}
    public string title{get;set;}
    public string description{get;set;}
    public int timestamp{get;set;}
    public int duration{get;set;}
    public int size{get;set;}
    public string url_website{get;set;}
    public string url_subtitle{get;set;}
    public string url_video{get;set;}
    public string url_video_low{get;set;}
    public string url_video_hd{get;set;}
    public string filmlisteTimestamp{get;set;}
    public string id{get;set;}
}

public class QueryInfo
{
    public string filmlisteTimestamp{get;set;}
    public string searchEngineTime{get;set;}
    public int resultCount{get;set;}
    public int totalResults{get;set;}
}
public class Result
{
    public List<MediaElement> results{get;set;}
    public QueryInfo queryInfo{get;set;}
}

///<summary>root class for json response</summary>
public class MediathekResult
{
    public Result result;
    public object err;

    public int countResults()
    {
        if(result?.results == null)
        {
            return 0;
        }
        return result.results.Count;
    }
}

