using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefab : MonoBehaviour
{
    public float itemSpeedRotate = 50f;
    //public float itemBobSpeed = 2f;
    //private Vector3 basePosition;

    private void Update()
    {
        transform.Rotate(Vector3.up, itemSpeedRotate * Time.deltaTime, Space.World);
        //transform.position = basePosition + new Vector3(0f, 0.25f * Mathf.Sin(Time.time + itemBobSpeed), 0f);
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
