using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    /*
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
    */

    GameController gameController;

    public bool canSkipCutscenes = false;

    [Tooltip("Sets Target Framerate")]
    public int FramerateTarget = 60;

    [Range(1, 45)]
    [Tooltip("Controls text speed based on a 1/x delay between characters and a 2/x delay on punctuation")]
    public int TextSpeed = 15;

    [Tooltip("Controls characters' move speed")]
    public int CharacterMoveSpeed = 5;

    [Tooltip("Sets max action count per character per turn")]
    public int MaxActionCount = 2;

    public GameObject tileHighlight_Positive;
    public GameObject tileHighlight_Negative;

    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            gameController.gameSettings = this;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = FramerateTarget;
    }

    private void Update()
    {
        if (gameController != null && gameController.cutsceneManager != null)
        {
            gameController.cutsceneManager.TextSpeed = TextSpeed;
        }
    }
}
