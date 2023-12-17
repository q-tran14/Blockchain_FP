using NFTStorage;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    public static ScreenshotHandler SInstance { get; private set; }
    public NFTStorage.NFTStorageClient nftStorage;
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

    public void TakeScreenShot() 
    {
        StartCoroutine(ScreenShot());
    }

    IEnumerator ScreenShot()
    {
        yield return new WaitForEndOfFrame();
    }
    public void UploadImageToNFTStorage()
    {
        if (imagePath != null || imagePath != "") nftStorage.UploadDataFromStringUnityWebrequest(imagePath);
    }

    public void UploadMetadataToNFTStorage()
    {
        if (metadataPath != null || metadataPath != "") nftStorage.UploadDataFromStringUnityWebrequest(metadataPath);
    }

    public void GenerateMetadataJson(string axieID,string imageCid)
    {
        Metadata metadata = new Metadata();
        metadata.name = axieID;
        metadata.image = "ipfs://" + imageCid;

        string json = JsonUtility.ToJson(metadata);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + axieID + ".json", json);
        metadataPath = Application.persistentDataPath + "/" + axieID + ".json";
    }

    [System.Serializable]
    public class Metadata
    {
        public string name;
        public string description;
        public string image;
    }
}
