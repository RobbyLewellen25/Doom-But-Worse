using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private Animator spriteAnim;
    private ProjectileAllignToPlayer allignToPlayer;

    public GameObject spawner; // The object that spawned this bullet
    public GameObject explosionPrefab;
    public GameObject explosionSprite;
    public AudioSource sound;
    public bool isRocket;
    private int damage;
    private float bigDamage;
    private float smallDamage;
    public bool isBFG;
    public float space = 5.0f;
    private float tempDamage = 0f;

    private void Start()
    {
        if(isRocket)
        {
            damage = (int)bigDamage;
        }
        else
        {
            bigDamage = Mathf.Max(spawner.GetComponent<Gun>().bigDamage, spawner.GetComponent<Gun>().smallDamage);
            smallDamage = Mathf.Min(spawner.GetComponent<Gun>().bigDamage, spawner.GetComponent<Gun>().smallDamage);

            // Calculate the number of intervals between smallDamage and bigDamage
            int intervals = Mathf.FloorToInt((bigDamage - smallDamage) / space) + 1;

            // Generate a random number between 0 and the number of intervals (inclusive), then multiply by space and add smallDamage to get a number between smallDamage and bigDamage in intervals of 5
            damage = Random.Range(0, intervals) * (int)space + (int)smallDamage;
        }

        sound.loop = false;
        spriteAnim = GetComponentInChildren<Animator>();
        allignToPlayer = GetComponent<ProjectileAllignToPlayer>();
    }

    void Update()
    {
        if (spriteAnim != null && allignToPlayer != null)
        {
            spriteAnim.SetFloat("SpriteRot", allignToPlayer.lastIndex);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != spawner && !other.isTrigger) // The detected object is not the spawner
        {
            if (sound != null && sound.clip != null) // Check if the AudioSource and the AudioClip are not null
            {
                AudioSource.PlayClipAtPoint(sound.clip, transform.position);
            }   
            if (other.gameObject.tag == "Player")
            {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                playerHealth.DamagePlayer(damage);
            }
            else if (other.gameObject.tag == "Enemy")
            {
                Enemy enemyHealth = other.gameObject.GetComponent<Enemy>();
                enemyHealth.TakeDamage(damage);
                
            }
            if (isRocket)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
            if (isBFG)
            {
                Vector3 direction = transform.position - spawner.transform.position;
               // direction.y = 0; // Ignore y component
                float angleStep = 45.0f / 40.0f;

                for (int i = 0; i < 40; i++)
                {
                    // Rotate the direction vector by the current step's angle
                    Vector3 rayDirection = Quaternion.Euler(0, -22.5f + i * angleStep, 0) * direction;

                    RaycastHit hit;
                    if (Physics.Raycast(spawner.transform.position, rayDirection, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Enemy"))
                        {
                            for(int j = 0; j < 15; j++)
                            {
                                tempDamage += Random.Range(1, 8);
                            }
                            // Assuming the enemy has a method called TakeDamage
                            hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(tempDamage);
                            tempDamage = 0f;
                            Instantiate(explosionPrefab, hit.point, Quaternion.identity);
                        }
                    }
                }
            }
            Instantiate(explosionSprite, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}