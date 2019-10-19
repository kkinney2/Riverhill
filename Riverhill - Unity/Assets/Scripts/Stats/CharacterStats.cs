using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    public bool isEnemy;

    //Base Stats
    [HideInInspector] // Name will be given when created based on gameobject
    public string Name;

    public float BaseHP;
    public float CurrentHP;

    public float attack;
}
