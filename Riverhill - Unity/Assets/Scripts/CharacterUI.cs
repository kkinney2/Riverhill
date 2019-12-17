using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    BattleManager battleManager;

    public CharacterState characterState;
    public bool hasCharacterState;
    public GameObject characterUI_Object;

    public Canvas characterUIObjCanvas;
    public GameObject lookToPlayerObject;
    public CharacterPathfinding charPathfindingAlyss;

    public Button move_Button;
    public Button attack_Button;
    public Button special_button;

    public Text specialText;

    public AudioSource audioSourceUI;
    public AudioClip UIbuttonClick;

    public CharacterUI(GameObject a_UIAsset, BattleManager battleM)
    {
        battleManager = battleM;

        //characterUI_Object = a_UIAsset;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSourceUI = GetComponent<AudioSource>();

        if (lookToPlayerObject.name.Contains("Alyss"))
        {
            specialText.text = "Healing Aura";
        }
    }

    private void Update()
    {
        if (lookToPlayerObject.name.Contains("Alyss"))
        {
            specialText.text = "Healing Aura";
        }
        if (lookToPlayerObject.name.Contains("Dayana"))
        {
            specialText.text = "Knockback";
        }
        /*
        if (lookToPlayerObject.name.Contains("Nelson"))
        {
            specialText.text = "Long Range";
        }
        */

        //Debug.Log("actionCount" + characterState.actionCount);
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
        /*
        if (charPathfindingAlyss.isPerformingMove == false)
        {
            if(characterState.nowSwitchTurns == true)
            {
                characterUIObjCanvas.enabled = false;
            }
            characterUIObjCanvas.enabled = true;
        }
        */
        if (charPathfindingAlyss.isPerformingMove == true)
        {
            characterUIObjCanvas.enabled = false;
        }

    }

    public void AssignNewCharacter(CharacterState a_CharacterState)
    {
        //characterUIObjCanvas.enabled = false;
        characterState = a_CharacterState;
        //Debug.Log("UI: Current Character: " + a_CharacterState.characterStats.Name);
    }

    #region Actions

    public void Moving()
    {
        audioSourceUI.clip = UIbuttonClick;
        audioSourceUI.Play();
        Debug.Log("Move Button Selected");
        if (!characterState.hasActiveAction || characterState.battleStateMachine.IsInState(characterState.state_CharacterAction))
        {
            characterState.moveSelected = true;
        }
        else
        {
            characterState.battleStateMachine.ChangeState(characterState.state_Idle);
        }
        
    }

    public void Attacking()
    {
        audioSourceUI.clip = UIbuttonClick;
        audioSourceUI.Play();
        Debug.Log("Attack Button Selected");
        if (!characterState.hasActiveAction || characterState.battleStateMachine.IsInState(characterState.state_CharacterAction))
        {
            characterState.attackSelected = true;
        }
        else
        {
            characterState.battleStateMachine.ChangeState(characterState.state_Idle);
        }
    }

    // TODO: Implement Specials - CharacterUI
    public void Special()
    {
        audioSourceUI.clip = UIbuttonClick;
        audioSourceUI.Play();
        StartCoroutine(SpecialSound());
    }

    private IEnumerator SpecialSound()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Special Button Selected");
        if (!characterState.hasActiveAction || characterState.battleStateMachine.IsInState(characterState.state_CharacterAction))
        {
            characterState.specialSelected = true;
        }
        else
        {
            characterState.battleStateMachine.ChangeState(characterState.state_Idle);
        }
    }

    public void EndTurn()
    {
        audioSourceUI.clip = UIbuttonClick;
        audioSourceUI.Play();
        StartCoroutine(EndTurnSound());
    }

    private IEnumerator EndTurnSound()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("EndTurn Button Selected");
        characterState.endTurnSelected = true;
    }

        #endregion
    }
