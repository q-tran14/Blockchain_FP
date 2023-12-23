using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvManager : MonoBehaviour
{
    public Environment envList;
    public GameObject Back;
    public Image Front;
    // Start is called before the first frame update
    void Start()
    {
        int id = Random.Range(0, envList.environmentList.Count);
        Sprite back = envList.environmentList[id].back;
        Sprite front = envList.environmentList[id].front;
        Back.GetComponent<SpriteRenderer>().sprite = back;
        Front.GetComponent<Image>().sprite = front;
        Debug.Log("Env: " + id);
    }
}
