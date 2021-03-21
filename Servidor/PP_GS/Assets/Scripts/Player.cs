using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public CharacterController controller;
    public float gravity = -9.81f;
    private float moveSpeed = 5f;

    public GameObject InstanciaSkill;
    public GameObject Skill_TEST;

    //CLICKS
    private bool click_0 = false;
    private bool skill = false;
    //

    //attack
    public Transform basicAttackOrigin;

    public float health;
    public float maxHealth = 10000f;

    //Inputs = Teclas
    private bool[] inputs;

    private void Start()
    {
        gravity *= Time.fixedDeltaTime * Time.fixedDeltaTime;
        moveSpeed *= Time.fixedDeltaTime;
    }
    public void Initialize(int _id, string _username)
    {
        //Set Id
        id = _id;

        //Set Username
        username = _username;

        //Seteo la vida del player con su vida maxima.
        this.health = this.maxHealth;

        //Inputs "w" "s" "a" "d"
        inputs = new bool[4];
    }

    public void Update()
    {
        this.DireccionSet();
    }

    //Seteo de direcciones y movimiento.
    private void DireccionSet()
    {
        if(health <= 0f)
        {
            return;
        }

        Vector2 _inputDirection = Vector2.zero;

        //Si "W"
        if (inputs[0] && inputs[2] == false && inputs[3] == false)
        {
            _inputDirection.y += 1;
        }

        //Si "W y A"
        if (inputs[0] && inputs[2] && inputs[3] == false)
        {
            _inputDirection.y += 1f;
        }

        //Si "W y D"
        if (inputs[0] && inputs[3] && inputs[2] == false)
        {
            _inputDirection.y += 1f;
        }

        //Si "S"
        if (inputs[1] && inputs[2] == false && inputs[3] == false)
        {
            _inputDirection.y += 1;
        }

        //Si "S y A"
        if (inputs[1] && inputs[2] && inputs[3] == false)
        {
            _inputDirection.y += 1;
        }

        //Si "S y D"
        if (inputs[1] && inputs[3] && inputs[2] == false)
        {
            _inputDirection.y += 1;
        }

        //Si "A"
        if (inputs[2] && inputs[0] == false && inputs[1] == false)
        {
            _inputDirection.y += 1;
        }

        //Si "D"
        if (inputs[3] && inputs[0] == false && inputs[1] == false)
        {
            _inputDirection.y += 1;
        }

        Move(_inputDirection);
    }

    private void Move(Vector2 _inputDirection)
    {
        Vector3 _moveDirection = transform.right * _inputDirection.x + transform.forward * _inputDirection.y;
        _moveDirection *= moveSpeed;

        controller.Move(_moveDirection);

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        transform.rotation = _rotation;

    }

    //Funcion de ataque basico
    public void BasicAttack(Vector3 _viewDirection)
    {
        this.click_0 = true;
        StartCoroutine(SetClick0False());

    }

    public void SkillAttack(Vector3 _viewDirection)
    {
        //Seteo click secundario true
        this.skill = true;
        //Instacio skill en player
        GameObject auxSkill = Instantiate(this.Skill_TEST, this.InstanciaSkill.transform);
        //Destruyo Skill
        StartCoroutine(SkillDestroy(auxSkill));
    }

    //Realizar daño y acciones si muere.
    public void TakeDamage(float _damage)
    {
        if(health <= 0f)
        {
            return;
        }

        health -= _damage;
        if(health <= 0f)
        {
            health = 0f;
            controller.enabled = false;
            
            //Coordenadas de respawn
            transform.position = new Vector3(0f, 1f, 0f);

            //Envio al serveidor.
            ServerSend.PlayerPosition(this);
            StartCoroutine(Respawn());
        }

        ServerSend.PlayerHealth(this);
    }

    /// Funcion que espera 5 segundos para realizar un respawn.
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);

        health = maxHealth;
        controller.enabled = true;
        ServerSend.PlayerRespawned(this);
    }

    private IEnumerator SetClick0False()
    {
        yield return new WaitForSeconds(0.25f);
        this.click_0 = false;
    }

    private IEnumerator SkillDestroy(GameObject skill)
    {
        yield return new WaitForSeconds(0.75f);
        this.skill = false;
        Destroy(skill);
    }

    //TRIGGERS

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                if (click_0)
                {
                    other.GetComponent<Enemy>().SetPlayersDamage(this.id);
                    other.GetComponent<Player>().TakeDamage(10f);
                }
                if (skill)
                {
                    other.GetComponent<Enemy>().SetPlayersDamage(this.id);
                    other.GetComponent<Player>().TakeDamage(25f);
                }
                break;

            case "Enemy":
                if (click_0)
                {
                    other.GetComponent<Enemy>().SetPlayersDamage(this.id);
                    other.GetComponent<Enemy>().TakeDamage(8f);
                }
                if (skill)
                {
                    other.GetComponent<Enemy>().SetPlayersDamage(this.id);
                    other.GetComponent<Enemy>().TakeDamage(5f);
                }
                break;

            default:
                this.click_0 = false;
                break;
        }

        this.click_0 = false;
    }
}
