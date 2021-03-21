using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcManager : MonoBehaviour
{
    public int id;
    public string name;

    //Prefab FloatingText
    public Text NameTxt;

    public void Initialize(int _id)
    {
        id = _id;
        NameTxt.text = "NPC: " + id.ToString() + " Name: " + name;
    }
}
