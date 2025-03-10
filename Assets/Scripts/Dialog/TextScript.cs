using UnityEngine;
using TMPro;
using System.Collections;


public class TextScript : MonoBehaviour
{
    public bool textcomplete = true;
    public bool speed_up = false;
    private TextMeshProUGUI TextObject;
    private void Awake()
    {
        TextObject = transform.Find("Panel").Find("Text").GetComponent<TextMeshProUGUI>();
    }
    public void SayText(string text = "")
    {
        TextObject.text = "";
        textcomplete = false;
        StartCoroutine("AddText", text);
    }
    
    private IEnumerator AddText(string text)
    {
        if (!speed_up)
        yield return new WaitForFixedUpdate();
        TextObject.text += text[0];
        if (text.Length != 1)
            StartCoroutine("AddText", text.Substring(1));
        else { 
            textcomplete = true;
            speed_up = false;
        }
    }
}
