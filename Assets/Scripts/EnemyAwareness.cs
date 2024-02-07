using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public Material aggroMat;
    public bool isAggro;

    void Update()
    {
        if (isAggro)
        {
            GetComponent<MeshRenderer>().material = aggroMat;
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
