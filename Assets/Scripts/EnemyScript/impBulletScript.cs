using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impBulletScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.gameObject.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
