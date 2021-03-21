using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth;
    public MeshRenderer model;


    //Variable que se va a setear desde player pasandole el prefab de la barra de vida.
    public GameObject healthBar;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;
        healthBar.GetComponentInChildren<HealthBar>().SetMaxHealthBar(health);
        healthBar.GetComponentInChildren<HealthBar>().SetNameText(username);
    }

    public void SetHealth(float _health)
    {
        health = _health;
        healthBar.GetComponentInChildren<HealthBar>().SetHealthBar(_health);
        if (health <= 0f)
        {
            Die(); 
        }
    }

    public void Die()
    {
        model.enabled = false;
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }

    public void DropItem(int _itemID, Vector3 _posEnemy)
    {
        Instantiate(GameManager.instance.ITEMS[_itemID], _posEnemy, new Quaternion());
    }
}
