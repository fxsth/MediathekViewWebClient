using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QueryData;

public class MediathekClient
{
    public MediathekClient() => _query = new Query();
    public MediathekClient searchTitle(string value){
        _query.addFieldAndQuery("title", value );
        return this;
        }
    public MediathekClient searchTitleOrTopic(string value){
        _query.addFieldAndQuery("title", "topic", value );
        return this;
    }
    public MediathekClient searchTopic(string value) {
        _query.addFieldAndQuery("topic", value );
        return this;
    }
    public MediathekClient searchChannel(Channel channel){
        _query.addFieldAndQuery("channel", channel==Channel.DreiSat ? "3sat" : Enum.GetName<Channel>(channel) );
        return this;
    }
    public MediathekClient searchDuration(string value) {
        _query.addFieldAndQuery("duration", value );
        return this;
    }
    public MediathekClient searchId(string value) {
        _query.addFieldAndQuery("id", value );
        return this;
    }
    public MediathekClient withFutureMedia(bool value) {
        _query.future = value;
        return this;
    }
    public MediathekClient setOffset(int value) {
        _query.offset = value;
        return this;
    }
    public MediathekClient setMaximumResults(int value) {
        _query.size = value;
        return this;
    }
    public MediathekClient orderResultsBy(string value, SortOrder order = SortOrder.asc) {
        _query.sortBy = value;
        _query.sortOrder = Enum.GetName<SortOrder>(order);
        return this;
    }


    public string title
    {
        set => _query.addFieldAndQuery("title", value );
    }
    public string topic
    {
        set => _query.addFieldAndQuery("topic", value );
    }
    public Channel channel
    {
        set =>_query.addFieldAndQuery("channel", value==Channel.DreiSat ? "3sat" : Enum.GetName<Channel>(value) );
    }
    public int duration
    {
        set => _query.addFieldAndQuery("duration", value.ToString() );
    }
    public string id
    {
        set => _query.addFieldAndQuery("id", value );
    }
    public bool future
    {
        set => _query.future = value;
    }

    public int offset
    {
        set => _query.offset = value;
    }
    public int size
    {
        set => _query.size = value;
    }

    public async Task<MediathekResult> sendQuery()
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
        JsonSerializerOptions querySerializerOptions = new JsonSerializerOptions{IgnoreNullValues = true};
        var text = JsonSerializer.Serialize(query, querySerializerOptions);
        var httpContent = new StringContent(text, Encoding.UTF8, "text/plain");
        var streamTask = await client.PostAsync("https://mediathekviewweb.de/api/query", httpContent);
        if(streamTask.IsSuccessStatusCode)
        {
            String stringResult = await streamTask.Content.ReadAsStringAsync();
            JsonSerializerOptions resultSerializerOptions = new JsonSerializerOptions();
            resultSerializerOptions.IncludeFields = true;
            resultSerializerOptions.Converters.Add(new Custom.DateTimeConverter());
            return JsonSerializer.Deserialize<MediathekResult>(stringResult, resultSerializerOptions);
        }
        return new MediathekResult{err = streamTask.ReasonPhrase};
    }
}


namespace Custom
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32()).DateTime;

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
    }
}