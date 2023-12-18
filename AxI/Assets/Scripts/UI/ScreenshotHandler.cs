using NFTStorage;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    public static ScreenshotHandler SInstance { get; private set; }
    public NFTStorage.NFTStorageClient nftStorage;
    public string cid;
    public GameObject objScreenShot;
    [SerializeField] private string imagePath;
    [SerializeField] private string metadataPath;


    private void Awake()
    {
        if (SInstance != null && SInstance != this) Destroy(this);
        else
        {
            SInstance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void TakeScreenShot(string axieId) 
    {
        objScreenShot = GameObject.Find("Flag");
        StartCoroutine(ScreenShot(axieId));
    }

    IEnumerator ScreenShot(string axieId)
    {
        yield return new WaitForEndOfFrame();
        float ratio = 0.37f;
        Vector3 pos = Camera.main.WorldToScreenPoint(objScreenShot.transform.position);
        int screenWidth = Screen.width;
        int screenHeight = screenWidth;

        int width = (int)(screenWidth * ratio);
        int height = (int)(screenHeight * ratio);

        Texture2D texture = new Texture2D(width, height,TextureFormat.RGBA32, false);
        texture.ReadPixels(new Rect(pos.x - width/2,pos.y - height/2,width,height),0,0);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        imagePath = Application.persistentDataPath + "/BP/" + axieId + ".png";
        System.IO.File.WriteAllBytes(imagePath, bytes);
    }
    public void UploadNFTToNFTStorage()
    {
        if (imagePath != null || imagePath != "") nftStorage.UploadDataFromStringUnityWebrequest(imagePath);
    }

    public void GenerateMetadataJson(string axieID)
    {
        Metadata metadata = new Metadata();
        metadata.name = axieID;
        metadata.image = "ipfs://" + cid;

        string json = JsonUtility.ToJson(metadata);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/BP/" + axieID + ".json", json);
        metadataPath = Application.persistentDataPath + "/BP/" + axieID + ".json";
    }

    public void UploadMetadataToNFTStorage()
    {
        if (metadataPath != null || metadataPath != "") nftStorage.UploadDataFromStringUnityWebrequest(metadataPath);
    }

    

    [System.Serializable]
    public class Metadata
    {
        public string name;
        public string description;
        public string image;
    }
}
