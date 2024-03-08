using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllignToPlayer : MonoBehaviour
{
    
    private Vector3 targetPos;
    private Transform player;
    private Vector3 targetDir;
    public float angle;
    public int lastIndex;

    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        targetDir = targetPos - transform.position;

        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        lastIndex = GetIndex(angle);
    }

    private int GetIndex(float angle)
    {
        //front
        if (angle > -22.5f && angle < 22.6f)
        {
            return 0;
        }
        if(angle >= 22.5f && angle < 67.5f)
        {
            return 7;
        }
        if(angle >= 67.5f && angle < 112.5f)
        {
            return 6;
        }
        if(angle >= 122.5f && angle < 157.5f)
        {
            return 5;
        }

        //back
        if(angle<= -157.5f || angle >= 157.5f)
        {
            return 4;
        }
        if(angle >= 22.5f && angle < 67.5f)
        {
            return 1;
        }
        if(angle >= 67.5f && angle < 112.5f)
        {
            return 2;
        }
        if(angle >= 122.5f && angle < 157.5f)
        {
            return 3;
        }







        return lastIndex;
    }

    void onDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPos);


    }
}
