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
        client.DefaultRequestHeaders.Accept.Clear();
        // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        var streamTask = await client.PostAsync("https://mediathekviewweb.de/api/query", httpContent);
        String stringResult = await streamTask.Content.ReadAsStringAsync();
        // var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
        JsonSerializerOptions defaultSerializerSettings = new JsonSerializerOptions();
        defaultSerializerSettings.IncludeFields = true;
        var des = JsonSerializer.Deserialize<MediathekResult>(stringResult, defaultSerializerSettings);
        return des;
    }
}