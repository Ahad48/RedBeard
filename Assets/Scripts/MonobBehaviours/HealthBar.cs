using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public HitPoints hitPoints;
    public Player character;
    public Slider healthSlider;
    public TextMeshProUGUI hpText;

    //public Text hpText;
    float maxHitPoints;

    //Slider is given a ratio so that value stays between 0 and 1
    float ratioHealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        maxHitPoints = character.maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (character != null)
        {
            SetHealth();
        }
    }

    void SetHealth()
    {
        ratioHealthSlider = hitPoints.value / maxHitPoints;

        //Meter is from right to left so subtracting from 1
        healthSlider.value = 1 - ratioHealthSlider;

        //Setting the HP value in TextMeshPro
        hpText.text = "HP:" + (ratioHealthSlider * 100);
    }
}
