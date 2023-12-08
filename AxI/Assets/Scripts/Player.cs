using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public string account;
    [SerializeField] public string createAccTime;
    [SerializeField] public string lastLogin;
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
    public void setCreateTime(string time)
    {
        createAccTime = time;
    }

    public string getPlayerAccout() {
        return account;
    }

    public int getTotalAxies() {
        return axieIDs.Count;
    }

    public void newBie()
    {
        createAccTime = DateTime.Now.Date.ToString();
    }
    public void setLastLoginTime(string time)
    {
        lastLogin = time.ToString();
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
