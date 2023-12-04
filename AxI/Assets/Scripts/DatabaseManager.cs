using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine.Networking;
using System;
using System.Text;

public class DatabaseManager : MonoBehaviour
{
    const string connectionUri = "mongodb+srv://quantran14:quantran14@cluster0.ozqblox.mongodb.net/?retryWrites=true&w=majority";
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    public string json;
    // Start is called before the first frame update
    void Awake()
    {
        // Connect Database from MongoDB
        MongoClient client = new MongoClient(connectionUri);
        
        // Access into Cluster and Database to get the Collection
        database = client.GetDatabase("Player");   // Player => name of Database
        collection = database.GetCollection<BsonDocument>("PlayerAxie");  // PlayerAxie => name of Collection
        FilterDefinition<BsonDocument> filter = new BsonDocument("PlayerAddress", PlayerPrefs.GetString("Account"));    // Create a Filter has title is nameOfPic

        
        if (collection.Find(filter).FirstOrDefault() == null)
        {
            //// Add account into DB
            //var jsonString = PlayerPrefs.GetString("Account");
            //var acc = JsonUtility.FromJson<WalletLogin>(jsonString);
            //StartCoroutine(Upload(_playerData.Stringify(), result => {
            //    Debug.Log(result);
            //}));
        }
    }

    public void Upload(string json)
    {
        BsonDocument doc = BsonDocument.Parse(json);
        collection.InsertOneAsync(doc);
    }

    void Receive()
    {

    }
    //IEnumerator Upload(string profile, System.Action<bool> callback = null)
    //{
    //    using (UnityWebRequest request = new UnityWebRequest("http://localhost:3000/plummies", "POST"))
    //    {
    //        request.SetRequestHeader("Content-Type", "application/json");
    //        byte[] bodyRaw = Encoding.UTF8.GetBytes(profile);
    //        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //        request.downloadHandler = new DownloadHandlerBuffer();
    //        yield return request.SendWebRequest();

    //        if (request.isNetworkError || request.isHttpError)
    //        {
    //            Debug.Log(request.error);
    //            if (callback != null)
    //            {
    //                callback.Invoke(false);
    //            }
    //        }
    //        else
    //        {
    //            if (callback != null)
    //            {
    //                callback.Invoke(request.downloadHandler.text != "{}");
    //            }
    //        }
    //    }
    //}
}
