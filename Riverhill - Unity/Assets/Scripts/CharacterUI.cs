using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    BattleManager battleManager;

    public CharacterState characterState;
    public GameObject characterUI_Object;

    //UI button options
    public bool moveSelected = false;
    public bool attackSelected = false;
    public bool specialSelected = false;
    public bool endTurnSelected = false; //essentially a pass option

    public CharacterUI(GameObject a_UIAsset)
    {
        battleManager = BattleManager.Instance;

        characterUI_Object = a_UIAsset;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AssignNewCharacter(CharacterState a_CharacterState)
    {
        characterState = a_CharacterState;
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
        endTurnSelected = true;
        Debug.Log("ET: " + endTurnSelected);
        //this.battleStateMachine.UpdateState(); //Unity crashes?
        characterState.battleStateMachine.ChangeState(characterState.state_Idle);
        battleManager.nextCharacter = true;
        ResetSelected();
    }

    #endregion

    private void ResetSelected()
    {
        moveSelected = false;
        attackSelected = false;
        specialSelected = false;
    }
}
