using Nest;
using Elasticsearch.Net;

public class HTTPElasticClient{
	
    private ElasticClient client;

    private HTTPElasticClient(){}
	
    public HTTPElasticClient(string uri){
        var settings = new ConnectionSettings(new Uri(uri));
        client = new ElasticClient(settings);
    }
    public HTTPElasticClient(string uri, string defaultIndex)
    {
        var settings = new ConnectionSettings(new Uri(uri)).DefaultIndex(defaultIndex);
        client = new ElasticClient(settings);
    }

    public HTTPElasticClient(Uri[] uris){
        var connectionPool = new SniffingConnectionPool(uris);
        var settings = new ConnectionSettings(connectionPool);
        client = new ElasticClient(settings);
    }
    public HTTPElasticClient(Uri[] uris, string defaultIndex){
        var connectionPool = new SniffingConnectionPool(uris);
        var settings = new ConnectionSettings(connectionPool).DefaultIndex(defaultIndex);
        client = new ElasticClient(settings);
    }

    public void CreateDocument<T>(T document, string _id) where T: class =>
        client.Index<T>(document, i => i.Id(_id).Refresh(Refresh.True));
    
    public void CreateDocument<T>(T document, string _id, string index) where T: class =>
        client.Index<T>(document, i => i.Index(index).Id(_id).Refresh(Refresh.True));
    
    public void DeleteDocument<T>(string _id) where T: class =>
        client.Delete<T>(_id);

    public void DeleteDocument<T>(string _id, string index) where T: class =>
        client.Delete<T>(_id, d => d.Index(index));

    public IReadOnlyCollection<IHit<T>> GetDocumentById<T>(string _id) where T: class =>
        client.Search<T>(s => s.Query(q => q.Term(t => t.Field("_id").Value(_id)))).Hits;
    
    public IReadOnlyCollection<IHit<T>> GetDocumentById<T>(string _id, string index) where T: class =>
        client.Search<T>(s => s.Index(index).Query(q => q.Term(t => t.Field("_id").Value(_id)))).Hits;
    
    public IReadOnlyCollection<IHit<T>> GetDocumentByField<T>(string key, string value) where T: class =>
        client.Search<T>(s => s.Query(q => q.Match(m => m.Field(key).Query(value)))).Hits;

    public IReadOnlyCollection<IHit<T>> GetDocumentByField<T>(string key, string value, string index) where T: class =>
        client.Search<T>(s => s.Index(index).Query(q => q.Match(m => m.Field(key).Query(value)))).Hits;
}