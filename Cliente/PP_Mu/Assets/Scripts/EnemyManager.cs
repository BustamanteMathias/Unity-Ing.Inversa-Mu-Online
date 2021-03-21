using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public int id;
    public float health;
    public float auxHealth;
    public float maxHealth = 100f;

    //Prefab Enemy Health
    public GameObject healthBarEnemy;

    //Prefab FloatingText
    public GameObject floatingText;

    public void Initialize(int _id)
    {
        id = _id;
        health = maxHealth;
        auxHealth = maxHealth;
        healthBarEnemy.GetComponentInChildren<HealthBarEnemy>().SetMaxHealthBar(health);
        healthBarEnemy.GetComponentInChildren<HealthBarEnemy>().SetNameText("Enemy: " + id.ToString());
    }

    public void SetHealth(float _health)
    {

        if (floatingText)
        {
            ShowDamage(_health);
        }        

        health = _health;
        healthBarEnemy.GetComponentInChildren<HealthBarEnemy>().SetHealthBar(_health);

        if (health <= 0f)
        {
            GameManager.enemies.Remove(id);
            Destroy(gameObject);
        }
    }

    public void ShowDamage(float _health)
    {
        float auxdmg = auxHealth - _health;

        GameObject G = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        G.GetComponent<TextMeshPro>().text = auxdmg.ToString();
        auxHealth = _health;
    }
}
