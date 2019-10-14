using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour
{
    Text textBox;
    string fullText = "";
    string displayedText;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponent<Text>();

        fullText = textBox.text;   
        displayedText = "";
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
        textBox.text = "";
    }

    IEnumerator DisplayText()
    {
        if (fullText.Length < 1)
        {
            yield return new WaitForEndOfFrame();
            //TODO: Why does DisplayText have to wait for fulltext?
        }

        for (int i = 0; i < fullText.Length; i++)
        {
            displayedText += fullText[i];

            //yield return new WaitForSeconds(1f / GameSettings.Instance.TextSpeed);
        }
        yield return null;
    }
}
