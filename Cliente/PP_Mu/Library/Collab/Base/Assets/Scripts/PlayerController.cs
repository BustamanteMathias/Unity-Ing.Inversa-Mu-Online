using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator mAnimator; 
    public GameObject miCamera;

    private Vector3 v3 = new Vector3(0, 12, -8);

    private void Awake()
    {
        miCamera = GameObject.Find("Camera");
        mAnimator = GetComponent<Animator>();
        mAnimator.SetBool("running", false);
    }

    private void Update()
    {
        this.MoveCamera(this.v3);
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        //sadasd
        bool[] _inputs = new bool[]
        {
        Input.GetKey(KeyCode.W),
        Input.GetKey(KeyCode.S),
        Input.GetKey(KeyCode.A),
        Input.GetKey(KeyCode.D)
        };

        mAnimator.SetBool("running", false);
        foreach (var item in _inputs)
        {            
            if (item)
            {
                mAnimator.SetBool("running", true);
                break;
            }
        }
        ClientSend.PlayerMovement(_inputs);
    }
    void MoveCamera(Vector3 v3)
    {
        this.miCamera.transform.position = this.transform.position + v3;
    }
}
