using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Stats")]
    public float bulletSpreadAngle = 5f;
    public bool isInInvintory;
    public float range = 20f;
    public float verticalRange = 20f;
    public float bigDamage = 2f;
    public float smallDamage = 1f;
    public float fireRate = 1f;
    public float gunShotRadius = 20f;

    [Header("Real-Time Bits")]
    private float nextTimeToFire;
    public bool isActive;

    [Header("References")]
    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;
    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;
    public GameObject bulletImpact;
    public GameObject bloodSplatter;
    private GunSwap gunSwap;
   
    void Start()
    {
       gunSwap = GameObject.Find("Player").GetComponent<GunSwap>();
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range / 2);
    }

   
    void Update()
    {
        bool has = gunSwap.HasBullets();

        if(Input.GetMouseButton(0) && Time.time > nextTimeToFire && has && isActive && isInInvintory)
        {
            Fire();
        }
        else if(Input.GetMouseButton(0) && Time.time > nextTimeToFire && has && isActive)
        {
            Debug.Log("Not in inventory");
        }
        else if(Input.GetMouseButton(0) && Time.time > nextTimeToFire && has && isInInvintory)
        {
            Debug.Log("Not Active");
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        
        if (enemy)
        {
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            enemyManager.RemoveEnemy(enemy);
        }
    }

    void Fire() 
    {
        //simulate gunfire radius

        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        
        //alert any enemy in earshot
        foreach (var enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }


        //play test audio
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        // Calculate spread
        float spreadX = Random.Range(-bulletSpreadAngle, bulletSpreadAngle);
        float spreadY = Random.Range(-bulletSpreadAngle, bulletSpreadAngle);
        Vector3 spreadDirection = Quaternion.Euler(spreadX, spreadY, 0) * transform.forward;

        //damage enemies
        RaycastHit hit;
        if (Physics.Raycast(transform.position, spreadDirection, out hit, range, raycastLayerMask))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy)
            {
                float dist = Vector3.Distance(hit.transform.position, transform.position);
                if (dist > range * .5f)
                {
                    enemy.TakeDamage(smallDamage);
                }
                else 
                {
                    enemy.TakeDamage(bigDamage);
                }
                Instantiate(bloodSplatter, hit.point, Quaternion.identity);
            }
            else
            {
                Instantiate(bulletImpact, hit.point, Quaternion.identity);
            }
            UpdateAmmo();
        }
        //reset timer
        nextTimeToFire = Time.time + fireRate;
    }

    public void UpdateAmmo()
    {
        gunSwap.TakeAmmo(1, 0, 0, 0);
    }
}
