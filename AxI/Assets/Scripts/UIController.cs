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
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    public Player player;
    public Text addressPlayer;
    public Text axieNum;
    public int axieSelected;

    public GameObject listAxies;
    // Start is called before the first frame update
    void Start()
    {
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
        for (int i = 0; i < l.Count; i++)
        {
            StartCoroutine(LevelManager.LInstance.GetAxiesGenes(l[i].ToString(), true, i));
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