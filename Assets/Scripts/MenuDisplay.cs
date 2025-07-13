using System.Collections;
using TMPro;
using UnityEngine;
[System.Serializable]
public struct DisplayMessage
{
    public string text;
    public float delayToNext;
}
public class MenuDisplay : MonoBehaviour
{
    public DisplayMessage[] texts;
    public TMP_Text displayText;
    public float typingSpeed = 0.075f;
    [TextArea(5, 25)]
    public string howToPlay;
    void Start()
    {
        displayText.text = "";
        StartCoroutine(writeAll(typingSpeed));
    }
    public IEnumerator writeAll(float speed = 0)
    {
        foreach (DisplayMessage message in texts)
        {
            yield return writeText(message.text + "\n", speed);
            yield return new WaitForSeconds(message.delayToNext);
        }
    }
    public IEnumerator writeText(string text, float typingSpeed)
    {
        for (int i = 0; i < text.Length; i++)
        {
            char character = text[i];
            displayText.text += character;
            if (character.ToString() != "\\")
                yield return new WaitForSeconds(typingSpeed);
        }
        yield return null;
    }
    public void ResetView()
    {
        StopAllCoroutines();
        displayText.text = "";
        foreach (DisplayMessage message in texts)
        {
            displayText.text += message.text + "\n";
        }
    }
    public void HowToPlay()
    {
        StopAllCoroutines();
        displayText.text = "";
        StartCoroutine(writeText(howToPlay, typingSpeed));
    }
}
