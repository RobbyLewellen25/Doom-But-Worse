using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float enemyHealth = 2f;
    public bool hasMelee;
    public bool hasMissile;
    public float missileSpeed = 1000f;
    public int reactionTimeDef = 12;
    public GameObject missile;
    public GameObject gunHitEffect;
    public Transform spawnPoint;

    [Header("References")]
    public EnemyManager enemyManager;

    private Animator spriteAnim;
    private AllignToPlayer allignToPlayer;
    private EnemyAI ai;
    private EnemyAwareness enemyAwareness;
    private Transform playerTransform;
    private CapsuleCollider enemyCollider;

    private int reactionTime;
    private bool dead;
    private bool isAttacking = false;
    private bool staggered;
    private float randomFloat;
    private float distance;

    private void Start()
    {
        InitializeComponents();
        reactionTime = reactionTimeDef;
        dead = false;
    }

    private void InitializeComponents()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        randomFloat = Random.Range(0f, 1f);

        spriteAnim = GetComponentInChildren<Animator>();
        allignToPlayer = GetComponent<AllignToPlayer>();
        ai = GetComponent<EnemyAI>();
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyAwareness = GetComponent<EnemyAwareness>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        randomFloat = Random.Range(0f, 1f);
        if (dead) return;
        spriteAnim.SetFloat("SpriteRotation", allignToPlayer.lastIndex);
    }

    void FixedUpdate()
    {
        if (dead) return;
        distance = Vector3.Distance(transform.position, playerTransform.position);
        PerformAttackCheck();
    }

    private void PerformAttackCheck()
    {
        //Debug.Log("Distance: " + distance);
        if(reactionTime == 0 && enemyAwareness.isAggro)
        {
            AttackCheck(distance);
            reactionTime = reactionTimeDef;
            //Debug.Log("Attack");
        }
        else if (reactionTime == 0)
        {
            reactionTime = reactionTimeDef;
        }
        else
        {
            reactionTime--;
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth > 0)
        {
            StartCoroutine(PlayHitAnimation());
        }
        else
        {
            StartCoroutine(PlayDieAnimation());
            dead = true;
            enemyCollider.enabled = false;
        }
    }

    private IEnumerator PlayHitAnimation()
    {
        if (randomFloat <= 0.8f)
        {
            ai.setEnable(false);
            spriteAnim.SetTrigger("Hit");
            yield return new WaitForSeconds(0.1f);
            ai.setEnable(true);
            staggered = true;
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

    private void AttackCheck(float dist)
    {
        if (isAttacking) return;

        float tempDistance = dist;
        float tempRand = randomFloat * 200f;

        if(!hasMelee) tempDistance -= 32f;
        if(dist > 50f) tempDistance = 50f;

        if(staggered && hasMissile) 
        {
            StartCoroutine(Missile()); 
            staggered = false;
        } 
        else if(dist <= 4f)
        {
            StartCoroutine(Melee());    
        }
        else if(tempRand < tempDistance && hasMissile)
        {
            StartCoroutine(Missile());
        }
    }

    private IEnumerator Missile() 
    {
        isAttacking = true;
        LookAtPlayer();
        yield return StartCoroutine(PlayAttackAnimation());
        LookAtPlayer();

        GameObject bulletObj = Instantiate(missile, spawnPoint.transform.position, Quaternion.identity);
        impBulletScript bulletScript = bulletObj.GetComponent<impBulletScript>();
        bulletScript.spawner = gameObject; // Pass a reference to the spawner to the bullet

        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(transform.forward * missileSpeed);
        isAttacking = false;
    }

    private IEnumerator Melee() 
    {
        isAttacking = true;
        LookAtPlayer();
        yield return StartCoroutine(PlayAttackAnimation());

        PlayerHealth playerHealth = playerTransform.gameObject.GetComponent<PlayerHealth>();
        if (Vector3.Distance(transform.position, playerTransform.position) <= 4f)
        {
            playerHealth.DamagePlayer(10);
        }
        isAttacking = false;
    }

    private IEnumerator PlayAttackAnimation()
    {
        ai.setEnable(false);
        spriteAnim.SetTrigger("Atacc");
        yield return new WaitForSeconds(0.8f);
        ai.setEnable(true);
    }

    private void LookAtPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    public bool isDead()
    {
        return dead;
    }
}