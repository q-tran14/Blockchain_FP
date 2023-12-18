using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class GiveAxieAsGift : MonoBehaviour
{
    [SerializeField] private int axieId;
    public Text axieIdTxt;
    // Start is called before the first frame update
    private void OnEnable()
    {
        do
        {
            axieId = Random.Range(5, 150);
            StartCoroutine(LevelManager.LInstance.GetAxiesGenes(axieId.ToString(),false,true, LevelManager.LInstance.axies.Count));
        } while (LevelManager.LInstance.flag);

        axieIdTxt.text = "#" + axieId.ToString();
    }

    private void OnDisable()
    {
        var skeletonGraphics = FindObjectsOfType<SkeletonGraphic>();
        foreach (var p in skeletonGraphics)
        {
            if (p.transform.gameObject.tag == "Gift"){
                Destroy(p.transform.gameObject);
            }
            
        }
    }

    public void ClaimAxie()
    {
        ScreenshotHandler.SInstance.TakeScreenShot(axieId.ToString());
        ScreenshotHandler.SInstance.UploadNFTToNFTStorage();
        if(ScreenshotHandler.SInstance.cid != "" || ScreenshotHandler.SInstance.cid != null)
        {
            ScreenshotHandler.SInstance.GenerateMetadataJson(axieId.ToString());
            ScreenshotHandler.SInstance.UploadMetadataToNFTStorage();
        }
        string uri = "https://ipfs.io/ipfs/" + ScreenshotHandler.SInstance.cid;
        
        //string[] args = { PlayerPrefs.GetString("Account"), uri };
        //ContractManager.CInstance.SendTransaction("safeMint", args);
    }
}
