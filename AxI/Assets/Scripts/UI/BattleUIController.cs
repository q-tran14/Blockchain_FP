using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    public GameObject panel;
    public Text wonOrLose;
    public Button returnHome;

    public Vector2 offset;
    public void End(bool win)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
        if (win == true)
        {
            wonOrLose.text = "YOU WON !!!";
        }
        else
        {
            wonOrLose.text = "YOU HAVE BEEN DEFEATED !!!";
        }
        returnHome.onClick.AddListener(ReturnHome);
    }

    public async void ReturnHome()
    {
        var scene = await SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        await Task.Delay(5000);
        Time.timeScale = 1;
        scene.allowSceneActivation = true;
    }
}
