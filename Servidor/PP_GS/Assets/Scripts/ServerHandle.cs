using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} Se conecto exitosamente y ahora es el jugador {_fromClient}");

        if (_fromClient != _clientIdCheck)
        {
            Console.WriteLine($"Jugador \"{_username}\" (ID: {_fromClient}) inizio con el ID del cliente incorrecto ({_clientIdCheck}) ");

            //TODO: enviar al jugador al juego
        }

        Server.clients[_fromClient].SendIntoGame(_username);
    }


    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        bool[] _inputs = new bool[_packet.ReadInt()];

        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadBool();
        }

        Quaternion _rotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
    }

    public static void PlayerBasicAttack(int _fromClient, Packet _packet)
    {
        Vector3 _basicAttackDirection = _packet.ReadVector3();

        Server.clients[_fromClient].player.BasicAttack(_basicAttackDirection);
    }

    public static void PlayerSkillAttack(int _fromClient, Packet _packet)
    {
        Vector3 _skillAttackDirection = _packet.ReadVector3();

        Server.clients[_fromClient].player.SkillAttack(_skillAttackDirection);
    }
}
