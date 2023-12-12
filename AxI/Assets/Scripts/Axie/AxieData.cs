using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AxieData : MonoBehaviour
{
    [SerializeField] private string axieID;
    [SerializeField] private string cid;

    public AxieData(string axieId, string id)
    {
        axieID = axieId;
        cid = id;
    }

    public string DataToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public AxieData ParseToData(string json)
    {
        return JsonUtility.FromJson<AxieData>(json); ;
    }
}

