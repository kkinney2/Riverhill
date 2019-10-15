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
    // TODO: GameSettings 

    private void Start()
    {
        Application.targetFrameRate = FramerateTarget;
    }
}
