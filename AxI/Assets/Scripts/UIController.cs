using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using AxieCore.AxieMixer;
using AxieMixer.Unity;
using Newtonsoft.Json.Linq;
using Spine.Unity;
using UnityEngine.Networking;
using Jint.Native.Number;


public class UIController : MonoBehaviour
{
    public Player player;
    public Text addressPlayer;
    public Text axieNum;
    public int axieSelected;

    public int numberAxieInList;
    public GameObject listAxiesActivate;

    // Start is called before the first frame update
    void Start()
    {
        numberAxieInList = 0;
        // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();   
        // addressPlayer.text = player.getPlayerAccout();
        // UpdateUI();
        loadList();
    }


    public void UpdateUI() {
        axieNum.text = player.getTotalAxies().ToString();
    }

   public void loadList() {
    List<int> l = new List<int>
    {
        1568,
        1569,
        1570,
        3212,
        322,
        111
    };
        //load list of axies - BUG
        foreach (int i in l) {
            StartCoroutine(LevelManager.LInstance.GetAxiesGenes(i.ToString(),true));
            numberAxieInList += 1;
            if(numberAxieInList != 1){
                listAxiesActivate.transform.GetChild(numberAxieInList - 1).gameObject.SetActive(false);
            }
        }
    }


    public void BattleGame() {

        //change screen
    }

    public void ExitGame() {
        // Update data to DB        
        Application.Quit();
    }

}