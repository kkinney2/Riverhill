using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    Text textBox;
    string fullText = "";
    string displayedText;
    float textSpeed = 25f;
    GameController gameController;

    private void Awake()
    {
        textBox = GetComponent<Text>();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        fullText = textBox.text;
        displayedText = "";
    }

    void Start()
    {

    }

    private void Update()
    {
        if (gameController != null && gameController.cutsceneManager != null)
        {
            textSpeed = gameController.cutsceneManager.TextSpeed;
        }
        textBox.text = displayedText;
    }

    private void OnEnable()
    {
        StartCoroutine(DisplayText());
    }

    private void OnDisable()
    {
        StopCoroutine(DisplayText());
        if (textBox != null)
        {
            textBox.text = "";
        }
    }

    IEnumerator DisplayText()
    {
        while (gameController != null && gameController.cutsceneManager == null)
        {
            yield return new WaitForEndOfFrame();
        }

        while (fullText.Length < 1)
        {
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < fullText.Length; i++)
        {
            displayedText += fullText[i];

            if (fullText[i] == '.' || fullText[i] == '?' || fullText[i] == '!')
            {
                yield return new WaitForSeconds(1f / textSpeed);
            }

            yield return new WaitForSeconds(1f / textSpeed);
        }
        yield return null;
    }
}
