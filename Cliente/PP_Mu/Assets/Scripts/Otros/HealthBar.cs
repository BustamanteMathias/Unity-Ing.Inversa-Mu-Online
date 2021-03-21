using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text username;

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
        username.text = name;
    }

}
