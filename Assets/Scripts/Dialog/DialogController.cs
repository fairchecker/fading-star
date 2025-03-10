using Ink.Parsed;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    List<GameObject> bt_obj = new List<GameObject>();
    private TextScript textScript;
    private Dialog dialog = new Dialog();

    private void Awake()
    {
        textScript = GetComponent<TextScript>();
    }

    private void Start()
    {
        StartDialog();
    }

    public void StartDialog(string dialog_file = "test")
    {
        dialog.LoadStory("Dialogs/" + dialog_file);
        string text = dialog.ContinueStory();
        switch (text)
        {
            case "CHOISE":
                Choise();
                break;
            default:
                textScript.SayText(text);
                break;
        }
    }
    public void NextText()
    {
        if (!textScript.textcomplete) { textScript.speed_up = true; return; }
        string text = dialog.ContinueStory();
        switch (text)
        {
            case "CHOISE":
                Choise();
                break;
            default:
                textScript.SayText(text);
                break;
        }
    }

    private void Choise()
    {
        if (bt_obj.Count != 0) return;
        List<Ink.Runtime.Choice> options = dialog.GetChoices();
        for (int i = 0; i < options.Count; i++)
        {
            var text = options[i].text;
            float position = -112.5f + i * 75;
            CreateChoiceButton(text, position, i);
        }
    }

    private GameObject CreateChoiceButton(string text, float position, int id)
    {
        var button = new GameObject();
        button.transform.SetParent(transform);
        button.AddComponent<RectTransform>();
        button.transform.localPosition = new Vector3(800, position, 0);
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 6);
        button.AddComponent<Image>(); 
        button.AddComponent<Button>();
        button.GetComponent<Image>().sprite = Resources.Load<Sprite>("");
        var textObject = new GameObject();
        textObject.transform.SetParent(button.transform);
        textObject.AddComponent<TextMeshProUGUI>();
        textObject.GetComponent<TextMeshProUGUI>().text = text;
        textObject.transform.localPosition = Vector3.zero;
        textObject.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 5);
        textObject.GetComponent<TextMeshProUGUI>().color = Color.black; 
        textObject.GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
        textObject.GetComponent<TextMeshProUGUI>().fontSizeMin = 0;
        var bt = button.GetComponent<Button>();
        bt.onClick.AddListener(() => GetResult(id));
        bt_obj.Add(button);
        return button;
    }
    void GetResult(int id)
    {
        foreach (var gameObject in bt_obj)
        Destroy(gameObject);
        dialog.ChooseChoice(id);
        NextText();
    }
}
