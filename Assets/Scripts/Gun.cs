using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f;

    public float bigDamage = 2f;
    public float smallDamage = 1f;

    public float fireRate = 1f;
    private float nextTimeToFire;
    public float gunShotRadius = 20f;

    public int maxAmmo;
    private int ammo;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;

    private BoxCollider gunTrigger;
    public EnemyManager enemyManager;

    public GameObject bulletImpact;
    public GameObject bloodSplatter;
   
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range / 2);
        CanvasManager.Instance.UpdateAmmo(ammo);
    }

   
    void Update()
    {
        if(Input.GetMouseButton(0) && Time.time > nextTimeToFire && ammo > 0)
        {
            Fire();
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

        //damage enemies
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, raycastLayerMask))
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
        //reset timer
        nextTimeToFire = Time.time + fireRate;
        ammo--;
        CanvasManager.Instance.UpdateAmmo(ammo);
    }

    public void GiveAmmo(int amount, GameObject pickup)
    {
        if(ammo < maxAmmo) 
        {
            ammo += amount;
            Destroy(pickup);
        }
        
        if(ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }
        CanvasManager.Instance.UpdateAmmo(ammo);
    }
}
