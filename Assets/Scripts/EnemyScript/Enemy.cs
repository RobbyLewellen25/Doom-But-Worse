using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;

    public float enemyHealth = 2f;
    private Animator spriteAnim;

    private AllignToPlayer allignToPlayer;
    private EnemyAI ai;
    public int reactionTimeDef = 12;
    private int reactionTime;

    private EnemyAwareness enemyAwareness;
    private Transform playerTransform;

    public bool hasMelee;
    public bool hasMissile;
        public float missileSpeed = 1000f;

    private bool staggd;
    private float distance;


    public GameObject missile;
    public GameObject gunHitEffect;
    public Transform spawnPoint;

    private float randomFloat;


    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks); // Seed the random number generator
        randomFloat = Random.Range(0f, 1f);

        spriteAnim = GetComponentInChildren<Animator>();
        allignToPlayer = GetComponent<AllignToPlayer>();

        ai = GetComponent<EnemyAI>();
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyAwareness = GetComponent<EnemyAwareness>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        reactionTime = reactionTimeDef;

    }

    void Update()
    {
        randomFloat = Random.Range(0f, 1f);
        spriteAnim.SetFloat("SpriteRotation", allignToPlayer.lastIndex);
    }

    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, playerTransform.position);
        
        if(reactionTime == 0 && enemyAwareness.isAggro == true)
        {
            reactionTime = reactionTimeDef;
            AttackCheck(distance);
        }
        else
        {
            reactionTime--;
        }
    }

    public void TakeDamage(float damage)
    {
        
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
        if (enemyHealth > 0)
        {
            StartCoroutine(PlayHitAnimation());
        }
        if (enemyHealth <= 0)
        {
            StartCoroutine(PlayDieAnimation());
        }
    }

    private IEnumerator PlayHitAnimation()
    {
        if (randomFloat <= 0.8f)
        {
            ai.setEnable(false);
            spriteAnim.SetTrigger("Hit");
            yield return new WaitForSeconds(0.1f); // Wait for 100 ms
            ai.setEnable(true);
            staggd = true;
        }
    }

    private IEnumerator PlayDieAnimation()
    {
        ai.setEnable(false);
        spriteAnim.SetTrigger("Die");
        yield return new WaitForSeconds(5f);
        enemyManager.RemoveEnemy(this);
        Destroy(gameObject);
    }

    public IEnumerator PlayAttackAnimation()
    {
        ai.setEnable(false);
        spriteAnim.SetTrigger("Atacc");
        yield return new WaitForSeconds(0.5f); // Wait for 500 ms
        ai.setEnable(true);
    }

    private void AttackCheck(float dist)
    {
        float tempDistance = dist;
        float tempRand;

        tempRand = randomFloat * 200f;

        if(!hasMelee) {tempDistance -= 32f;}
        if(dist > 50f) {tempDistance = 50f;}

        if(staggd && hasMissile) {Missile(); staggd = false;} 
        if(dist <= 4f)
        {
            Melee();
        }
        else if(tempRand < tempDistance && hasMissile)
        {
            Missile();
        }
    }

    private void Missile() 
    {
        StartCoroutine(PlayAttackAnimation());
        GameObject bulletObj = Instantiate(missile, spawnPoint.transform.position, Quaternion.identity);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(transform.forward * missileSpeed);

    }
    private void Melee() 
    {
    
        StartCoroutine(PlayAttackAnimation());
    }
}
