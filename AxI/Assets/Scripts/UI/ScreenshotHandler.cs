using NFTStorage;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Metadata
{
    public string name;
    public string description;
    public string image;
}
public class ScreenshotHandler : MonoBehaviour
{
    public static ScreenshotHandler SInstance { get; private set; }
    public NFTStorage.NFTStorageClient nftStorage;
    public string cid;
    public GameObject objScreenShot;
    [SerializeField] private string imagePath;
    [SerializeField] private string metadataPath;
    public string axieID;
    public bool finished;
    private void Awake()
    {
        if (SInstance != null && SInstance != this) Destroy(this);
        else
        {
            SInstance = this;
            DontDestroyOnLoad(this);
        }
    }
    public IEnumerator ScreenShot()
    {
        finished = false;
        yield return new WaitForEndOfFrame();
        float ratio = 0.375f;
        Vector3 pos = objScreenShot.transform.position;
        int screenHeight = Screen.height;
        int screenWidth = screenHeight;
        int width = (int)(screenWidth * ratio);
        int height = (int)(screenHeight * ratio);

        Texture2D texture = new Texture2D(width, height,TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(pos.x - width/2,pos.y - height/2 + 50,width,height),0,0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        imagePath = Application.persistentDataPath + "/" + axieID + ".png";
        Debug.Log(imagePath);
        System.IO.File.WriteAllBytes(imagePath, bytes);
        finished = true;

        yield return new WaitUntil(() => finished == true);
        UploadNFTToNFTStorage();

        yield return new WaitUntil(() => finished == true);
        GenerateMetadataJson();
        UploadMetadataToNFTStorage();
    }
    public void UploadNFTToNFTStorage()
    {
        if (imagePath != null || imagePath != "" && finished == true)
        {
            Debug.Log(imagePath);
            finished = false;
            nftStorage.UploadDataFromStringUnityWebrequest(imagePath);
        }
    }

    public void GenerateMetadataJson()
    {
        Metadata metadata = new Metadata();
        metadata.name = axieID;
        metadata.image = "ipfs://" + cid;

        string json = JsonUtility.ToJson(metadata);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + axieID + ".json", json);
        metadataPath = Application.persistentDataPath + "/" + axieID + ".json";
    }

    public void UploadMetadataToNFTStorage()
    {
        Debug.Log("Up Metadata");
        if (metadataPath != null || metadataPath != "" && finished == true)
        {

            nftStorage.UploadDataFromStringUnityWebrequest(metadataPath);
        }
    }
}
