using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

public class MediathekDownloader
{
    public MediathekDownloader() => client = new WebClient();
    public void Download(ResultData.MediaElement element, string saveLocation)
    {
        Uri uri = new Uri(element.url_video_hd);
        Download(uri, saveLocation);
    }
    public Task DownloadAsync(ResultData.MediaElement element, string saveLocation)
    {
        Uri uri = new Uri(element.url_video_hd);
        return DownloadAsync(uri, saveLocation);
    }
    public void Download(ResultData.MediaElement element, MediathekDownloadOptions options)
    {
        Download(options.GetUri(element), options.GetPath(element));
    }
    public Task DownloadAsync(ResultData.MediaElement element, MediathekDownloadOptions options)
    {
        return DownloadAsync(options.GetUri(element), options.GetPath(element));
    }
    private void Download(Uri uri, string saveLocation)
    {
        client.DownloadFileCompleted += (sender, e) => Console.WriteLine("Finished");
        client.DownloadFile(uri, RemoveInvalidChars(saveLocation));
    }
    private Task DownloadAsync(Uri uri, string saveLocation)
    {
        client.DownloadFileCompleted += (sender, e) => Console.WriteLine("Finished");
        return client.DownloadFileTaskAsync(uri, RemoveInvalidChars(saveLocation));
    }
    private string RemoveInvalidChars(string filename)
{
    return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));
}
    private WebClient client;
}

public class MediathekDownloadOptions
{
    public bool setOutputFolder(string pathToFolder){
        if(Directory.Exists(pathToFolder))
        {
            _folder = pathToFolder;
            return true;
        }
        return false;
    }
    public void NameFileAfterTopicTitle()
    {
        _nameTopicTitle = true;
    }
    public void SetFilename(string filename) => _filename =filename;
    public string GetPath(ResultData.MediaElement element)
    {
        if(_nameTopicTitle)
        {
        var extension = ".mp4";
        return _folder + element.topic + "-" + element.title + extension;
        }
        return _filename;
    }
    public void SetQualityHD() => _quality=Quality.high;
    public void SetQualityMD() => _quality=Quality.medium;
    public void SetQualityLD() => _quality=Quality.low;
    public Uri GetUri(ResultData.MediaElement element)
    {
        switch (_quality)
        {
            case Quality.low:
            return new Uri(element.url_video_low);
            case Quality.medium:
            return new Uri(element.url_video);
            case Quality.high:
            return new Uri(element.url_video_low);
            default:
            return new Uri(element.url_video);


        }
    }
    private string _folder;
    private string _filename;
    // private Uri _uri;
    private bool _nameTopicTitle = false;
    private Quality _quality;
}
public enum Quality{
    low, medium, high
};