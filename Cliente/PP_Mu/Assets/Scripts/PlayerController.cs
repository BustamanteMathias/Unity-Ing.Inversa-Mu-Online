using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    public GameObject miCamera;
    public Animator miAnimator;

    //Variable set camera.
    private Vector3 cameraPosition = new Vector3(0, 12, -8);

    //ataque
    public Transform camTransform;

    private void Awake()
    {
        miCamera = GameObject.FindGameObjectWithTag("MainCamera");

        camTransform = miCamera.transform;
        //miAnimator = GetComponentInChildren<Animator>();
        //miAnimator.SetBool("running", false);
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void Update()
    {

        //Si precionamos click izquierdo
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //El cliente le envia al servidor la direccion donde quiere golpear al player.
            ClientSend.PlayerBasicAttack(camTransform.forward);
        }

        //Si precionamos click derecho.
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //
            ClientSend.PlayerSkillAttack(camTransform.forward);
        }
        this.MoveCamera(this.cameraPosition);
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
        Input.GetKey(KeyCode.W),
        Input.GetKey(KeyCode.S),
        Input.GetKey(KeyCode.A),
        Input.GetKey(KeyCode.D),
        };

        //miAnimator.SetBool("running", false);
        //foreach (var item in _inputs)
        //{
        //    if (item)
        //    {
        //        miAnimator.SetBool("running", true);
        //        break;
        //    }
        //}
        //Si "W"
        if (_inputs[0] && _inputs[2] == false && _inputs[3] == false)
        {
            //Debug.Log("W");
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = rotation;
        }
        
        //Si "W y A"
        if (_inputs[0] && _inputs[2] && _inputs[3] == false)
        {
            //Debug.Log("WA");
            Quaternion rotation = Quaternion.Euler(0, 315, 0);
            this.transform.rotation = rotation;
        }
        
        //Si "W y D"
        if (_inputs[0] && _inputs[3] && _inputs[2] == false)
        {
            //Debug.Log("WD");
            Quaternion rotation = Quaternion.Euler(0, 45, 0);
            this.transform.rotation = rotation;
        }
        
        //Si "S"
        if (_inputs[1] && _inputs[2] == false && _inputs[3] == false)
        {
            //Debug.Log("S");
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            this.transform.rotation = rotation;
        }
        
        //Si "S y A"
        if (_inputs[1] && _inputs[2] && _inputs[3] == false)
        {
            //Debug.Log("SA");
            Quaternion rotation = Quaternion.Euler(0, 225, 0);
            this.transform.rotation = rotation;
        }
        
        //Si "S y D"
        if (_inputs[1] && _inputs[3] && _inputs[2] == false)
        {
            //Debug.Log("SD");
            Quaternion rotation = Quaternion.Euler(0, 135, 0);
            this.transform.rotation = rotation;
        }
        
        //Si "A"
        if (_inputs[2] && _inputs[0] == false && _inputs[1] == false)
        {
            //Debug.Log("A");
            Quaternion rotation = Quaternion.Euler(0, 270, 0);
            this.transform.rotation = rotation;
        }
        
        //Si "D"
        if (_inputs[3] && _inputs[0] == false && _inputs[1] == false)
        {
            //Debug.Log("D");
            Quaternion rotation = Quaternion.Euler(0, 90, 0);
            this.transform.rotation = rotation;
        }

        ClientSend.PlayerMovement(_inputs);
    }
    void MoveCamera(Vector3 cameraPosition)
    {
        this.miCamera.transform.position = this.transform.position + cameraPosition;
        this.miCamera.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

}
