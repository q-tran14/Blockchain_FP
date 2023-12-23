using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentData", menuName = "ScriptableObjects/EnvironmentScriptableObject", order = 0)]
[System.Serializable]
public class Environment : ScriptableObject
{
    public List<Env> environmentList;
}
[System.Serializable]
 public class Env
{
    public Sprite back;
    public Sprite front;
}