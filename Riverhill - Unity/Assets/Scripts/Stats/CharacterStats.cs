using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    public bool isEnemy;

    //Base Stats
    public string Name;

    public float BaseHP;
    public float CurrentHP;

    public float attack;
}
