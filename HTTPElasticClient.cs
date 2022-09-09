using Nest;
using Elasticsearch.Net;

public class HTTPElasticClient
{

    private ElasticClient client;

    private HTTPElasticClient() { }

    public HTTPElasticClient(string uri)
    {
        var settings = new ConnectionSettings(new Uri(uri));
        client = new ElasticClient(settings);
    }

    public HTTPElasticClient(string uri, int timeout)
    {
        var settings = new ConnectionSettings(new Uri(uri))
            .RequestTimeout(new TimeSpan(0,0,0,0,timeout));
        client = new ElasticClient(settings);
    }

    public HTTPElasticClient(string uri, string defaultIndex)
    {
        var settings = new ConnectionSettings(new Uri(uri)).DefaultIndex(defaultIndex);
        client = new ElasticClient(settings);
    }

    public HTTPElasticClient(string uri, string defaultIndex, int timeout)
    {
        var settings = new ConnectionSettings(new Uri(uri))
            .DefaultIndex(defaultIndex)
            .RequestTimeout(new TimeSpan(0,0,0,0,timeout));
        client = new ElasticClient(settings);
    }

    public HTTPElasticClient(Uri[] uris)
    {
        var connectionPool = new StaticConnectionPool(uris);
        var settings = new ConnectionSettings(connectionPool);
        client = new ElasticClient(settings);
    }

    public HTTPElasticClient(Uri[] uris, int timeout)
    {
        var connectionPool = new StaticConnectionPool(uris);
        var settings = new ConnectionSettings(connectionPool).RequestTimeout(new TimeSpan(0,0,0,0,timeout));
        client = new ElasticClient(settings);
    }
    public HTTPElasticClient(Uri[] uris, string defaultIndex)
    {
        var connectionPool = new StaticConnectionPool(uris);
        var settings = new ConnectionSettings(connectionPool).DefaultIndex(defaultIndex);
        client = new ElasticClient(settings);
    }

    public HTTPElasticClient(Uri[] uris, string defaultIndex, int timeout)
    {
        var connectionPool = new StaticConnectionPool(uris);
        var settings = new ConnectionSettings(connectionPool)
            .DefaultIndex(defaultIndex)
            .RequestTimeout(new TimeSpan(0,0,0,0,timeout));
        client = new ElasticClient(settings);
    }

    public ClusterHealthResponse ClusterHealt_Response() =>
           client.Cluster.Health();

    public void CreateDocument<T>(T document, string _id) where T : class =>
        client.Index<T>(document, i => i.Id(_id).Refresh(Refresh.True));
    
    public IndexResponse CreateDocument_Response<T>(T document, string _id) where T : class =>
        client.Index<T>(document, i => i.Id(_id).Refresh(Refresh.True));

    public void CreateDocument<T>(T document, string _id, string index) where T : class =>
        client.Index<T>(document, i => i.Index(index).Id(_id).Refresh(Refresh.True));
    
    public IndexResponse CreateDocument_Response<T>(T document, string _id, string index) where T : class =>
        client.Index<T>(document, i => i.Index(index).Id(_id).Refresh(Refresh.True));

    public void DeleteDocument<T>(string _id) where T : class =>
        client.Delete<T>(_id);

    public DeleteResponse DeleteDocument_Response<T>(string _id) where T : class =>
        client.Delete<T>(_id);

    public void DeleteDocument<T>(string _id, string index) where T : class =>
        client.Delete<T>(_id, d => d.Index(index));

    public DeleteResponse DeleteDocument_Response<T>(string _id, string index) where T : class =>
        client.Delete<T>(_id, d => d.Index(index));

    public IReadOnlyCollection<IHit<T>> GetDocumentById<T>(string _id) where T : class =>
        client.Search<T>(s => s.Query(q => q.Term(t => t.Field("_id").Value(_id)))).Hits;

    public ISearchResponse<T> GetDocumentById_Response<T>(string _id) where T : class =>
        client.Search<T>(s => s.Query(q => q.Term(t => t.Field("_id").Value(_id))));

    public IReadOnlyCollection<IHit<T>> GetDocumentById<T>(string _id, string index) where T : class =>
        client.Search<T>(s => s.Index(index).Query(q => q.Term(t => t.Field("_id").Value(_id)))).Hits;

    public ISearchResponse<T> GetDocumentById_Response<T>(string _id, string index) where T : class =>
        client.Search<T>(s => s.Index(index).Query(q => q.Term(t => t.Field("_id").Value(_id))));

    public IReadOnlyCollection<IHit<T>> GetDocumentByField<T>(string key, string value) where T : class =>
        client.Search<T>(s => s.Query(q => q.Match(m => m.Field(key).Query(value)))).Hits;

    public ISearchResponse<T> GetDocumentByField_Response<T>(string key, string value) where T : class =>
        client.Search<T>(s => s.Query(q => q.Match(m => m.Field(key).Query(value))));

    public IReadOnlyCollection<IHit<T>> GetDocumentByField<T>(string key, string value, string index) where T : class =>
        client.Search<T>(s => s.Index(index).Query(q => q.Match(m => m.Field(key).Query(value)))).Hits;

    public ISearchResponse<T> GetDocumentByField_Response<T>(string key, string value, string index) where T : class =>
        client.Search<T>(s => s.Index(index).Query(q => q.Match(m => m.Field(key).Query(value))));

    public IReadOnlyCollection<IHit<T>> GetAllDocument<T>() where T : class =>
        client.Search<T>().Hits;

    public ISearchResponse<T> GetAllDocument_Response<T>() where T : class =>
        client.Search<T>();

    public IReadOnlyCollection<IHit<T>> GetAllDocument<T>(string index) where T : class =>
        client.Search<T>(s => s.Index(index)).Hits;

    public ISearchResponse<T> GetAllDocument_Response<T>(string index) where T : class =>
        client.Search<T>(s => s.Index(index));
}
