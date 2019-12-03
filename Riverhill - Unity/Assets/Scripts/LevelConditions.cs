using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditions : MonoBehaviour
{
    public string levelName = ""; // Probs could/should be an enum

    [Header("Tutorial Switches")]
    public bool tutorial_firstAttack = false;
    bool toggle_firstAttack = true;
    public bool tutorial_firstMovement = false;
    bool toggle_firstMovement = true;
    public bool tutorial_firstWasHit = false;
    bool toggle_firstWasHit = true;
    public bool tutorial_DayanaDies = false;
    bool toggle_DayanaDies = true;
    public bool tutorial_AlyssDies = false;
    bool toggle_AlyssDies = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (levelName)
        {
            case "Tutorial":
                #region Tutorial Cutscenes
                if (tutorial_firstAttack && toggle_firstAttack)
                {
                    CutsceneManager.Instance.StartCutscene("Tutorial - player hits Dayana");
                    toggle_firstAttack = false;
                }
                if (tutorial_firstMovement && toggle_firstMovement)
                {
                    CutsceneManager.Instance.StartCutscene("Tutorial - player moves");
                    toggle_firstMovement = false;
                }
                if (tutorial_firstWasHit && toggle_firstWasHit)
                {
                    CutsceneManager.Instance.StartCutscene("Tutorial - Dayana hits player");
                    toggle_firstWasHit = false;
                }
                if (tutorial_DayanaDies && toggle_DayanaDies)
                {
                    CutsceneManager.Instance.StartCutscene("Tutorial - Dayana is defeated");
                    toggle_DayanaDies = false;

                }
                if (tutorial_AlyssDies && toggle_AlyssDies)
                {
                    CutsceneManager.Instance.StartCutscene("Tutorial - Alyss is defeated");
                    toggle_AlyssDies = false;
                }
                #endregion
                break;

            default:
                break;
        }
    }
}
