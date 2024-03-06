using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public float awarenessRadius = 8f;

    public bool isAggro;
    private Transform playersTransform;


    private void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
    }

    void Update()
    {
        var dist = Vector3.Distance(transform.position, playersTransform.position);

        if (dist < awarenessRadius)
        {
            isAggro = true;
        }


        if (isAggro)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            isAggro = true;
        }
    }
}
