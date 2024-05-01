using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private Animator spriteAnim;
    private ProjectileAllignToPlayer allignToPlayer;

    public GameObject spawner; // The object that spawned this bullet

    private void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        allignToPlayer = GetComponent<ProjectileAllignToPlayer>();
    }

    void Update()
    {
        spriteAnim.SetFloat("SpriteRot", allignToPlayer.lastIndex);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != spawner) // The detected object is not the spawner
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                playerHealth.DamagePlayer(10);
                Destroy(gameObject);
            }
            else if (other.gameObject.tag == "Enemy")
            {
                Enemy enemyHealth = other.gameObject.GetComponent<Enemy>();
                enemyHealth.TakeDamage(10);
                Destroy(gameObject);
            }
        }
    }
}