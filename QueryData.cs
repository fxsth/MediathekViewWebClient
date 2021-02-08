using System.Collections.Generic;
using System.ComponentModel;

namespace QueryData
{
///<summary>Data type for serializing into json-content</summary>
public class Query
{
    public Query()
    {
        queries = new List<FieldAndQuery>();
    }
    public void addFieldAndQuery(string field, string query) => queries.Add(new FieldAndQuery(field, query));
    public void addFieldAndQuery(string field1, string field2, string query) => queries.Add(new FieldAndQuery(field1, field2, query));

    public List<FieldAndQuery> queries { get; set; }
    public string sortBy { get; set; }
    public string sortOrder { get; set; }
    public bool? future { get; set; }
    public int? offset { get; set; }
    public int? size { get; set; }
}


public class FieldAndQuery
{    public FieldAndQuery(string pField, string pQuery)
    {
        fields = new List<string>{pField};
        query = pQuery;
    }
    public FieldAndQuery(string pField1,string pField2, string pQuery)
    {
        fields = new List<string>{pField1, pField2};
        query = pQuery;
    }
    public List<string> fields { get; set; }
    public string query { get; set; }
}
}

public enum SortOrder{
    asc, desc
}
public enum Channel
{
    ARD,
    BR,
    HR,
    MDR,
    NDR,
    RBB,
    SR,
    SWR,
    WDR,
    ZDF,
    Phoenix,
    Kika,
    DreiSat,
    Arte,
    DWTV,
    ORF,
    SRF

}