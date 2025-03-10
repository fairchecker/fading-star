using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlidersController : MonoBehaviour
{
    private Image hp_bar, mana_bar, stamina_bar;
    // TODO : Сделать взятие максимального хп у персонажа
    private float max_hp = 100, max_mana = 10, max_stamina = 50;
    private void Awake()
    {
        hp_bar = transform.Find("HP_Slider").Find("Bar").GetComponent<Image>();
        mana_bar = transform.Find("MANA_Slider").Find("Bar").GetComponent<Image>();
        stamina_bar = transform.Find("STAMINA_Slider").Find("Bar").GetComponent<Image>();
    }

    private void Start()
    {
        EditSlider("HP", 50);
        EditSlider("MANA", 3);
        EditSlider("STAMINA", 5);
    }

    public void EditSlider (string name, float new_state)
    {
        (Image image, float state) a;
        a.state = new_state;
        a.state /= (name == "HP") ? max_hp : (name == "MANA") ? max_mana : max_stamina;
        a.image = (name == "HP") ? hp_bar : (name == "MANA") ? mana_bar : stamina_bar;
        StartCoroutine("edit", a);
    }

    IEnumerator edit((Image image, float state) tuple)
    {
        yield return new WaitForFixedUpdate();
        if ((int)(tuple.Item1.fillAmount*100/1) != (int)(tuple.Item2*100/1)) { 
            tuple.Item1.fillAmount += (tuple.Item1.fillAmount < tuple.Item2) ? 0.01f : -0.01f;
            StartCoroutine("edit", tuple);
        }
    }
}
