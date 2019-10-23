using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    BattleManager battleManager;

    public CharacterState characterState;
    public bool hasCharacterState;
    public GameObject characterUI_Object;

    public CharacterUI(GameObject a_UIAsset)
    {
        battleManager = BattleManager.Instance;

        //characterUI_Object = a_UIAsset;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (characterState != null)
        {
            hasCharacterState = true;
        }
        else hasCharacterState = false;
    }

    public void AssignNewCharacter(CharacterState a_CharacterState)
    {
        characterState = a_CharacterState;
        Debug.Log("UI: Current Character: " + a_CharacterState.characterStats.Name);
    }

    #region Actions

    public void Moving()
    {
        characterState.moveSelected = true;
    }

    public void Attacking()
    {
        characterState.attackSelected = true;
    }

    // TODO: Implement Specials - CharacterUI
    public void Special()
    {
        characterState.specialSelected = true;
    }

    public void EndTurn()
    {
        characterState.endTurnSelected = true;
    }

    #endregion
}
