using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarEnemy: MonoBehaviour
{
    public Slider slider;

    public Text monsterName;

    public void SetHealthBar(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealthBar(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //vamos a utilizar el mismo script para insertar el player name
    public void SetNameText(string name)
    {
        monsterName.text = name;
    }
}
