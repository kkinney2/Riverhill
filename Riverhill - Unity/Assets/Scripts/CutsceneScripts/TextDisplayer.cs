using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    Text textBox;
    string fullText = "";
    string displayedText;

    private void Awake()
    {
        textBox = GetComponent<Text>();

        fullText = textBox.text;
        displayedText = "";
    }

    void Start()
    {
        
    }

    private void Update()
    {
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
        if (fullText.Length < 1)
        {
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < fullText.Length; i++)
        {
            displayedText += fullText[i];

            if (fullText[i] == '.' || fullText[i] == '?' || fullText[i] == '!')
            {
                yield return new WaitForSeconds(1f / CutsceneManager.Instance.TextSpeed);
            }

            yield return new WaitForSeconds(1f / CutsceneManager.Instance.TextSpeed);
        }
        yield return null;
    }
}
