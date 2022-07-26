# HTTPElasticClient

## HTTPElasticClient è una classe C# basata su Nest, per la gestione di base (inserimento, aggiornamento, cancellazione e ricerca) dei documenti di un Server Elasticsearch

### Requisiti

- [NEST](https://www.nuget.org/packages/Nest)

### Esempio

Sia data la classe Persona:

*Persona.cs*

```csharp
public class Persona{
    public string Id {get; set;}
    public string Nome {get; set;}
    public string Cognome {get; set;}
}
```

Di seguito andiamo ad inserire per poi leggere un nuovo documento

*Program.cs*

```csharp
var elastic = new HTTPElasticClient(
    "https://elastic:Fabrizio123@192.168.1.64:9200",
    "persone"
);

var nuovaPersona = new Persona(){
    Id = "001",
    Nome = "Fabrizio",
    Cognome = "Amorelli"
};

try {

    //Inserisco - Aggiorno un documento
    elastic.CreateDocument<Persona>(nuovaPersona, nuovaPersona.Id);
    
    //Ricerca di un documento per Id
    var persone = elastic.GetDocumentById<Persona>("001");
    if(persone.Count() > 0)
        foreach(var p in persone)
            Console.WriteLine($"{p.Source.Id} - {p.Source.Nome} - {p.Source.Cognome}");
    else
        Console.WriteLine("Nessun documento trovato");
}
catch(Exception e){
    Console.WriteLine(e.Message);
}
```

### Lista metodi disponibili

- HTTPElasticClient(string uri): costruttore

- HTTPElasticClient(string uri, string defaultIndex): costruttore

- HTTPElasticClient(Uri[] uris): costruttore

- HTTPElasticClient(Uri[] uris, string defaultIndex): costruttore

- CreateDocument<T>(T document, string _id): void

- CreateDocument<T>(T document, string _id, string index): void

- DeleteDocument<T>(string _id): void

- DeleteDocument<T>(string _id, string index): void

- GetDocumentById<T>(string _id): IReadOnlyCollection<IHit<T>>

- GetDocumentById<T>(string _id, string index): IReadOnlyCollection<IHit<T>>

- GetDocumentByField<T>(string key, string value): IReadOnlyCollection<IHit<T>>

- GetDocumentByField<T>(string key, string value, string index): IReadOnlyCollection<IHit<T>>
