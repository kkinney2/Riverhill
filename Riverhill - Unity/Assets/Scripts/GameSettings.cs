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

    public int FramerateTarget = 60;
    public int TextSpeed = 5;
    // TODO: GameSettings 

    private void Start()
    {
        Application.targetFrameRate = FramerateTarget;
    }
}
