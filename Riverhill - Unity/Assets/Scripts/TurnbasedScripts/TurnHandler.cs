using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnHandler
{
    public string Attacker; //name of attacker, who attacked
    public GameObject attackerGO; //who's attacking
    public GameObject attacksTarget; //who's the target of attack
}
