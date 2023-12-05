using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Linq;
using Unity.VisualScripting;
using Newtonsoft.Json;

public class DatabaseManager : MonoBehaviour
{
    const string connectionUri = "mongodb+srv://quantran14:quantran14@cluster0.ozqblox.mongodb.net/?retryWrites=true&w=majority";
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    // Start is called before the first frame update
    void Awake()
    {
        // Connect Database from MongoDB
        MongoClient client = new MongoClient(connectionUri);
        
        // Access into Cluster and Database to get the Collection
        database = client.GetDatabase("Player");   // Player => name of Database
        collection = database.GetCollection<BsonDocument>("PlayerAxie");  // PlayerAxie => name of Collection
    }

    public void Upload(string json)
    {
        BsonDocument doc = BsonDocument.Parse(json);
        collection.InsertOneAsync(doc);
    }

    public void CheckData()
    {
        Player user = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        FilterDefinition<BsonDocument> filter = new BsonDocument("account", PlayerPrefs.GetString("Account"));    // Create a Filter has title is Account
        BsonDocument doc = collection.Find(filter).FirstOrDefault();
        user.setAccount(PlayerPrefs.GetString("Account"));
        if (doc == null)
        {
            // Add account into DB
            user.newBie();
            //user.setLastLoginTime(DateTime.Now);
            string data =  user.DataToJson();
            Debug.Log(data);
            Upload(data);
        }
        else
        {
            //Download account data from Data
            var list = doc.ToList()[4].Value.ToJson();

            List<int> axieIDs = JsonConvert.DeserializeObject<List<int>>(list);
            user.setAxieIDs(axieIDs);
            user.setCreateTime(doc.GetElement(2).Value.ToString());
            user.setLastLoginTime(doc.GetElement(3).Value.ToString());
        }
        DontDestroyOnLoad(user);
    }
}
