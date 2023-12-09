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
    public Button nextButton;
    public Button prevButton;
    private int currentAxiesIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();   
        // addressPlayer.text = player.getPlayerAccout();
        // UpdateUI();
        loadList();
        nextButton.onClick.AddListener(NextAxie);
        prevButton.onClick.AddListener(PrevAxie);
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

    // Load list of axies
    StartCoroutine(LoadAxiesSequentially(l));
    }

    private IEnumerator LoadAxiesSequentially(List<int> axies) {
        for (int i = 0; i < axies.Count; i++) {
            yield return StartCoroutine(LevelManager.LInstance.GetAxiesGenes(axies[i].ToString(), true, i));
            currentAxiesIndex = i;
            Debug.Log(currentAxiesIndex);
        }
    }

    public void NextAxie() {
        if (currentAxiesIndex < LevelManager.LInstance.axies.Count - 1) {
            // tới vị trí kế
            currentAxiesIndex++;
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(true);
            LevelManager.LInstance.axies[currentAxiesIndex - 1].axie.SetActive(false);
        }
    }

    public void PrevAxie() {
        if (currentAxiesIndex > 0) {
            // lùi 1 vị trí
            currentAxiesIndex--;
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(true);
            LevelManager.LInstance.axies[currentAxiesIndex + 1].axie.SetActive(false);
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