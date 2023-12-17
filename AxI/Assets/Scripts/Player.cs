using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public string account;
    [SerializeField] public List<string> axieIDs;
    
    
    public void setAccount(string acc)
    {
        account = acc;
    }

    public void addAxie(string Id)
    {
        axieIDs.Add(Id);
        Debug.Log($"Added {Id}");
    }

    public void setAxieIDs(List<string> IDs)
    {
        axieIDs = IDs;
    }

    public string getPlayerAccout() {
        return account;
    }

    public int getTotalAxies() {
        return axieIDs.Count;
    }

    public string DataToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public Player ParseToData(string json)
    {
        return JsonUtility.FromJson<Player>(json); ;
    }
}
