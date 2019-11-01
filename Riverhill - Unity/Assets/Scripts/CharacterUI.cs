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

    private void Update()
    {
        if (characterState != null)
        {
            hasCharacterState = true;
            gameObject.transform.position = characterState.character.transform.position;
        }
        else
        {
            hasCharacterState = false;
            gameObject.transform.position = new Vector3(int.MaxValue, int.MaxValue, 0);
        }
    }

    public void AssignNewCharacter(CharacterState a_CharacterState)
    {
        characterState = a_CharacterState;
        //Debug.Log("UI: Current Character: " + a_CharacterState.characterStats.Name);
    }

    #region Actions

    public void Moving()
    {
        Debug.Log("Move Button Selected");
        characterState.moveSelected = true;
    }

    public void Attacking()
    {
        Debug.Log("Attack Button Selected");
        characterState.attackSelected = true;
    }

    // TODO: Implement Specials - CharacterUI
    public void Special()
    {
        Debug.Log("Special Button Selected");
        characterState.specialSelected = true;
    }

    public void EndTurn()
    {
        Debug.Log("EndTurn Button Selected");
        characterState.endTurnSelected = true;
    }

    #endregion
}
