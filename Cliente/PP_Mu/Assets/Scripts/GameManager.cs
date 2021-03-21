using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, EnemyManager> enemies = new Dictionary<int, EnemyManager>();
    public static Dictionary<int, NpcManager> npcs= new Dictionary<int, NpcManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject npcPrefab;
    //ITEMS
    public GameObject[] ITEMS;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Debug.Log("Una instancia ya existe, destruyendo el objeto");
            Destroy(this);
        }
    }


    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void SpawnEnemy(int _id, Vector3 _position)
    {
        GameObject _enemy = Instantiate(enemyPrefab, _position, Quaternion.identity);
        _enemy.GetComponent<EnemyManager>().Initialize(_id);
        enemies.Add(_id, _enemy.GetComponent<EnemyManager>());
    }

    public void SpawnNPC(int _id, Vector3 _position)
    {
        try
        {
            GameObject _npc = Instantiate(npcPrefab, _position, Quaternion.identity);
            _npc.GetComponent<NpcManager>().Initialize(_id);
            npcs.Add(_id, _npc.GetComponent<NpcManager>());
        }
        catch (Exception e)
        {
            Debug.Log(e + ", NPC SPAWN");
        }
    }
}
