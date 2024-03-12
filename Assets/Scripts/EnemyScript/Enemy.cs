    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    public float enemyHealth = 2f;
    private Animator spriteAnim;
    private AllignToPlayer allignToPlayer;


    public GameObject gunHitEffect;

    private void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        allignToPlayer = GetComponent<AllignToPlayer>();

        enemyManager = FindObjectOfType<EnemyManager>();
    }

    void Update()
    {
        spriteAnim.SetFloat("SpriteRotation", allignToPlayer.lastIndex);


        if (enemyHealth <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
    }
}
