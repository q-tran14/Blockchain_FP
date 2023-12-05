using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader SInstance { get; private set; }
    public float duration = 6f;

    public Image i;

    private void Awake()
    {
        if (SInstance != null && SInstance != this) Destroy(this);
        else { 
            SInstance = this;
            DontDestroyOnLoad(this);
        }
    }
    public void Run()
    {
        StartCoroutine("_FadingOutI",i);

    }
    public IEnumerator _FadingOut()
    {
        Color from = Color.white;
        Color to = new Color(1,1,1,0);
        float t = 0f;
        while (t < duration)
        {
            i.color = Color.Lerp(from, to, t);
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void setImage(Image im)
    {
        i = im;
    }
}
