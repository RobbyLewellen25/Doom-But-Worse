using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;
    private Enemy enemy;
    private bool isEnabled;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindObjectOfType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        isEnabled = true;
    }

    private void Update()
    {
        if(isEnabled)
        {
            if (enemyAwareness.isAggro && enemy.isDead() == false)
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
        isEnabled = yrn;
    }

}


