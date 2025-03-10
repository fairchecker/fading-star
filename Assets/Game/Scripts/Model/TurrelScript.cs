using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurrelScript : MonoBehaviour
{
    [SerializeField] 
    int seconds = 50,
    maxHealth;
    Slider slider, hpSlider;
    TextMeshProUGUI text, textd;
    int moves;
    [SerializeField]
    float fullLoad = 5f,
    curLoad = 0f;
    private ParticleSystem
        win, prepare;
    private int health;
    private bool stop = false;
    private void Awake()
    {
        health = maxHealth;
        slider = GameObject.Find("Preparing").GetComponent<Slider>();
        hpSlider = GameObject.Find("HP").GetComponent<Slider>();
        text = slider.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        textd = hpSlider.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        moves = seconds * 50;
        win = transform.Find("win").GetComponent<ParticleSystem>();
        prepare = transform.Find("prep").GetComponent<ParticleSystem>();
    }


    private void Start()
    {
        StartCoroutine("updateState");
        hpSlider.value = health;
        textd.text = health.ToString() + "/" + maxHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            health--;
            hpSlider.value = health;
            textd.text = health.ToString() + "/" + maxHealth.ToString();
            if (health <= 0)
            {
                stop = true;
                
            }
        }    
    }

    IEnumerator updateState()
    {
        if (!stop) { 
        yield return new WaitForFixedUpdate();
        float pers = curLoad / fullLoad;
        if (pers >= 1)
        {
            text.text = "100%";
            win.gameObject.active = true;
        }
        else
            text.text = (pers * 100).ToString() + "%";
        curLoad += fullLoad / moves;
        slider.value = (float)pers;
        prepare.startSize = curLoad;
        if ((pers < 1))
        {
            StartCoroutine("updateState");
        }
        }
    }
}
