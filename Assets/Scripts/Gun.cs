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
    public int rays;
    public float raySpread = 10f;
    public enum AmmoType { Bullets = 1, Shells, Rockets, Cells }
    public AmmoType ammoType;
    public bool isFist;

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

        float initialSpreadAngle = -raySpread / 2;

         for (int i = 0; i < rays; i++)
            {
            // Calculate spread
            float spreadX = initialSpreadAngle;

            if (rays > 1)
            {
                spreadX += (raySpread / (rays - 1)) * i;
            }

            spreadX = Random.Range(-bulletSpreadAngle + spreadX, bulletSpreadAngle + spreadX);
            spreadX = Mathf.Clamp(spreadX, -180f, 180f); // Clamp spreadX to valid range

            float spreadY = Random.Range(-bulletSpreadAngle, bulletSpreadAngle);
            spreadY = Mathf.Clamp(spreadY, -180f, 180f); // Clamp spreadY to valid range
            
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
            }
        }
        UpdateAmmo();
        //reset timer
        nextTimeToFire = Time.time + fireRate;
    }

    public void UpdateAmmo()
    {
        switch (ammoType)
        {
            case AmmoType.Bullets:
                gunSwap.TakeAmmo(1, 0, 0, 0);
                break;
            case AmmoType.Shells:
                gunSwap.TakeAmmo(0, 1, 0, 0);
                break;
            case AmmoType.Rockets:
                gunSwap.TakeAmmo(0, 0, 1, 0);
                break;
            case AmmoType.Cells:
                gunSwap.TakeAmmo(0, 0, 0, 1);
                break;
        }
    }
}
