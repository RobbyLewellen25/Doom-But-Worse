using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impBulletScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindWithTag("Player");
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.DamagePlayer(10);
            Destroy(gameObject);
        }
    }
}
