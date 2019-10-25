using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    #region Singleton
    private static GameSettings _instance;

    public static GameSettings Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    [Tooltip("Sets Target Framerate")]
    public int FramerateTarget = 60;

    [Range(1,45)]
    [Tooltip("Controls text speed based on a 1/x delay between characters and a 2/x delay on punctuation")]
    public int TextSpeed = 15;

    [Tooltip("Controls characters' move speed")]
    public int CharacterMoveSpeed = 5;

    [Tooltip("Sets max action count per character per turn")]
    public int MaxActionCount = 2;
    // TODO: GameSettings 

    public GameObject tileHighlight_Positive;
    public GameObject tileHighlight_Negative;

    private void Start()
    {
        Application.targetFrameRate = FramerateTarget;

        tileHighlight_Positive = Instantiate(tileHighlight_Positive);
        tileHighlight_Negative = Instantiate(tileHighlight_Negative);

        tileHighlight_Positive.SetActive(false);
        tileHighlight_Negative.SetActive(false);
    }
}
