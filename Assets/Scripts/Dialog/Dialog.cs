using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Dialog
{
    public Story story;
    public void LoadStory(string path)
    {
        TextAsset inkJSONAsset = Resources.Load<TextAsset>(path);
        if (inkJSONAsset != null)
        {
            story = new Story(inkJSONAsset.text);
        }
        else
        {
            Debug.LogError("Не найден ink JSON файл по пути: " + path);
        }
    }
    public string ContinueStory()
    {
        if (story != null && story.canContinue)
        {
            return story.Continue();
        }
        if (story.currentChoices.Count != 0)
        {
            return "CHOISE";
        }
        Debug.LogError("ДЕРЬМО");
        return string.Empty;
    }
    public List<Choice> GetChoices()
    {
        if (story != null)
        {
            return story.currentChoices;
        }
        return new List<Choice>();
    }
    public void ChooseChoice(int index)
    {
        if (story != null && index >= 0 && index < story.currentChoices.Count)
        {
            story.ChooseChoiceIndex(index);
        }
        else
        {
            Debug.LogError("Неверный индекс выбора: " + index);
        }
    }
}
