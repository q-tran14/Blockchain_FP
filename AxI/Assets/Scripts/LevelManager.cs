using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LInstance { get; private set; }
    public GameObject player;
    public Vector2 spawnPos;
    public GameObject UI;
    private void Awake()
    {
        if (LInstance != null && LInstance != this) DestroyImmediate(gameObject);
        else LInstance = this;
    }

    //private IEnumerator LoadScene()
    //{
    //    var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    //    while (scene.progress < 0.9f)
    //    {
    //        if (SceneManager.GetActiveScene().buildIndex == 0)
    //        {

    //        }
    //    }
    //    yield return new WaitForSeconds(2.5f);
    //    yield return new WaitForEndOfFrame();
    //}
}
