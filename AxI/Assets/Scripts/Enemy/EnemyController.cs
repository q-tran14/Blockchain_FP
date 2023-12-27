using AxieMixer.Unity;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Enemy
{
    public float HP;
    public float dmg;

    public Enemy(float hp, float d)
    {
        HP = hp;
        dmg = d;
    }
}

public class EnemyController : MonoBehaviour
{
    public Enemy enemy;
    public string currentState;
    public string previousState;

    public SkeletonAnimation eneAni;

    public bool collisionPlayer = false;
    public bool move = false;
    public float speed = 2;
    public HealthBar axieHealth;

    public float countDown;
    public bool canCount;
    public GameObject axieObj;
    void OnEnable()
    {
        float hp = 500;
        float dmg = Random.Range(100, 301);
        enemy = new Enemy(hp, dmg-30);

        Debug.Log("Enemy dmg: " + dmg);
        previousState = "";
        currentState = "action/idle/normal";
        eneAni.state.SetAnimation(0, currentState, true);
        move = true;
    }
    public void SetEneAni(SkeletonAnimation ene)
    {
        eneAni = ene;
    }
    // Update is called once per frame
    void Update()
    {
        if (collisionPlayer == false)
        {
            if (move == true)
            {
                changeState("action/move-forward");
                gameObject.transform.position -= transform.right * speed * Time.deltaTime;
            }
        }
        if (enemy.HP <= 0)
        {
            changeState("action/idle/normal");
            Die();
        }
        if (collisionPlayer == true && canCount == true && axieObj != null)
        {
            countDown += 0.01f;
            if (countDown >= eneAni.state.GetCurrent(0).AnimationEnd)
            {
                Debug.Log("Enemy animation end: " + eneAni.state.GetCurrent(0).AnimationEnd);
                if (axieObj.GetComponent<AxieController>().data.hp > 0)
                {
                    axieObj.GetComponent<AxieController>().data.hp -= enemy.dmg;
                    axieHealth.OnHPChange(enemy.dmg);
                    Debug.Log("Enemy HP: -" + enemy.dmg);
                    countDown = 0;
                }
            }
        }
    }

    public void changeState(string newState)
    {
        if (newState != currentState && currentState != previousState)
        {
            Debug.Log(newState + "  :  " + currentState);
            previousState = currentState;
            currentState = newState;
            eneAni.state.SetAnimation(0, currentState, true);
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enter player");
        axieHealth = GameObject.Find("Axie Health Bar").GetComponent<HealthBar>();
        if (collision.collider.transform.gameObject.tag == "Player" && collision.collider.GetComponent<AxieController>().data.hp > 0)
        {
            countDown = 0;
            canCount = true;
            Debug.Log(collision.collider.tag);
            collisionPlayer = true;
            changeState("attack/melee/normal-attack");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Enter on collision stay player");
        if (collision.collider.transform.gameObject.tag == "Player" && collision.collider.GetComponent<AxieController>().data.hp > 0)
        {
            Debug.Log("Start fight with axie");
            StartFighting(collision.gameObject);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        collisionPlayer = false;
        StartFighting(null);
    }

    public void Die()
    {
        collisionPlayer = false;
        Debug.Log("Enemy died");
        eneAni.state.SetAnimation(0, "action/idle/normal", false);
        eneAni.GetComponent<MeshRenderer>().materials[0].SetColor("Tint", new Color(82, 69, 69, 255)); ;

        GameObject.Find("Game Manager").GetComponent<GameManager>().Won();
        Destroy(gameObject, 100);
    }

    public void StartFighting(GameObject axie)
    {
        axieObj = axie;
    }
}

