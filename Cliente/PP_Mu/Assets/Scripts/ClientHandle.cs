using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    #region Player
    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
        {
            _player.transform.position = _position;
        }

    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
        {
            _player.transform.rotation = _rotation;
        }

    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_health);
    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].Respawn();
    }
    #endregion

    #region Enemy
    public static void SpawnEnemy(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
    Vector3 _position = _packet.ReadVector3();
    GameManager.instance.SpawnEnemy(_enemyId, _position);
    }

    public static void EnemyPosition(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
    
        if (GameManager.enemies.TryGetValue(_enemyId, out EnemyManager _enemy))
            _enemy.transform.position = _position;
    }
    
    public static void EnemyHealth(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        float _health = _packet.ReadFloat();
        GameManager.enemies[_enemyId].SetHealth(_health);
    }
    
    public static void EnemyDropItem(Packet _packet)
    {
        int _id = _packet.ReadInt();
        int _itemID = _packet.ReadInt();
        Vector3 _positionEnemy = _packet.ReadVector3();
    
        GameManager.players[_id].DropItem(_itemID, _positionEnemy);
    }
    #endregion

    #region NPC
    public static void NPCSpawn(Packet _packet)
    {
        try
        {
            int _npcId = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();
            GameManager.instance.SpawnNPC(_npcId, _position);
        }
        catch (Exception e)
        {
            Debug.Log(e + ", NPC SPAWN");
        }
    }

    public static void NPCPosition(Packet _packet)
    {
        try
        {
            int _npcId = _packet.ReadInt();
            Vector3 _position = _packet.ReadVector3();

            if (GameManager.npcs.TryGetValue(_npcId, out NpcManager _npc))
            {
                _npc.transform.position = _position;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e + ", NPC POSITION");
        }
    }
    public static void NPCRotation(Packet _packet)
    {
        try
        {
            int _id = _packet.ReadInt();
            Quaternion _rotation = _packet.ReadQuaternion();

            if (GameManager.npcs.TryGetValue(_id, out NpcManager _npc))
            {
                _npc.transform.rotation = _rotation;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e + ", NPC POSITION");
        }
    }
    #endregion
}
