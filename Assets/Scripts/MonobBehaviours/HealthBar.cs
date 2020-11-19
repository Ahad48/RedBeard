using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The health bar of the player
/// </summary>
public class HealthBar : MonoBehaviour
{
    // The hitpoints of the character
    public HitPoints hitPoints;

    // The player character it is set using the Player script 
    [HideInInspector]
    public Player character;

    // The slider of the helath bar
    public Slider healthSlider;

    // The TextmextProUGUI object
    public TextMeshProUGUI hpText;

    //public Text hpText;
    float maxHitPoints;

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

    /// <summary>
    /// Set the Fill of the health bar and change the HP value of the text
    /// </summary>
    void SetHealth()
    {
        //Slider is given a ratio so that value stays between 0 and 1
        float ratioHealthSlider = hitPoints.value / maxHitPoints;

        //Meter is from right to left so subtracting from 1
        healthSlider.value = 1 - ratioHealthSlider;

        //Setting the HP value in TextMeshPro
        hpText.text = "HP:" + (ratioHealthSlider * 100);
    }
}
