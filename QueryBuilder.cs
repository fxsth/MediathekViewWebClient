using System.Collections.Generic;

///<summary>Data type for serializing into json-content</summary>
public class Query
{
    public Query()
    {
        queries = new List<FieldAndQuery>();
    }
    public List<FieldAndQuery> queries { get; set; }
    public string sortBy { get; set; }
    public string sortOrder { get; set; }
    public string future { get; set; }
    public int offset { get; set; }
    public int size { get; set; }
}


public class FieldAndQuery
{
    public string fields { get; set; }
    public string query { get; set; }
}