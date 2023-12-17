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
using System;
using Random = UnityEngine.Random;

public class UIController : MonoBehaviour
{
    public Player player;
    public Text addressPlayer;
    public Text axieNum;
    public GameObject listAxies;
    public Button nextButton;
    public Button prevButton;
    public int currentAxiesIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        addressPlayer.text = player.getPlayerAccout().Substring(0, 7)+ "..." + player.getPlayerAccout().Substring(player.getPlayerAccout().Length - 5, 5);
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

        // Load list of axies
        StartCoroutine(LoadAxiesSequentially(l));
        nextButton.onClick.AddListener(NextAxie);
        prevButton.onClick.AddListener(PrevAxie);
    }

    private IEnumerator LoadAxiesSequentially(List<int> axies) {
        for (int i = 0; i < axies.Count; i++) {
            yield return StartCoroutine(LevelManager.LInstance.GetAxiesGenes(axies[i].ToString(), true, i));
            currentAxiesIndex = i;
        }
        currentAxiesIndex = 0;
    }

    public void NextAxie() {
        if (currentAxiesIndex == LevelManager.LInstance.axies.Count - 1)
        {
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(false);
            currentAxiesIndex = 0;
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(true);
        }
        else
        {
            // tới vị trí kế
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(false);
            currentAxiesIndex++;
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(true);
        }
    }

    public void PrevAxie() {
        if (currentAxiesIndex == 0)
        {
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(false);
            currentAxiesIndex = LevelManager.LInstance.axies.Count - 1;
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(true);
        }
        else
        {
            // lùi 1 vị trí
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(false);
            currentAxiesIndex--;
            LevelManager.LInstance.axies[currentAxiesIndex].axie.SetActive(true);
        }
    } 

    public void BattleGame() {

        //change screen
    }

    public void ExitGame() {
        // Update data to DB        
        Application.Quit();
    }
    
    public void Gift()
    {
        int id;
        do
        {
            id = Random.Range(5, 11932553);
            StartCoroutine(LevelManager.LInstance.GetAxiesGenes(id.ToString(), true, LevelManager.LInstance.axies.Count));
        } while (LevelManager.LInstance.flag);
    }
}