using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToMove : MonoBehaviour
{

    private Animator mAnimator;
    private UnityEngine.AI.NavMeshAgent mNavMeshAgent;

    private bool mRunning = false;

    public GameObject miCamera;

    private Vector3 v3 = new Vector3(0, 12, -8);
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        miCamera = GameObject.Find("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        this.MoverPersonaje();

        this.MoveCamera(this.v3);
    }

    void MoverPersonaje()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                mNavMeshAgent.destination = hit.point;
            }
        }

        if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
        {
            mRunning = false;
        }
        else
        {
            mRunning = true;
        }

        mAnimator.SetBool("running", mRunning);
    }

    void MoveCamera(Vector3 v3)
    {
        this.miCamera.transform.position = this.transform.position + v3;
    }
}
