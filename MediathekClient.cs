using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


public class MediathekClient
{
    public MediathekClient()
    {
        _query = new Query();
    }
    public string title
    {
        set
        {
            _query.queries.Add(new FieldAndQuery() { fields = "title", query = value });
        }
    }
    public string topic
    {
        set
        {
            _query.queries.Add(new FieldAndQuery() { fields = "topic", query = value });
        }
    }
    public string channel
    {
        set
        {
            _query.queries.Add(new FieldAndQuery() { fields = "channel", query = value });
        }
    }
    public int duration
    {
        set
        {
            _query.queries.Add(new FieldAndQuery() { fields = "duration", query = value.ToString() });
        }
    }
    public string id
    {
        set
        {
            _query.queries.Add(new FieldAndQuery() { fields = "id", query = value });
        }
    }
    public bool future
    {
        set
        {
            _query.future = value;
        }
    }

    public int offset
    {
        set
        {
            _query.offset = value;
        }
    }
    public int size
    {
        set
        {
            _query.size = value;
        }
    }

    public async Task<MediathekResult> query()
    {
        try
        {
            return await ProcessQuery(_query);
        }
        catch (Exception e)
        {
            return new MediathekResult { err = e.Message };
        }
    }

    private Query _query;
    private static readonly HttpClient client = new HttpClient();



    private static async Task<MediathekResult> ProcessQuery(Query query)
    {
        var text = JsonSerializer.Serialize(query);
        var httpContent = new StringContent(text, Encoding.UTF8, "text/plain");
        var streamTask = await client.PostAsync("https://mediathekviewweb.de/api/query", httpContent);
        if(streamTask.IsSuccessStatusCode)
        {
        String stringResult = await streamTask.Content.ReadAsStringAsync();
        JsonSerializerOptions defaultSerializerSettings = new JsonSerializerOptions();
        defaultSerializerSettings.IncludeFields = true;
        return JsonSerializer.Deserialize<MediathekResult>(stringResult, defaultSerializerSettings);
        }
        return new MediathekResult{err = streamTask.ReasonPhrase};
    }
}