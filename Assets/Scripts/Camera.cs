using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;

    // Update is called once per frame
    private void Update()
    {
        if (target.position.y >= 0)
        {
            Vector3 newPosition = transform.position + Vector3.up * 0.5f * Time.deltaTime;
            transform.position = newPosition;
        }
       
    }

    void LateUpdate()
    {
        if (target.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
            
    }
}
