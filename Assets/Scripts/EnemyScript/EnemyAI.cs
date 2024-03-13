using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;
    private bool enabled;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindObjectOfType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enabled = true;
    }

    private void Update()
    {
        if(enabled)
        {
            if (enemyAwareness.isAggro)
            {
                enemyNavMeshAgent.SetDestination(playersTransform.position);
            }
            else
            {
                enemyNavMeshAgent.SetDestination(transform.position);
            }
        }
        else
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        }
    }

    public void setEnable(bool yrn)
    {
        enabled = yrn;
    }

}


